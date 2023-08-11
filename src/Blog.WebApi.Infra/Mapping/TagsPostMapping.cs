using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class TagsPostMapping : BaseMapping<TagsPost>
    {
        public TagsPostMapping(): base("tagspost")
        {           
        }

        public override void Configure(EntityTypeBuilder<TagsPost> builder)
        {
            base.Configure(builder);

            builder.Property(_ => _.PostId).IsRequired();
            builder.Property(_ => _.TagId).IsRequired();

            builder.HasOne(_ => _.Tag)
                .WithMany(_ => _.TagsPost);

            builder.HasOne(_ => _.Post)
                .WithMany(_ => _.TagsPost);
                
        }
    }
}
