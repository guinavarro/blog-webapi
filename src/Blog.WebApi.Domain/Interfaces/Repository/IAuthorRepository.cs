using Blog.WebApi.Domain.Models.Entities;

namespace Blog.WebApi.Domain.Interfaces.Repository
{
    public interface IAuthorRepository : IBaseRepository
    {
        Task<Author> FindAuthorByName(string name);
    }
}
