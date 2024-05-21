using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class FirmRepository : BaseEntityRepository<App.Domain.Firm, App.DAL.DTO.Firm, AppDbContext>,
    IFirmRepository
{
    public FirmRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Firm, App.DAL.DTO.Firm>(mapper))
    {
    }
    
}