using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared
{
    public sealed class TermProgressDto
    {
        [BindRequired]
        public int TermId { get; set; }

        [BindRequired]
        public int UserId { get; set; }
    }
}
