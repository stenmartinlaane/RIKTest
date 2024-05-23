using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface iFirmService : IEntityRepository<App.BLL.DTO.Firm>, IFirmRepositoryCustom<App.BLL.DTO.Firm>
{
    
}

public interface IFirmRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}