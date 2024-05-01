using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ParticipantEventRepository : BaseEntityRepository<App.Domain.ParticipantEvent, App.DAL.DTO.ParticipantEvent, AppDbContext>,
    IParticipantEventRepository
{

    protected readonly IDalMapper<App.Domain.Firm, App.DAL.DTO.Firm> FrimMapper;
    protected readonly IDalMapper<App.Domain.Person, App.DAL.DTO.Person> PersonMapper;
    
    public ParticipantEventRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.ParticipantEvent, App.DAL.DTO.ParticipantEvent>(mapper))
    {
        FrimMapper = new DalDomainMapper<App.Domain.Firm, App.DAL.DTO.Firm>(mapper);
        PersonMapper = new DalDomainMapper<App.Domain.Person, App.DAL.DTO.Person>(mapper);;
    }
    
    public async Task<IEnumerable<App.DAL.DTO.ParticipantEvent>> GetAllByEventId(Guid EventId, bool noTracking = true)
    {
        return (await CreateQuery(noTracking)
                .Where(pe => pe.EventId == EventId)
                .ToListAsync())
            .Select(pe => Mapper.Map(pe));
    }
    
    public override App.DAL.DTO.ParticipantEvent Add(App.DAL.DTO.ParticipantEvent entity)
    {
        App.Domain.ParticipantEvent domainEntity = Mapper.Map(entity)!;
        DateTime creationTime = DateTime.Now.ToUniversalTime();
        if (domainEntity.Id.ToString() == "00000000-0000-0000-0000-000000000000")
        {
            domainEntity.Id = Guid.NewGuid();
        }
        creationTime = DateTime.Now.ToUniversalTime();
        domainEntity.CreatedAt = creationTime;
        domainEntity.CreatedBy = "api";
        domainEntity.UpdatedAt = creationTime;
        domainEntity.UpdatedBy = "api";
        
        if (entity.Firm != null)
        {
            var firm = FrimMapper.Map(entity.Firm)!;
            if (firm.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                firm.Id = Guid.NewGuid();
                firm.CreatedAt = creationTime;
                firm.CreatedBy = "api";
            }
            firm.UpdatedAt = creationTime;
            firm.UpdatedBy = "api";
            domainEntity.Firm = firm;
            domainEntity.FirmId = firm.Id;
            domainEntity.PersonId = null;
        } else if (entity.Person != null)
        {
            var person = PersonMapper.Map(entity.Person)!;
            if (person.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                person.Id = Guid.NewGuid();
                person.CreatedAt = creationTime;
                person.CreatedBy = "api";
            }
            person.UpdatedAt = creationTime;
            person.UpdatedBy = "api";
            domainEntity.Person = person;
            domainEntity.PersonId = person.Id;
        }
        
        return Mapper.Map(RepoDbSet.Add(domainEntity).Entity)!;
    }
    
    
    public virtual async Task<App.DAL.DTO.ParticipantEvent?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        return Mapper.Map(await CreateQuery(noTracking)
            .Include(pe => pe.Firm)
            .Include(pe => pe.Person)
            .FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
    
    public override App.DAL.DTO.ParticipantEvent Update(App.DAL.DTO.ParticipantEvent entity)
    {
        App.Domain.ParticipantEvent domainEntity = Mapper.Map(entity)!;
        domainEntity.UpdatedAt = DateTime.Now.ToUniversalTime();
        domainEntity.UpdatedBy = "api";
        RepoDbContext.ParticipantEvents.Where(pe => pe.Id == domainEntity.Id)
            .ExecuteUpdate(s => s
                .SetProperty(pe => pe.RegisterDateTime, domainEntity.RegisterDateTime)
                .SetProperty(pe => pe.PersonId, domainEntity.PersonId)
                .SetProperty(pe => pe.FirmId, domainEntity.FirmId)
                .SetProperty(pe => pe.PaymentMethodId, domainEntity.PaymentMethodId)
                .SetProperty(pe => pe.AdditionalNotes, domainEntity.AdditionalNotes)
                .SetProperty(pe => pe.UpdatedAt, domainEntity.UpdatedAt)
                .SetProperty(pe => pe.UpdatedBy, domainEntity.UpdatedBy)
            );

        if (domainEntity.Person != null)
        {
            RepoDbContext.Persons.Where(p => p.Id == domainEntity.PersonId)
                .ExecuteUpdate(s => s
                    .SetProperty(p => p.FirstName, domainEntity.Person.FirstName)
                    .SetProperty(p => p.LastName, domainEntity.Person.LastName)
                    .SetProperty(p => p.PersonalIdentificationNumber, domainEntity.Person.PersonalIdentificationNumber)
                    .SetProperty(p => p.UpdatedAt, domainEntity.UpdatedAt)
                    .SetProperty(p => p.UpdatedBy, domainEntity.UpdatedBy)
                );
        }
        if (domainEntity.Firm != null)
        {
            RepoDbContext.Firms.Where(f => f.Id == domainEntity.FirmId)
                .ExecuteUpdate(s => s
                    .SetProperty(f => f.Name, domainEntity.Firm.Name)
                    .SetProperty(f => f.RegistryCode, domainEntity.Firm.RegistryCode)
                    .SetProperty(f => f.ParticipantCount, domainEntity.Firm.ParticipantCount)
                    .SetProperty(f => f.UpdatedAt, domainEntity.UpdatedAt)
                    .SetProperty(f => f.UpdatedBy, domainEntity.UpdatedBy)
                );
        }

        

        return Mapper.Map(domainEntity)!;
    }
}