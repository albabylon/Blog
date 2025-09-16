namespace Blog.Infrastructure.Contracts.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);

        Task Create(T item);

        Task Update(T item);

        Task Delete(T item);

        Task<IEnumerable<T>> GetAll();
    }
}
