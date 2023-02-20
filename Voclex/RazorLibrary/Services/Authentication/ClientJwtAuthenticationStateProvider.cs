using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using SharedLibrary.Services.Interfaces;

namespace RazorLibrary.Services.Authentication
{
    public sealed class ClientJwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IUserStorage userStorage;

        public ClientJwtAuthenticationStateProvider(IUserStorage userStorage)
        {
            this.userStorage = userStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await userStorage.GetCurrentUserToken();
            if (stringToken == null) return new AuthenticationState(new ClaimsPrincipal());

            //does not validate on the client atm

            var token = ReadJwtToken(stringToken);
            
            return new AuthenticationState(
                new ClaimsPrincipal(
                new ClaimsIdentity(
                    token.Claims.Select(c => new Claim(c.Type, c.Value)), "jwt"
                    )
                )
                );
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static JwtSecurityToken ReadJwtToken(string stringToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(stringToken);
        }
    }
}
