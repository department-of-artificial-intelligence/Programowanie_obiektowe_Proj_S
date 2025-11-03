using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Customer : Person
    {
        public List<Ticket> Tickets { get; set; }

        public Customer() : base(string.Empty, string.Empty)
        {
            Tickets = new List<Ticket>();
        }
        public Customer(string firstName, string lastName, List<Ticket> tickets) : base(firstName, lastName)
        {
            Tickets = tickets;
        }
    }
}
