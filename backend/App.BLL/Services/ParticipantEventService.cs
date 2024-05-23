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
    public ParticipantEventService(IAppUnitOfWork uoW, IParticipantEventRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.ParticipantEvent, App.BLL.DTO.ParticipantEvent>(mapper))
    {
        
    }
}