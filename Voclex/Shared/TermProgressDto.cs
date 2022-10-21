using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared
{
    public sealed class TermProgressDto
    {
        public TermProgressDto(int termId, int userId)
        {
            TermId = termId;
            UserId = userId;
        }

        public TermProgressDto() { }

        [BindRequired]
        public int TermId { get; set; }

        [BindRequired]
        public int UserId { get; set; }
    }
}
