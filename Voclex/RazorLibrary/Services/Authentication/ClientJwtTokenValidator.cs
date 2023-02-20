using SharedLibrary.Services.Interfaces;

namespace RazorLibrary.Services.Authentication
{
    public sealed class ClientJwtTokenValidator : ITokenValidator
    {
        public bool IsValid(string token)
        {
            return true;
            //todo do we need to validate the token on the client? I dont think it is necessary atm
        }
    }
}
