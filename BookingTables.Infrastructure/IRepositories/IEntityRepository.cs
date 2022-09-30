using Models.Models;
using Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface IEntityRepository<T,K> : IRepositoryBase<T,K> where T : Entity
                                                                   where K : RequestFeatures
    {

    }
}
