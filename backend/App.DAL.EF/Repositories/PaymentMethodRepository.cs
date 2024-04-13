using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PaymentMethodRepository : BaseEntityRepository<App.Domain.PaymentMethod, App.DAL.DTO.PaymentMethod, AppDbContext>,
    IPaymentMethodRepository
{
    public PaymentMethodRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.PaymentMethod, App.DAL.DTO.PaymentMethod>(mapper))
    {
    }
}