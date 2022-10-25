using Models.Models;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface IEntityRepository<T,K> : IRepositoryBase<T,K> where T : Entity
                                                                   where K : RequestFeatures
    {

    }
}
