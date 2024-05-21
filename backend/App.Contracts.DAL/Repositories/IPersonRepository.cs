using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IPersonRepository: IEntityRepository<App.DAL.DTO.Person>, IPersonRepositoryCustom<App.DAL.DTO.Event>
{
    
}

public interface IPersonRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}