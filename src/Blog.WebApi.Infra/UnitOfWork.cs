using Blog.WebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.WebApi.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();
        

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            _transaction.Commit();
        }

        
        public async Task Rollback()
        {
            _transaction.Rollback();
            await _context.DisposeAsync();
        }

        public void Dispose() => _context.Dispose();
        

    }
}
