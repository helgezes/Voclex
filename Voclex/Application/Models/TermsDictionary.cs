namespace Application.Models
{
    public class TermsDictionary : Entity
    {
        public TermsDictionary(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }
    }
}
