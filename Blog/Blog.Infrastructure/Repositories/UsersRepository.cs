using Blog.Domain.Identity;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(BlogDbContext appContext) : base(appContext)
        {

        }
    }
}
