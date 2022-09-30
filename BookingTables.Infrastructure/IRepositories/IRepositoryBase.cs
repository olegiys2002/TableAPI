namespace Infrastructure.IRepositories
{
    public interface IRepositoryBase <T,K>
    {
 
        Task<List<T>> FindAllAsync(bool trackChanges,K requestFeatures);
        void Delete(T entity);
        void Create(T entity);
        void Update(T entity);

    }
}
