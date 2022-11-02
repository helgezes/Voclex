using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class ExampleDto : ITermRelated
    {
        public ExampleDto(int termId, string value)
        {
            TermId = termId;
            Value = value;
        }

        public int TermId { get; init; }
        
        public string Value { get; init; }
    }
}
