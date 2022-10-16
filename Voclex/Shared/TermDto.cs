using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared
{
    public sealed class TermDto : Dto
    {
        public TermDto(int id, string value, int termsDictionaryId) : base(id)
        {
            Value = value;
            TermsDictionaryId = termsDictionaryId;
        }

        [BindRequired]
        public int TermsDictionaryId { get; set; }

        [BindRequired]
        public string Value { get; set; }
    }
}
