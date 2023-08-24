using Blog.WebApi.Domain.Models.Entities;

namespace Blog.WebApi.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> FindUserByEmail(string email);
    }
}
