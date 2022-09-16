
namespace Infrastructure.IRepositories
{
    public interface IRepositoryBase <T>
    {
 
        Task<List<T>> FindAllAsync(bool trackChanges);
        void Delete(T entity);
        void Create(T entity);
        void Update(T entity);

    }
}
