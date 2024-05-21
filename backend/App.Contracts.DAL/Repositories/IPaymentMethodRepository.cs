using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IPaymentMethodRepository: IEntityRepository<App.DAL.DTO.PaymentMethod>, IPaymentMethodRepositoryCustom<App.DAL.DTO.PaymentMethod>
{
    
}

public interface IPaymentMethodRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}