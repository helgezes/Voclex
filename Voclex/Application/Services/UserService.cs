using Application.DataAccess;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IDbContext context;
        private readonly IPasswordHasher<User> hasher;
        private readonly IMapper mapper;

        public UserService(IDbContext context, IPasswordHasher<User> hasher, IMapper mapper)
        {
            this.context = context;
            this.hasher = hasher;
            this.mapper = mapper;
        }

        public async Task<IUserDto?> GetUserByNameAndVerifyPassword(string name, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == name);
            if (user == null) return null;

            if (!user.VerifyPassword(password, hasher)) return null;

            return mapper.Map<IUserDto>(user);
        }
    }
}
