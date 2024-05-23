using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IPaymentMethodService : IEntityRepository<App.BLL.DTO.PaymentMethod>, IPaymentMethodRepositoryCustom<App.BLL.DTO.PaymentMethod>
{
    
}

public interface IPaymentMethodRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}