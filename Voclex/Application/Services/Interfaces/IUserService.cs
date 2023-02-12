using Application.ModelInterfaces.DtoInterfaces;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IUserDto?> GetUserByNameAndVerifyPassword(string name, string password);
    }
}
