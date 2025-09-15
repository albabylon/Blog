using Blog.Contracts.Entities;
using Blog.Infrastructure.Data;

namespace Blog.Infrastructure.Repositories
{
    public class TagsRepository : Repository<Tag>
    {
        public TagsRepository(BlogDbContext appContext) : base(appContext)
        {

        }
    }
}
