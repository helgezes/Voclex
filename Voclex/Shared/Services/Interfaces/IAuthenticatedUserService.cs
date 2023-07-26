using System.Security.Authentication;
using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace SharedLibrary.Services.Interfaces
{
    public interface IAuthenticatedUserService
    {
        Task<IUserDto?> GetCurrentUser();
        async Task<IUserDto> GetCurrentUserOrThrowIfNull()
        {
	        var user = await GetCurrentUser();

	        if (user == null)
		        throw new AuthenticationException();

	        return user;
        }
	}
}
