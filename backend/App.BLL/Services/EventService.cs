using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Event = App.DAL.DTO.Event;

namespace App.BLL.Services;

public class EventService :
    BaseEntityService<App.DAL.DTO.Event, App.BLL.DTO.Event, IEventRepository>,
    IEventService
{
    private readonly IAppUnitOfWork _uow;
    
    public EventService(IAppUnitOfWork uoW, IEventRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Event, App.BLL.DTO.Event>(mapper))
    {
        _uow = uoW;
    }
    
}