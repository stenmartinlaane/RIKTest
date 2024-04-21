using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventRepository : BaseEntityRepository<App.Domain.Event, App.DAL.DTO.Event, AppDbContext>,
    IEventRepository
{
    public EventRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Event, App.DAL.DTO.Event>(mapper))
    {
    }
    
    public override async Task<App.DAL.DTO.Event?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        return Mapper.Map(
            await CreateQuery(noTracking)
                .Include(e => e.ParticipantEvents)
                .ThenInclude(pe => pe.Person)
                .Include(e => e.ParticipantEvents)
                .ThenInclude(pe => pe.Firm)
                .FirstOrDefaultAsync(m => m.Id.Equals(id))
            );
    }
}