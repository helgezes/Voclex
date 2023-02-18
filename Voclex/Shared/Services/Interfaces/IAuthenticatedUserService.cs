using Application.ModelInterfaces.DtoInterfaces;

namespace SharedLibrary.Services.Interfaces
{
    public interface IAuthenticatedUserService
    {
        Task<IUserDto?> GetCurrentUser();
    }
}
