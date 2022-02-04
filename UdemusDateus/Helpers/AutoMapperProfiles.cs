using AutoMapper;
using UdemusDateus.DTOs;
using UdemusDateus.Entities;
using UdemusDateus.Extensions;

namespace UdemusDateus.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt =>
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)).ForMember(dest => dest.Age, opt =>
                opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

        CreateMap<Photo, PhotoDto>();

        CreateMap<MemberUpdateDto, AppUser>();

        CreateMap<RegisterDto, AppUser>();

        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderPhotoUrl, opt 
                => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.RecipientPhotoUrl, opt 
            => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        
        CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
    }
}