using Blog.WebApi.Domain.Models.Entities;
using System.Linq.Expressions;

namespace Blog.WebApi.Domain.Interfaces.Repository
{
    public interface IBaseRepository
    {
        Task<T> Find<T>(Guid guid) where T : Base;
        void Add<T>(T entity) where T : Base;
        void Update<T>(T entity) where T : Base;
        void Delete<T>(T entity) where T : Base;
        Task<bool> SaveChangesAsync();
    }
}
