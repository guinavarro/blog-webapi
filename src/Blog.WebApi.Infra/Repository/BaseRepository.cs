using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Infra.Repository
{
    public class BaseRepository: IBaseRepository
    {
        private readonly BlogContext _context;

        public BaseRepository(BlogContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Find<T>(Guid guid) where T : Base => await _context.Set<T>().FirstOrDefaultAsync(_ => _.Key == guid);

        public void Add<T>(T entity) where T : Base => _context.Add(entity);

        public void Delete<T>(T entity) where T : Base => _context.Remove(entity);        

        public void Update<T>(T entity) where T : Base => _context.Update(entity);      

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
        
    }
}
