using Application.ModelInterfaces;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class DefinitionDto : ITermRelated
    {
        public DefinitionDto(int termId, string value)
        {
            TermId = termId;
            Value = value;
        }

        public int TermId { get; init; }
        
        public string Value { get; init; }
    }
}
