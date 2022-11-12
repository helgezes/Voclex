using Application.ModelInterfaces;

namespace Application.Models
{
    public class Picture : Entity, ITermRelated
    {
        public Picture(string path, Term term) : this(path)
        {
            Term = term;
            TermId = term.Id;
        }

        private Picture(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            Path = path;
        }

        public int TermId { get; init; }

        public Term Term { get; } = null!;

        public string Path { get; set; } //we can store it as byte[], but with gifs (they can be very large) that would probably make our db pretty big as well
    }
}
