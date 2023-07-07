using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models.Entities;
using Blog.WebApi.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Blog.WebApi.Domain.Services
{
    public class BlogService : IBlogService
    {
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

        public async Task<bool> Post(PostViewModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                // TODO: Criar validação pra ver qual é o usuário logado            

                int? imageId = null;
                if (model.FileBytes is not null)
                {
                    var imageKey = Guid.NewGuid();
                    _imageFileRepository.Add(new ImageFile("", model.FileBytes, "jpeg", imageKey));

                    var imagePost = await _imageFileRepository.Find<ImageFile>(imageKey);
                    imageId = imagePost?.Id;
                }

                var postKey = Guid.NewGuid();
                _postRepository.Add(new Post(1, model.Title, model.Message, postKey, imageId));
                await _postRepository.SaveChangesAsync();

                var post = await _postRepository.Find<Post>(postKey);

                if (model.Tags.Any())
                {
                    foreach (var tagModel in model.Tags)
                    {
                        // Verify if tag already exist on database
                        var tagAlreadyExist = await _tagRepository.IsTagExists(tagModel);

                        if (tagAlreadyExist.Item1 == true)
                        {
                            _tagsPostRepository.Add(new TagsPost(post.Id, tagAlreadyExist.Item2.Value));
                            await _tagRepository.SaveChangesAsync();
                        }

                        // If tag doesn't exists, we will add on database
                        var tagKey = Guid.NewGuid();
                        _tagRepository.Add(new Tag(tagModel, tagKey));
                        await _tagRepository.SaveChangesAsync();

                        var lastTagInserted = await _tagRepository.Find<Tag>(tagKey);
                        _tagsPostRepository.Add(new TagsPost(post.Id, lastTagInserted.Id));
                        await _tagsPostRepository.SaveChangesAsync();
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                return false;
            }
        }

      
    }
}
