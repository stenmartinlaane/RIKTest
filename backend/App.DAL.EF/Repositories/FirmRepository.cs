using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class FirmRepository : BaseEntityRepository<App.Domain.Firm, App.DAL.DTO.Firm, AppDbContext>,
    IFirmRepository
{
    public FirmRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Firm, App.DAL.DTO.Firm>(mapper))
    {
    }
}