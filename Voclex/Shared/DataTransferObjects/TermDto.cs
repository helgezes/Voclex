
using System.ComponentModel.DataAnnotations;
using SharedLibrary.Attributes;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class TermDto : Dto
    {
        public TermDto(int id, string value, int termsDictionaryId) : base(id)
        {
            Value = value;
            TermsDictionaryId = termsDictionaryId;
        }

        [Required]
        [DictionaryIdValidation]
        public int TermsDictionaryId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
