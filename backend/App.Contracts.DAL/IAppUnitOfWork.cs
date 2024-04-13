using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    // list your repos here

    IEventRepository Event { get; }
    IFirmRepository Firm { get; }
    IParticipantEventRepository ParticipantEvent { get; }
    IPaymentMethodRepository PaymentMethod { get; }
    IPersonRepository Person { get; }
}