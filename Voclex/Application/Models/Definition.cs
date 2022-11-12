using Application.ModelInterfaces;

namespace Application.Models
{
    public class Definition : Entity, ITermRelated
    {
        public Definition(string value, Term term) : this(value)
        {
            Term = term;
            TermId = term.Id;
        }

        private Definition(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public int TermId { get; init; }

        public Term Term { get; } = null!;

        public string Value { get; set; }
    }
}
