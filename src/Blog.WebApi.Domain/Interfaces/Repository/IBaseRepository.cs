using Blog.WebApi.Domain.Models.Entities;

namespace Blog.WebApi.Domain.Interfaces.Repository
{
    public interface IBaseRepository
    {
        void Add<T>(T entity) where T : Base;
        void Update<T>(T entity) where T : Base;
        void Delete<T>(T entity) where T : Base;
        Task<bool> SaveChangesAsync();
    }
}
