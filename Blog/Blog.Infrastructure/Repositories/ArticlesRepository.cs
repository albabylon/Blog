using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories
{
    public class ArticlesRepository : Repository<Article>
    {
        public ArticlesRepository(BlogDbContext appContext) : base(appContext)
        {

        }
    }
}
