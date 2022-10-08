namespace Application.Models
{
    public class Definition : Entity
    {
        public Definition(int dictionaryItemId, string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
            DictionaryItemId = dictionaryItemId;
        }

        public int DictionaryItemId { get; private set; }

        public string Value { get; private set; }
    }
}
