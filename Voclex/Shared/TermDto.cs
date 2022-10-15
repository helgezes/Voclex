namespace Shared
{
    public sealed class TermDto : Dto
    {
        public TermDto(int id, string value, int termsDictionaryId) : base(id)
        {
            Value = value;
            TermsDictionaryId = termsDictionaryId;
        }

        public int TermsDictionaryId { get; }

        public string Value { get; set; } //todo validation attributes
    }
}
