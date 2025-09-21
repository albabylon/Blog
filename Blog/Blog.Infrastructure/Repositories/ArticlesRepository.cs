using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class ArticlesRepository : Repository<Article>
    {
        public ArticlesRepository(BlogDbContext appContext) : base(appContext)
        {

        }

        public async Task<IEnumerable<Article>> GetAllByAuthorId(string authorId)
        { 
            return await Set.Where(x => x.AuthorId ==  authorId).ToListAsync() 
                ?? throw new Exception($"{authorId} не найден");          
        }

        public async Task<IEnumerable<Article>> GetAllByTag(string tagName)
        {
            return await Set
                .Include(x => x.Author)
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Tag)
                .Where(x => x.ArticleTags.Any(y => y.Tag.Name == tagName))
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync() ?? throw new Exception($"статьи по тегу {tagName} не найден");
        }
    }
}
