using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EventRepository : BaseEntityRepository<App.Domain.Event, App.DAL.DTO.Event, AppDbContext>,
    IEventRepository
{
    public EventRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Event, App.DAL.DTO.Event>(mapper))
    {
    }
}