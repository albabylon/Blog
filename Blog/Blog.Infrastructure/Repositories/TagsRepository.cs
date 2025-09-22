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

        public async Task<Tag?> GetByName(string name)
        {
            return await Set.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Tag>> GetAllByArticleId(int articleId)
        {
            return await Set
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Article)
                .Where(x => x.ArticleTags.Any(y => y.ArticleId == articleId))
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync() ?? throw new Exception($"теги по id {articleId} не найдены");
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
