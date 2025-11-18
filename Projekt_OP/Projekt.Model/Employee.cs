using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Employee
    {
        public string _name;
        public string _lastName;
        public int _iD;

        public string Name { get { return _name; } set { _name = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public int ID { get { return _iD; } set { _iD = value; } }




        public override string ToString()
        {
            return $"Pracownik: {_name} {_lastName} - {_iD}\n";
        }

    }
}
