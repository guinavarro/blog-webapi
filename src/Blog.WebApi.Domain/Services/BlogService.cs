using Blog.WebApi.Domain.Helpers.Extensions;
using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models;
using Blog.WebApi.Domain.Models.Entities;
using Blog.WebApi.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Http;
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

        public BlogService(ITagRepository tagRepository, ITagsPostRepository tagsPostRepository, IPostRepository postRepository, IImageFileRepository imageFileRepository, IUnitOfWork unitOfWork)
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
                Title = post.Title,
                Message = post.Content,
                Image = post.ImagePost != null ? new ImageViewModel
                {
                    Name = post.ImagePost.Name,
                    DataBase64 = Convert.ToBase64String(post.ImagePost.Data),
                    ContentType = post.ImagePost.ContentType
                } : null,
                Tags = post.TagsPost?.SelectMany(tagPost => tagPost.Tags.Select(tag => tag.Name)).ToList() ?? new List<string>()
            };

            return new Return<PostViewModel>(true, "A busca retornou o Post com sucesso", postViewModel);
        }
        #endregion

        #region Private Methods
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
