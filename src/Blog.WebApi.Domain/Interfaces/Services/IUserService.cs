using Blog.WebApi.Domain.Models;
using Blog.WebApi.Domain.Models.Entities;

namespace Blog.WebApi.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<Return<bool>> Register(User user);
        Task<User> FindUserByEmail(string email);
        Task<Author> FindAuthorByName(string name);
    }
}
