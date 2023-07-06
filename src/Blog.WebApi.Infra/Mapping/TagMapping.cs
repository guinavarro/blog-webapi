using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class TagMapping : BaseMapping<Tag>
    {
        public TagMapping(): base("tags")
        {           
        }

        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.Property(_ => _.Name).HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
        }
    }
}
