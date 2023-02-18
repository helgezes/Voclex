using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.ModelInterfaces.DtoInterfaces;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.Services.Interfaces;

namespace SharedLibrary.Services
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

            return currentUser = ConvertTokenClaimsToUser(token);
        }

        private static JwtSecurityToken ReadJwtToken(string stringToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(stringToken);
        }

        private static IUserDto ConvertTokenClaimsToUser(JwtSecurityToken token)
        {
            var userId = int.Parse(token.Claims.First(c => c.Type == "id").Value);
            var userName = token.Claims.First(c => c.Type == "name").Value;

            return new UserDto(userId, userName);
        }
    }
}
