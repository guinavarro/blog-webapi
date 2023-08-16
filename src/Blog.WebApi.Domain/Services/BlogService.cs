using Blog.WebApi.Domain.Helpers.Extensions;
using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models;
using Blog.WebApi.Domain.Models.Entities;
using Blog.WebApi.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Blog.WebApi.Domain.Services
{
    public class BlogService : IBlogService
    {
        #region Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;
        private readonly ITagsPostRepository _tagsPostRepository;
        private readonly IImageFileRepository _imageFileRepository;

        public BlogService(ITagRepository tagRepository,
        ITagsPostRepository tagsPostRepository,
        IPostRepository postRepository, 
        IImageFileRepository imageFileRepository, 
        IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _postRepository = postRepository;
            _tagsPostRepository = tagsPostRepository;
            _imageFileRepository = imageFileRepository;
        }

        #endregion

        #region Methods
        public async Task<Return<bool>> Post(CreatePostViewModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                // TODO: Criar validação pra ver qual é o usuário logado            

                int? imageId = null;
                if (model.Image is not null)
                {
                    var imageKey = Guid.NewGuid();

                    var image = new ImageFile(GenerateFileName(model.Title, model.Image.ContentType), IFormFileToByteArray(model.Image), model.Image.ContentType, imageKey);
                    _imageFileRepository.Add(image);
                    await _imageFileRepository.SaveChangesAsync();

                    var imagePost = await _imageFileRepository.Find<ImageFile>(imageKey);
                    imageId = imagePost?.Id;
                }

                var postKey = Guid.NewGuid();
                var post = new Post(1, model.Title, model.Message, postKey, imageId);

                _postRepository.Add(post);
                await _postRepository.SaveChangesAsync();

                var postEntity = await _postRepository.Find<Post>(postKey);

                if (model.Tags is not null && model.Tags.Any())
                {
                    foreach (var tagModel in model.Tags)
                    {
                        // Verify if tag already exist on database
                        var tagAlreadyExist = await _tagRepository.IsTagExists(tagModel);

                        if (tagAlreadyExist.Item1 == true)
                        {
                            _tagsPostRepository.Add(new TagsPost(postEntity.Id, tagAlreadyExist.Item2.Value));
                            await _tagRepository.SaveChangesAsync();
                        }

                        // If tag doesn't exists, we will add on database
                        var tagKey = Guid.NewGuid();
                        _tagRepository.Add(new Tag(tagModel, tagKey));
                        await _tagRepository.SaveChangesAsync();

                        var lastTagInserted = await _tagRepository.Find<Tag>(tagKey);
                        _tagsPostRepository.Add(new TagsPost(postEntity.Id, lastTagInserted.Id));
                        await _tagsPostRepository.SaveChangesAsync();
                    }
                }

                await _unitOfWork.CommitAsync();
                return new Return<bool>(true, "Post realizado com sucesso!");
            }
            catch
            {
                await _unitOfWork.Rollback();
                return new Return<bool>(true, "Houve um erro na hora de realizar o Post. Por favor, tente novamente");
            }
        }

        public async Task<Return<PostViewModel>> GetPostById(Guid key)
        {
            if (key == Guid.Empty)
                return new Return<PostViewModel>(false, "O Post Id é inválido. Por favor, insira um Post Id válido e tente novamente.");

            var post = await _postRepository.GetPostWithInclude(key);

            if (post == null)
                return new Return<PostViewModel>(false, "Não foi possível localizar o post em nossa base pelo Post Id passado.");

            var postViewModel = new PostViewModel
            {
                Key = post.Key,
                Title = post.Title,
                Message = post.Content,
                Active = post.Active,
                PublishDate = post.Date,
                Tags = post.TagsPost.Any() ? post.TagsPost.Select(_ => _.Tag.Name).ToList() : null,
                Image = post.ImagePost != null ? new ImageViewModel
                {
                    Name = post.ImagePost.Name,
                    DataBase64 = Convert.ToBase64String(post.ImagePost.Data),
                    ContentType = post.ImagePost.ContentType
                } : null
            };

            return new Return<PostViewModel>(true, "A busca retornou o Post com sucesso", postViewModel);
        }

        public async Task<Return<IEnumerable<PostViewModel>>> GetAllPosts(FilterViewModel filter)
        {
            var postList = new List<PostViewModel>();

            var posts = await _postRepository.GetAllPosts();

            if (posts == null)
            {
                return new Return<IEnumerable<PostViewModel>>(false, "Não foi encontrado nenhum post");
            }

            if (posts.Any())
            {
                foreach (var post in posts)
                {
                    var postViewModel = new PostViewModel
                    {
                        Key = post.Key,
                        Title = post.Title,
                        Message = post.Content,
                        Active = post.Active,
                        PublishDate = post.Date,
                        Tags = post.TagsPost.Any() ? post.TagsPost.Select(_ => _.Tag.Name).ToList() : null,
                        Image = post.ImagePost != null ? new ImageViewModel
                        {
                            Name = post.ImagePost.Name,
                            DataBase64 = Convert.ToBase64String(post.ImagePost.Data),
                            ContentType = post.ImagePost.ContentType
                        } : null
                    };

                    postList.Add(postViewModel);
                }
            }


            if (filter.PublishDate is not null)
                postList = postList.Where(p => DateTime.Compare(p.PublishDate.Date, filter.PublishDate.Value.Date) == 0).ToList();
            if (filter.ContainsInName is not null)
                postList = postList.Where(p => TransformStringToSearch(p.Title).Contains(TransformStringToSearch(filter.ContainsInName))).ToList();
            if (filter.ContainsInText is not null)
                postList = postList.Where(p => TransformStringToSearch(p.Message).Contains(TransformStringToSearch(filter.ContainsInText))).ToList();
            if (filter.Status is not null)
                postList = postList.Where(p => p.Active == filter.Status).ToList();
            if (filter.WithImage is not null)
            {
                if (filter.WithImage == true)
                    postList = postList.Where(p => p.Image is not null).ToList();
                else
                    postList = postList.Where(p => p.Image is null).ToList();
            }
            if (filter.Tags is not null)
            {
                if (filter.Tags.Count > 0)
                {
                    postList = postList.Where(p => p.Tags.Intersect(filter.Tags).Any()).ToList();
                }
            }

            return new Return<IEnumerable<PostViewModel>>(true, $"Busca realizada com sucesso: ${postList.Count} registros", postList);

        }

        public async Task<Return<bool>> DisablePost(Guid key)
        {
            if (key == Guid.Empty)
            {
                return new Return<bool>(false, "O Guid passado é inválido.");
            }

            var entity = await _postRepository.Find<Post>(key);

            entity.UpdateActiveStatus(false);

            var result = await _postRepository.SaveChangesAsync();

            if (result)
            {
                return new Return<bool>(true, "Post desativado com sucesso.", true);
            }
            else
            {
                return new Return<bool>(true, "Houve um erro na hora de tentar desativar o post.", true);
            }
        }

        #endregion

        #region Private Methods
        private string TransformStringToSearch(string str) => str.RemoveAccents().ToLower();
        private string GenerateFileName(string postName, string fileType) =>
            $"{postName.TransformToLowerCase()}_{DateTime.Now:ddMMyyyy_HHmm}.{Regex.Replace(fileType, @"image\/", "")}";
        private static byte[] IFormFileToByteArray(IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                return ms.ToArray();
            }
        }
        #endregion

    }
}
