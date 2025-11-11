using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Performance
    {
        public int PerformanceId { get; set; }
        public Play Play { get; set; }
        public Hall Hall { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public PerformanceStatus Status { get; set; }
        public List<Ticket> Tickets { get; set; }

        //public Performance() : this(default, new Play(), new Hall(), new DateTime(), new DateTime(), new List<Ticket>()) { }
        public Performance(int performanceId, Play play, Hall hall, DateTime startTime, DateTime endTime, PerformanceStatus status, List<Ticket> tickets)
        {
            PerformanceId = performanceId;
            Play = play;
            Hall = hall;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
            Tickets = tickets;
        }
    }
}
