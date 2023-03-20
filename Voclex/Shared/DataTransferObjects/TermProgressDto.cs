using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Attributes;
using SharedLibrary.Binders;

namespace SharedLibrary.DataTransferObjects
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
        
        [UserId]
        public int UserId { get; set; }
    }
}
