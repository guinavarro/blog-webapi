using Blog.WebApi.Domain.Models.Entities;

namespace Blog.WebApi.Domain.Interfaces.Repository
{
    public interface IPostRepository: IBaseRepository
    {
        Task<Post> GetPostWithInclude(Guid key);
        Task<IEnumerable<Post>> GetAllPosts();
    }
}
