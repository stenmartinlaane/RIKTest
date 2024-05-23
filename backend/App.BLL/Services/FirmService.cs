using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class FirmService :
    BaseEntityService<App.DAL.DTO.Firm, App.BLL.DTO.Firm, IFirmRepository>,
    iFirmService
{
    public FirmService(IAppUnitOfWork uoW, IFirmRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Firm, App.BLL.DTO.Firm>(mapper))
    {
        
    }
}