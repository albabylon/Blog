using Blog.Contracts.Interfaces;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blog.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _appContext;
        private Dictionary<Type, object> _repositories;


        public UnitOfWork(BlogDbContext appContext)
        {
            _appContext = appContext;
            _repositories ??= new Dictionary<Type, object>();
        }


        public void Dispose()
        {
            
        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            if (hasCustomRepository)
            {
                var customRepo = _appContext.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                    return customRepo;
            }

            var type = typeof(TEntity);
            if (!_repositories.TryGetValue(type, out object? value))
            {
                value = new Repository<TEntity>(_appContext);
                _repositories[type] = value;
            }

            return (IRepository<TEntity>)value;

            //return hasCustomRepository
            //    ? _appContext.GetService<IRepository<TEntity>>()
            //    : new Repository<TEntity>(_appContext);
        }
    }
}
