namespace SharedLibrary.Services.Interfaces
{
    public interface ITokenValidator
    {
        bool IsValid(string token);
    }
}
