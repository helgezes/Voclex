using System.ComponentModel.DataAnnotations;
using SharedLibrary.Attributes;

namespace SharedLibrary.DataTransferObjects
{
    public sealed class TermProgressDto
    {
        public TermProgressDto(int termId)
        {
            TermId = termId;
        }

        public TermProgressDto() { }

        [Required]
        public int TermId { get; set; }
        
        [UserId]
        public int UserId { get; set; }
    }
}
