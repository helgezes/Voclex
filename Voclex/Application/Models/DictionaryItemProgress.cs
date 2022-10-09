﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DictionaryItemProgress : Entity
    {
        public DictionaryItemProgress(User user, DictionaryItem dictionaryItem) : this()
        {
            UserId = user.Id;
            User = user;
            DictionaryItemId = dictionaryItem.Id;
            DictionaryItem = dictionaryItem;
        }

        private DictionaryItemProgress()
        {
            GuessedTimesCount = 1;
        }

        public int UserId { get; }
        public User User { get; } = null!;

        public int DictionaryItemId { get; }
        public DictionaryItem DictionaryItem { get; } = null!;

        public byte GuessedTimesCount { get; private set; }
    }
}
