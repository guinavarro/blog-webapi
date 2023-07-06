using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class AuthorMapping : BaseMapping<Author>
    {
        public AuthorMapping(): base("authors")
        {           
        }

        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            builder.Property(_ => _.Name).HasMaxLength(100);
           
        }
    }
}
