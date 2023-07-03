using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Domain.Models.Entities
{
    public sealed class Post : Base
    {
        public int AuthorId { get; private set; }
        public int FileId { get; private set; }
        public int TagId { get; set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
    }
}
