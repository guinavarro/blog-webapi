using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi.Infra.Repository
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        private readonly BlogContext _context;
        public AuthorRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public Task<Author> FindAuthorByName(string name)
        {
            return _context.Authors.Where(x => String.Compare(x.Name, name) == 0).FirstOrDefaultAsync();
        }
    }
}
