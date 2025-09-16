using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories
{
    public class CommentsRepository : Repository<Comment>
    {
        public CommentsRepository(BlogDbContext appContext) : base(appContext)
        {

        }
    }
}
