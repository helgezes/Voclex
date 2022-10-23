
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public sealed class TermDto : Dto
    {
        public TermDto(int id, string value, int termsDictionaryId) : base(id)
        {
            Value = value;
            TermsDictionaryId = termsDictionaryId;
        }

        [Required]
        public int TermsDictionaryId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
