using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class ParticipantEventService :
    BaseEntityService<App.DAL.DTO.ParticipantEvent, App.BLL.DTO.ParticipantEvent, IParticipantEventRepository>,
    IParticipantEventService
{
    private readonly IAppUnitOfWork _uow;

    public ParticipantEventService(IAppUnitOfWork uoW, IParticipantEventRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ParticipantEvent, App.BLL.DTO.ParticipantEvent>(mapper))
    {
        _uow = uoW;
    }
    
    public async Task<App.BLL.DTO.ParticipantEvent?> AddParticipantToEventAsync(App.BLL.DTO.ParticipantEvent participantEvent, Guid userId)
    {
        return Mapper.Map(_uow.ParticipantEvent.Add(Mapper.Map(participantEvent)!, userId));
    }

    public async Task<IEnumerable<App.BLL.DTO.ParticipantEvent>?> GetAllByEventId(Guid EventId, Guid userId = default,
        bool noTracking = true)
    {
        return (await _uow.ParticipantEvent.GetAllByEventId(EventId, userId, noTracking)).Select(pe => Mapper.Map(pe));
    }

    public virtual App.BLL.DTO.ParticipantEvent? Update(App.BLL.DTO.ParticipantEvent entity, Guid userId = default)
    {
        return Mapper.Map(_uow.ParticipantEvent.Update(Mapper.Map(entity), userId));
    }
}