using Blog.WebApi.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi.Infra.Repository
{
    public class TagRepository: BaseRepository, ITagRepository
    {
        private readonly BlogContext _context;

        public TagRepository(BlogContext context): base(context) 
        {
            _context = context;
        }

        public async Task<Tuple<bool, int?>> IsTagExists(string tagName)
        {
            var tag = await _context.Tags.Where(_ => _.Name.ToLower().Contains(tagName.ToLower())).FirstOrDefaultAsync();

            if(tag != null)
                return new Tuple<bool, int?>(true, tag.Id);

            return new Tuple<bool, int?>(false, null);
        }
    }
}
