using Application.ModelInterfaces.DtoInterfaces;
using SharedLibrary.Attributes;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class ExceptionLogDto : Dto, IExceptionLogDto
    {
        public ExceptionLogDto(
            string message,
            string stackTrace,
            string source,
            string uri,
            string s) : base(0)
        {
            Message = message;
            StackTrace = stackTrace;
            Source = source;
            Uri = uri;
            String = s;
        }

        public ExceptionLogDto() : base(0) { }

        public string Message { get; init; }

        public string? StackTrace { get; init; }

        public string? Source { get; init; }

        public DateTimeOffset TimeOccurred { get; init; }

        [UserId]
        public int? UserId { get; set; }

        public string Uri { get; init; }

        public string String { get; init; }
    }
}
