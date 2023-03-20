using Application.ModelInterfaces.DtoInterfaces;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Services;
using SharedLibrary.Services.Interfaces;

namespace Infrastructure.Services;

public sealed class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private IUserDto? currentUser = null;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IUserDto?> GetCurrentUser()
    {
        if (currentUser != null)
            return currentUser;

        return await GetCurrentUserFromHttpContext();
    }

    private Task<IUserDto?> GetCurrentUserFromHttpContext()
    {
        var claims = httpContextAccessor.HttpContext?.User.Claims.ToArray();
        if (claims == null || !claims.Any())
        {
            return Task.FromResult((IUserDto?)null);
        }

        currentUser =
            ClaimsUserConverter.ConvertClaimsToUser(claims);

        return Task.FromResult((IUserDto?) currentUser);
    }
}