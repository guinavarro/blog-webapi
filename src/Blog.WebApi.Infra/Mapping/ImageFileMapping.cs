using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class ImageFileMapping : BaseMapping<ImageFile>
    {
        public ImageFileMapping(): base("images")
        {           
        }

        public override void Configure(EntityTypeBuilder<ImageFile> builder)
        {
            base.Configure(builder);

            builder.Property(_ => _.Name).HasMaxLength(100);
            builder.Property(_ => _.Data).HasColumnType("bytea").IsRequired();
            builder.Property(_ => _.ContentType).HasMaxLength(10).IsRequired(false);
        }
    }
}
