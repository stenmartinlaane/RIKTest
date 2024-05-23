using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class PaymentMethodService :
    BaseEntityService<App.DAL.DTO.PaymentMethod, App.BLL.DTO.PaymentMethod, IPaymentMethodRepository>,
    IPaymentMethodService
{
    public PaymentMethodService(IAppUnitOfWork uoW, IPaymentMethodRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.PaymentMethod, App.BLL.DTO.PaymentMethod>(mapper))
    {
        
    }
}