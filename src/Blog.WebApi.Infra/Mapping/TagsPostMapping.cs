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

            builder.HasMany(_ => _.Tags)
                .WithMany(_ => _.TagsPost);

            builder.HasMany(_ => _.Posts)
                .WithMany(_ => _.TagsPost);
                
        }
    }
}
