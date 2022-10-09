namespace Application.Models
{
    public class DictionaryItem : Entity
    {
        public DictionaryItem(string value, Dictionary dictionary) : this(value)
        {
            Dictionary = dictionary;
            DictionaryId = dictionary.Id;
        }

        private DictionaryItem(string value) 
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public int DictionaryId { get; }

        public Dictionary Dictionary { get; } = null!; //see https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types

        public string Value { get; set; }
    }
}
