using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TermProgress : Entity
    {
        public const byte MaximumGuessedTimesCount = 7;

        public TermProgress(User user, Term term) : this()
        {
            UserId = user.Id;
            User = user;
            TermId = term.Id;
            Term = term;
        }

        private TermProgress()
        {
            GuessedTimesCount = 0;
        }

        public int UserId { get; }
        public User User { get; } = null!;

        public int TermId { get; }
        public Term Term { get; } = null!;

        public byte GuessedTimesCount { get; private set; }

        public DateTime LastGuessDateTime { get; private set; }


        public void CorrectGuess()
        {
            if(GuessedTimesCount < MaximumGuessedTimesCount)
                GuessedTimesCount++;

            UpdateLastGuessDateTime();
        }

        public void IncorrectGuess()
        {
            if(GuessedTimesCount > 0)
                GuessedTimesCount--;
        }

        public void AlreadyKnow()
        {
            GuessedTimesCount = MaximumGuessedTimesCount;
        }

        private void UpdateLastGuessDateTime()
        {
            LastGuessDateTime = DateTime.Now;
        }
    }
}
