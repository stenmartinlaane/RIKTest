using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.Event, App.BLL.DTO.Event>().ReverseMap();
        CreateMap<App.DAL.DTO.Firm, App.BLL.DTO.Firm>().ReverseMap();
        CreateMap<App.DAL.DTO.ParticipantEvent, App.BLL.DTO.ParticipantEvent>().ReverseMap();
        CreateMap<App.DAL.DTO.PaymentMethod, App.BLL.DTO.PaymentMethod>().ReverseMap();
        CreateMap<App.DAL.DTO.Person, App.BLL.DTO.Person>().ReverseMap();
    }
}