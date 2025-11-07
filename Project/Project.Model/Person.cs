using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

       

        public override string ToString()
        {
            string _tickets = Tickets.Count > 0 ? String.Join("\n",Tickets.Select(t => t.ToString())) : "Brak biletów";
            return $"{FirstName} {LastName} {Age}, Bilety:\n{_tickets}";
        }

    }
}
