namespace Application.Models
{
    public class User : Entity
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
