using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class TagsRepository : Repository<Tag>
    {
        public TagsRepository(BlogDbContext appContext) : base(appContext)
        {

        }

        public async Task<Tag> GetOrCreate(string name)
        {
            var result = await Set.FirstOrDefaultAsync(x => x.Name == name);

            if(result == null)
            {
                var tag = new Tag { Name = name };
                await Create(tag);
                return tag;
            }
            else
                return result;
        }
    }
}
