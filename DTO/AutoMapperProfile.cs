using AutoMapper;
using BonsaiBackend.DTO;
using BonsaiBackend.Models;
using System;
public class AutoMapperProfile: Profile {
    public AutoMapperProfile() {
        CreateMap<UserDto, Users>()
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}