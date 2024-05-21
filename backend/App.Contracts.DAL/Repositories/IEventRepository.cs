using Base.Contracts.DAL;
namespace App.Contracts.DAL.Repositories;

public interface IEventRepository: IEntityRepository<App.DAL.DTO.Event>, IEventRepositoryCustom<App.DAL.DTO.Event>
{

}

public interface IEventRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}
