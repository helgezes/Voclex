namespace Application.Models
{
    public class Definition : Entity
    {
        public Definition(string value, DictionaryItem dictionaryItem) : this(value)
        {
            DictionaryItem = dictionaryItem;
            DictionaryItemId = dictionaryItem.Id;
        }

        private Definition(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public int DictionaryItemId { get; }

        public DictionaryItem DictionaryItem { get; } = null!;

        public string Value { get; set; }
    }
}
