using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class DefinitionDto : ITermRelatedEditable, IIdentifiable
    {
        public DefinitionDto(int id, int termId, string value)
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
