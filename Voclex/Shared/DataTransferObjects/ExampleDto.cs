using SharedLibrary.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class ExampleDto : ITermRelatedEditable, IIdentifiable
    {
        public ExampleDto(int id, int termId, string value)
        {
            Id = id;
            TermId = termId;
            Value = value;
        }

        public int Id { get; init; }

        public int TermId { get; set; }
        
        public string Value { get; set; }
    }
}
