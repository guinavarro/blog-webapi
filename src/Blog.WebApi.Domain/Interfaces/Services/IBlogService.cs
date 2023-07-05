using Blog.WebApi.Domain.Models.ViewModels;

namespace Blog.WebApi.Domain.Interfaces.Services
{
    public interface IBlogService
    {
        Task<bool> Post(PostViewModel model);
    }
}
