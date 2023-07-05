using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Models;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.ModelInterfaces.DtoInterfaces;
using SharedLibrary.Services;
using SharedLibrary.Services.Interfaces;

namespace RazorLibrary.Services.Authentication
{
    public sealed class JwtAuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IUserStorage userStorage;
        private readonly ITokenValidator tokenValidator;
        private IUserDto? currentUser = null;

        public JwtAuthenticatedUserService(IUserStorage userStorage, ITokenValidator tokenValidator)
        {
            this.userStorage = userStorage;
            this.tokenValidator = tokenValidator;
        }

        public async Task<IUserDto?> GetCurrentUser() 
        {
            if (currentUser != null) 
                return currentUser;

            return await GetCurrentUserFromToken();
        }

        private async Task<IUserDto?> GetCurrentUserFromToken()
        {
            var stringToken = await userStorage.GetCurrentUserToken();
            if (stringToken == null) return null;

            if (!tokenValidator.IsValid(stringToken)) return null; //does not validate on the client atm

            var token = ReadJwtToken(stringToken);

            return currentUser = ClaimsUserConverter.ConvertClaimsToUser(token.Claims);
        }

        private static JwtSecurityToken ReadJwtToken(string stringToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(stringToken);
        }
    }
}
