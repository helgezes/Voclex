namespace Application.Models
{
    public class ExceptionLog : Entity
    {
        public ExceptionLog(
            string message, 
            string? stackTrace,
            string? source, 
            DateTimeOffset timeOccurred, 
            int? userId, 
            string uri, 
            string s)
        {
            Message = message;
            StackTrace = stackTrace;
            Source = source;
            TimeOccurred = timeOccurred;
            UserId = userId;
            Uri = uri;
            String = s;
        }

        private ExceptionLog(){}

        public string Message { get; init; }
        public string? StackTrace { get; init; }
        public string? Source { get; init; }
        public DateTimeOffset TimeOccurred { get; init; }
        public int? UserId { get; init; }
        public string Uri { get; init; }
        public string String { get; init; }
    }

}
