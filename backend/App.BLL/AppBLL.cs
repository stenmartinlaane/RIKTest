using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL: BaseBLL<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
    
    public AppBLL(IAppUnitOfWork uoW, IMapper mapper) : base(uoW)
    {
        _mapper = mapper;
        _uow = uoW;
    }

    private IEventService? _events;
    public IEventService Events => _events ?? new EventService(_uow, _uow.Event, _mapper);
    private iFirmService? _firms;
    public iFirmService? Firms => _firms ?? new FirmService(_uow, _uow.Firm, _mapper);
    private IParticipantEventService? _participantEvents;
    public IParticipantEventService? ParticipantEvents => _participantEvents ?? new ParticipantEventService(_uow, _uow.ParticipantEvent, _mapper);
    private IPaymentMethodService? _paymentMethods;
    public IPaymentMethodService? PaymentMethods => _paymentMethods ?? new PaymentMethodService(_uow, _uow.PaymentMethod, _mapper);
    private IPersonService? _persons;
    public IPersonService? Persons => _persons ?? new PersonService(_uow, _uow.Person, _mapper);
}