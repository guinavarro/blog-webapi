using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WebApi.Infra.Mapping
{
    public class BaseMapping<T>: IEntityTypeConfiguration<T> where T: Base
    {
        private readonly string _tableName;

        public BaseMapping(string tableName)
        {
            _tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrEmpty(_tableName))
                builder.ToTable(_tableName);

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.Key).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(_ => _.Date).HasColumnType("timestamp without time zone")
                                                .HasDefaultValueSql("NOW()")
                                                .ValueGeneratedOnAddOrUpdate();

        }
    }
}
