using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class DefinitionDto : ITermRelated, IIdentifiable
    {
        public DefinitionDto(int id, int termId, string value)
        {
            Id = id;
            TermId = termId;
            Value = value;
        }

        public int Id { get; init; }

        public int TermId { get; init; }
        
        public string Value { get; set; }
    }
}
