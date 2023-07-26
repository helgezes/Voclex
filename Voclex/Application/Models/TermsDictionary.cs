namespace Application.Models
{
    public class TermsDictionary : Entity
    {
        public TermsDictionary(string title, User? user = null)
        {
            Title = title;
            UserId = user?.Id;
            User = user;
        }

        public TermsDictionary(string title, int userId)
        {
	        Title = title;
            UserId = userId;
        }

        private TermsDictionary(){}

        public string Title { get; private set; }

        public int? UserId { get; private set; }

        public User? User { get; private set; }
    }
}
