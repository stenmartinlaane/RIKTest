using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using App.BLL.DTO;
using App.Contracts.DAL.Repositories;

namespace App.BLL.Services;

public class EventService :
    BaseEntityService<App.DAL.DTO.Event, App.BLL.DTO.Event, IEventRepository>,
    IEventService
{
    public EventService(IAppUnitOfWork uoW, IEventRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Event, App.BLL.DTO.Event>(mapper))
    {
        
    }
    
}