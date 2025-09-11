using Blog.Contracts.Entities;
using Blog.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ArticleTag> ArticleTag { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (!Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.Migrate();
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfuiguration());
            modelBuilder.ApplyConfiguration(new CommentConfuiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfuiguration());
            modelBuilder.ApplyConfiguration(new TagConfuiguration());
            modelBuilder.ApplyConfiguration(new ArticleTagConfuiguration());
        }
    }
}
