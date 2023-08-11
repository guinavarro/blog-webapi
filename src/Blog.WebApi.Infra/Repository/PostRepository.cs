using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi.Infra.Repository
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts() =>
            await _context.Posts.AsNoTracking()
            .Include(p => p.ImagePost)
            .Include(p => p.TagsPost)
                .ThenInclude(t => t.Tag)
            .ToListAsync();

        public async Task<Post> GetPostWithInclude(Guid key) =>
            await _context.Posts.Where(p => p.Key == key)
                .Include(p => p.ImagePost)
                .Include(p => p.TagsPost)
                    .ThenInclude(t => t.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync();
    
    }
}
