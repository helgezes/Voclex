namespace Application.Models
{
    public class DictionaryItem : Entity
    {
        public DictionaryItem(string value, TermsDictionary termsDictionary) : this(value)
        {
            TermsDictionary = termsDictionary;
            TermsDictionaryId = termsDictionary.Id;
        }

        private DictionaryItem(string value) 
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
