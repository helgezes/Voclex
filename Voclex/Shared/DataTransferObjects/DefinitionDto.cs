using Application.ModelInterfaces;
using Application.Models;

namespace Shared.DataTransferObjects
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
