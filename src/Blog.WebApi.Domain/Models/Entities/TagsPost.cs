using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.WebApi.Domain.Models.Entities
{
    public class TagsPost : Base
    {
        public int PostId { get; private set; }
        public int TagId { get; private set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }


        public TagsPost()
        {
            
        }
        public TagsPost(int postId, int tagId)
        {
            UpdateTagPost(postId, tagId);
        }

        void UpdateTagPost(int postId, int tagId)
        {
            PostId = postId;
            TagId = tagId;
        }
    }
}
