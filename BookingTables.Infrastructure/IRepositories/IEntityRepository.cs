using Models.Models;


namespace Infrastructure.IRepositories
{
    public interface IEntityRepository<T> : IRepositoryBase<T> where T : Entity
    {

    }
}
