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
        public List<Seat> Seats { get; set; }
        public List<Performance> Performances { get; set; }

        public Hall() : this(default, new List<Seat>(), new List<Performance>()) { }
        public Hall(int hallNumber, List<Seat> seats, List<Performance> performances)
        {
            HallNumber = hallNumber;
            Seats = seats;
            Performances = performances;
        }
    }
}
