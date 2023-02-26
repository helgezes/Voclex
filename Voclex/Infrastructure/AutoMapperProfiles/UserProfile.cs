using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.DataTransferObjects.Authentication;

namespace Infrastructure.AutoMapperProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, IUserDto>().As<UserDto>();


            CreateMap<RegistrationRequest, User>().ConstructUsing((request, context) =>
            {
                var hasher = (IPasswordHasher<User>)context.Items[nameof(IPasswordHasher<User>)];

                return new User(request.Username, request.Password, Role.User, hasher);
            });
        }
    }
}
