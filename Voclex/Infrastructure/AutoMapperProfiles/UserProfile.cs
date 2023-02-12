using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using AutoMapper;
using SharedLibrary.DataTransferObjects;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, IUserDto>().As<UserDto>();
        }
    }
}
