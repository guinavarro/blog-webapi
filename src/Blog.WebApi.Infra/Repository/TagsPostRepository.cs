using Blog.WebApi.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Infra.Repository
{
    public class TagsPostRepository : BaseRepository, ITagsPostRepository
    {
        private readonly BlogContext _context;

        public TagsPostRepository(BlogContext context) : base(context)
        {
            _context = context;
        }
    }
}
