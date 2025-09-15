using Blog.Contracts.Interfaces;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _context;


        public Repository(BlogDbContext appContext)
        {
            _context = appContext;
        }


        protected DbSet<T> Set => _context.Set<T>();


        public async Task Create(T item)
        {
            Set.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            Set.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
        {
            var result = await Set.FindAsync(id);
            return result ?? throw new Exception($"{id} не найден");
        }

        public async Task Update(T item)
        {
            Set.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Set.ToListAsync();
        }
    }
}
