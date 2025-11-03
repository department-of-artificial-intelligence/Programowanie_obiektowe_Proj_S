using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Performance
    {
        public Play Play { get; set; }
        public Hall Hall { get; set; }
        public DateTime ShowTime { get; set; }
        public List<Ticket> Tickets { get; set; }

        public Performance() : this(new Play(), new Hall(), new DateTime(), new List<Ticket>()) { }
        public Performance(Play play, Hall hall, DateTime showTime, List<Ticket> tickets)
        {
            Play = play;
            Hall = hall;
            ShowTime = showTime;
            Tickets = tickets;
        }
    }
}
