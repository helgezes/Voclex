namespace Application.Models
{
    public class DictionaryItem : Entity
    {
        public DictionaryItem(int dictionaryId, string value)
        {
            if (string.IsNullOrEmpty(value)) 
                throw new ArgumentNullException(nameof(value));

            DictionaryId = dictionaryId;
            Value = value;
        }

        public int DictionaryId { get; private set; }

        public string Value { get; set; }
    }
}
