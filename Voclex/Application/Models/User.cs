using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
