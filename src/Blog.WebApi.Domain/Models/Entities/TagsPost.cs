using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Domain.Models.Entities
{
    public class TagsPost : Base
    {
        public int PostId { get; private set; }
        public int TagId { get; private set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }


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
