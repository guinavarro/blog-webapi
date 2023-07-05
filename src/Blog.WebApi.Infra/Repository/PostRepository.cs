using Blog.WebApi.Domain.Interfaces.Repository;

namespace Blog.WebApi.Infra.Repository
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context) : base(context)
        {
            _context = context;
        }
    }
}
