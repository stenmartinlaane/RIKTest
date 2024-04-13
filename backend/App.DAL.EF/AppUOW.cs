using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IEventRepository? _event;
    public IEventRepository Event => _event ?? new EventRepository(UowDbContext, _mapper);
    
    private IFirmRepository? _firm;
    public IFirmRepository Firm => _firm ?? new FirmRepository(UowDbContext, _mapper);

    private IParticipantEventRepository? _participantEvent;
    public IParticipantEventRepository ParticipantEvent => _participantEvent ?? new ParticipantEventRepository(UowDbContext, _mapper);
    
    private IPaymentMethodRepository? _paymentMethod;
    public IPaymentMethodRepository PaymentMethod => _paymentMethod ?? new PaymentMethodRepository(UowDbContext, _mapper);
    
    private IPersonRepository? _person;
    public IPersonRepository Person => _person ?? new PersonRepository(UowDbContext, _mapper);
}