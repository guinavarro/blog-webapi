using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.WebApi.Domain.Models.Entities
{
    public class Post : Base
    {
        public int AuthorId { get; private set; }
        public int? FileId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        [ForeignKey("FileId")]
        public virtual ImageFile ImagePost { get; private set; }

        public Post(int authorId, string title, string content, Guid key, int? fileId = null) : base(key)
        {
            AuthorId = authorId;
            Title = title;
            Content = content;
            FileId = fileId;
        }

    }
}
