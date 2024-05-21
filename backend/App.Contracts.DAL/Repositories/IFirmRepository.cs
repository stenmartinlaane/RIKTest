using Base.Contracts.DAL;
namespace App.Contracts.DAL.Repositories;

public interface IFirmRepository: IEntityRepository<App.DAL.DTO.Firm>, IFirmRepositoryCustom<App.DAL.DTO.Firm>
{

}

public interface IFirmRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}
