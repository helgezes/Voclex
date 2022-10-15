namespace Shared
{
    public sealed class TermDto : Dto
    {
        public TermDto(int id, string value) : base(id)
        {
            Value = value;
        }
        
        public string Value { get; set; }
    }
}
