namespace Blog.WebApi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        Task CommitAsync();
        Task Rollback();
    }
}
