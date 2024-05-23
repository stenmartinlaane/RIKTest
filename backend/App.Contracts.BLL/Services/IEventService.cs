using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IEventService : IEntityRepository<App.BLL.DTO.Event>, IEventRepositoryCustom<App.BLL.DTO.Event>
{
    
}
public interface IEventRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}