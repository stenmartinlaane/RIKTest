using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.Event, App.DTO.v1_0.Event>().ReverseMap();
        CreateMap<App.BLL.DTO.Firm, App.DTO.v1_0.Firm>().ReverseMap();
        CreateMap<App.BLL.DTO.ParticipantEvent, App.DTO.v1_0.ParticipantEvent>().ReverseMap();
        CreateMap<App.BLL.DTO.PaymentMethod, App.DTO.v1_0.PaymentMethod>().ReverseMap();
        CreateMap<App.BLL.DTO.Person, App.DTO.v1_0.Person>().ReverseMap();
    }
}