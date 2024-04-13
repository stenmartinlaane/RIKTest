using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Event, App.DAL.DTO.Event>().ReverseMap();
        CreateMap<App.Domain.Firm, App.DAL.DTO.Firm>().ReverseMap();
        CreateMap<App.Domain.ParticipantEvent, App.DAL.DTO.ParticipantEvent>().ReverseMap();
        CreateMap<App.Domain.PaymentMethod, App.DAL.DTO.PaymentMethod>().ReverseMap();
        CreateMap<App.Domain.Person, App.DAL.DTO.Person>().ReverseMap();
    }
}