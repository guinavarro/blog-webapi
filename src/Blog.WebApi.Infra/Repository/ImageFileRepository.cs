using Blog.WebApi.Domain.Interfaces.Repository;

namespace Blog.WebApi.Infra.Repository
{
    public class ImageFileRepository : BaseRepository, IImageFileRepository
    {
        private readonly BlogContext _context;

        public ImageFileRepository(BlogContext context) : base(context)
        {
            _context = context;
        }
    }
}
