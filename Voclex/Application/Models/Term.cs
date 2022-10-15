namespace Application.Models
{
    public class Term : Entity
    {
        public Term(string value, TermsDictionary termsDictionary) : this(termsDictionary.Id, value)
        {
            TermsDictionary = termsDictionary;
        }

        private Term(int termsDictionaryId, string value) : this(value)
        {
            TermsDictionaryId = termsDictionaryId;
        }

        private Term(string value) 
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public int TermsDictionaryId { get; }

        public TermsDictionary TermsDictionary { get; } = null!; //see https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types

        public string Value { get; set; }
    }
}
