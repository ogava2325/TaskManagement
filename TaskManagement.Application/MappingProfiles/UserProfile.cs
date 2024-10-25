using AutoMapper;
using TaskManagement.Application.Features.User.Commands.Register;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    { 
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }
}