using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DictionaryItemProgress : Entity
    {
        public DictionaryItemProgress(int userId, int dictionaryItemId)
        {
            UserId = userId;
            DictionaryItemId = dictionaryItemId;
            GuessedTimesCount = 1;
        }

        public int UserId { get; private set; }

        public int DictionaryItemId { get; private set; }

        public byte GuessedTimesCount { get; private set; }
    }
}
