using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PersonRepository : BaseEntityRepository<App.Domain.Person, App.DAL.DTO.Person, AppDbContext>,
    IPersonRepository
{
    public PersonRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Person, App.DAL.DTO.Person>(mapper))
    {
    }
}