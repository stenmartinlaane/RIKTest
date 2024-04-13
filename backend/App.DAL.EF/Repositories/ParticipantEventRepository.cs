using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ParticipantEventRepository : BaseEntityRepository<App.Domain.ParticipantEvent, App.DAL.DTO.ParticipantEvent, AppDbContext>,
    IParticipantEventRepository
{
    public ParticipantEventRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.ParticipantEvent, App.DAL.DTO.ParticipantEvent>(mapper))
    {
    }
}