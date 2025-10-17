using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Author
    {
        public int _id { get; set; }

        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;

        public DateTime birthDay { get; set; }

        public Author()
        {

        }

        public Author(int id, string firstName, string lastName, DateTime birthDay)
        {
            this._id = id;
            FirstName = firstName;
            LastName = lastName;
            this.birthDay = birthDay;
        }

        public override string ToString()
        {
            return String.Format("Author {0} {1} {2} with id: ", FirstName, LastName, birthDay, _id);
        }
    }
}
