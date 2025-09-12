using Blog.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
    public class TagConfuiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            // Ограничение уникальности для имени тега
            builder.HasIndex(t => t.Name).IsUnique();
        }
    }
}
