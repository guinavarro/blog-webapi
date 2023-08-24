using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class UserMapping : BaseMapping<User>
    {
        public UserMapping(): base("users")
        {           
        }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(_ => _.UserName).IsRequired(true);
            builder.Property(_ => _.Email).IsRequired(true);         
            builder.Property(_ => _.PasswordHash).IsRequired(true);         
        }
    }
}
