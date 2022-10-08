namespace Application.Models
{
    public class Dictionary : Entity
    {
        public Dictionary(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }
    }
}
