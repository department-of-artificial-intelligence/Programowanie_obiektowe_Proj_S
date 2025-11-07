using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public abstract class Event
    {
        public int Id { get; set; }
        public Venue Venue { get; set; }
        public DateTime Date { get; set; }

        public int TicketsSold { get; set; } = 0;
    }
}
