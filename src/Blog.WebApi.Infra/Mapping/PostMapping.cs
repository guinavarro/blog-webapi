using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class PostMapping : BaseMapping<Post>
    {
        public PostMapping(): base("posts")
        {           
        }

        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            builder.Property(_ => _.AuthorId).IsRequired(true);
            builder.Property(_ => _.FileId).IsRequired(false);
            builder.Property(_ => _.Title).HasColumnType("varchar").HasMaxLength(200).IsRequired(true);
            builder.Property(_ => _.Content).HasColumnType("text").IsRequired(true);           
        }
    }
}
