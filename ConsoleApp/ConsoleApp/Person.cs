using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNetworkManagement
{
    public class Person
    {
        int _id;
        string _firstName;
        string _lastName;

        public Person() : this(0, string.Empty, string.Empty){ }

        public Person(int id, string firstName, string lastName)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
        }
        public virtual string GenerateDescription()
        {
            return $"ID: {_id} Imie: {_firstName} Nazwisko: {_lastName}";
        }

        public override string ToString()    
        {
            return GenerateDescription();
        }
    }
}
