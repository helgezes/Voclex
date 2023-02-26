namespace Application.ModelInterfaces.DtoInterfaces;

public interface IRegistrationRequest
{
    string Username { get; init; }
    string Password { get; init; }
}