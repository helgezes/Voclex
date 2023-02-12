using Application.ModelInterfaces.DtoInterfaces;

namespace Application.Services.Interfaces
{
    public interface IAuthTokenService
    {
        string CreateToken(IUserDto user);
    }
}
