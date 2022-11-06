using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class ExampleDto : ITermRelated, IIdentifiable
    {
        public ExampleDto(int id, int termId, string value)
        {
            Id = id;
            TermId = termId;
            Value = value;
        }

        public int Id { get; init; }

        public int TermId { get; init; }
        
        public string Value { get; init; }
    }
}
