using Application.DataAccess;
using Application.Exceptions;
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

        public async Task Register(IRegistrationRequest request)
        {
            var isEmailExists = await context.Users.AnyAsync(u => u.Name == request.Username);

            if (isEmailExists)
                throw new UserExistsException();

            var user = mapper.Map<User>(request, op => op.Items[nameof(IPasswordHasher<User>)] = hasher);

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }
    }
}
