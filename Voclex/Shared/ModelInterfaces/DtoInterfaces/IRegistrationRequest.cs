namespace SharedLibrary.ModelInterfaces.DtoInterfaces;

public interface IRegistrationRequest
{
    string Username { get; }
    string Password { get; }
}