using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class CommentsRepository : Repository<Comment>
    {
        public CommentsRepository(BlogDbContext appContext) : base(appContext)
        {

        }

        public override async Task<Comment> Get(int id)
        {
            return await Set.Include(c => c.Article)
                            .Include(c => c.User)
                            .FirstOrDefaultAsync(x => x.Id == id) 
                            ?? throw new Exception($"{id} не найден");
        }

        public override async Task<IEnumerable<Comment>> GetAll()
        {
            return await Set.Include(c => c.Article)
                            .Include(c => c.User)
                            .OrderByDescending(a => a.CreatedAt)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByArticleId(int articleId)
        {
            return await Set.Include(c => c.Article)
                            .Include(c => c.User)
                            .Where(c => c.ArticleId == articleId)
                            .OrderByDescending(a => a.CreatedAt)
                            .ToListAsync();
        }
    }
}
