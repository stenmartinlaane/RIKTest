using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IEventService Events { get; }
    iFirmService Firms { get; }
    IParticipantEventService ParticipantEvents { get; }
    IPaymentMethodService PaymentMethods { get; }
    IPersonService Persons { get; }
}