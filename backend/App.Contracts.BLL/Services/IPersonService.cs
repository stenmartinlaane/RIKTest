using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IPersonService : IEntityRepository<App.BLL.DTO.Person>, IPersonRepositoryCustom<App.BLL.DTO.Person>
{
    
}

public interface IContestRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}