using Blog.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
    public class UserConfuiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.ToTable("Users").HasKey(p => p.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();

            //// Отключаем каскадное удаление
            //builder.HasOne(f => f.User)
            //      .WithMany()
            //      .HasForeignKey(f => f.UserId)
            //      .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(f => f.CurrentFriend)
            //      .WithMany()
            //      .HasForeignKey(f => f.CurrentFriendId)
            //      .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
