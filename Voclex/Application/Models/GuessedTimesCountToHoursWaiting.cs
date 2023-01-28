namespace Application.Models
{
    public sealed class GuessedTimesCountToHoursWaiting //todo mb make this user-customizable
    {
        public GuessedTimesCountToHoursWaiting(byte guessedTimesCount, ushort hoursWaiting) : this()
        {
            GuessedTimesCount = guessedTimesCount;
            HoursWaiting = hoursWaiting;
        }

        private GuessedTimesCountToHoursWaiting()
        { }

        public byte GuessedTimesCount { get; set; }

        public ushort HoursWaiting { get; set; }
    }
}
