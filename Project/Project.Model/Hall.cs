using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Hall
    {
        public int HallNumber { get; set; }
        public List<Seat> seats { get; set; }
        public List<Performance> performances { get; set; }

        public Hall() : this(default, new List<Seat>(), new List<Performance>()) { }
        public Hall(int hallNumber, List<Seat> seats, List<Performance> performances)
        {
            HallNumber = hallNumber;
            this.seats = seats;
            this.performances = performances;
        }
    }
}
