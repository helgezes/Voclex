using SharedLibrary.Enums;

namespace SharedLibrary.ModelInterfaces.DtoInterfaces
{
    public interface IUserDto : IIdentifiable
    {
        string Name { get; }

        Role Role { get; }
    }
}
