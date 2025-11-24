using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Employee
    {
        private string _name;
        private string _lastName;
        private int _iD;

        public string Name { get { return _name; } set { _name = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public int ID { get { return _iD; } set { _iD = value; } }


        public Employee() 
        {
            _name = string.Empty;
            _lastName = string.Empty;
            _iD = 0;
        }

        public Employee(int ID,string Name, string LastName)
        {
            _name = Name;
            _lastName = LastName;
            _iD = ID;
        }

        public override string ToString()
        {
            return $"Pracownik: {_name} {_lastName} | ID: {_iD}\n";
        }

    }
}
