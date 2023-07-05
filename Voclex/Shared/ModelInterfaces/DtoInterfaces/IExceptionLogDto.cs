namespace SharedLibrary.ModelInterfaces.DtoInterfaces;

public interface IExceptionLogDto : IIdentifiable
{
    string Message { get; }
    string? StackTrace { get; }
    string? Source { get; }
    DateTimeOffset TimeOccurred { get; }
    int? UserId { get; }
    string Uri { get; }
    string String { get; }
}