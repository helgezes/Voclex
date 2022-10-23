
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public sealed class TermProgressDto
    {
        public TermProgressDto(int termId, int userId)
        {
            TermId = termId;
            UserId = userId;
        }

        public TermProgressDto() { }

        [Required]
        public int TermId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
