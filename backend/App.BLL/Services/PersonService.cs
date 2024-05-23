using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class PersonService :
    BaseEntityService<App.DAL.DTO.Person, App.BLL.DTO.Person, IPersonRepository>,
    IPersonService
{
    public PersonService(IAppUnitOfWork uoW, IPersonRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Person, App.BLL.DTO.Person>(mapper))
    {
        
    }
}