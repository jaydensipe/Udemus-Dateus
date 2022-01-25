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
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain))).ForMember(dest => dest.Age, opt =>
                opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        
        CreateMap<Photo, PhotoDto>();
    }
}