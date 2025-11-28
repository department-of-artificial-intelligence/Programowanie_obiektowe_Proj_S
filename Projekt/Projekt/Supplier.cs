using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }

        public Supplier(int id, string name, string adres, string email)
        {
            Id = id;
            Name = name;
            Adres = Adres;
            Email = Email;
        }
        public Supplier()
        {
            Id = 0;
            Name = "";
            Adres = "";
            Email = "";
        }


        public override string ToString()
        {
            return $"{Id} {Name} {Adres} {Email}";
        }

    }

}