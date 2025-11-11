using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Hall
    {
        public int HallId { get; set; }
        public List<Seat> Seats { get; private set; }
        public List<Performance> Performances { get; private set; }

        public Hall(int hallId)
        {
            HallId = hallId;
            Seats = new List<Seat>();
            Performances = new List<Performance>();
        }

        public bool AddSeat(int rowNumber, int seatNumber)
        {
            if (rowNumber <= 0 || seatNumber <= 0) return false;
            Seat seat = new Seat(rowNumber, seatNumber);
            Seats.Add(seat);
            return true;
        }
        public bool DeleteSeat(int rowNumber, int seatNumber)
        {
            if (Seats.Count == 0 || rowNumber <= 0 || seatNumber <= 0) return false;
            var hall = Seats.FirstOrDefault(t => t.RowNumber == rowNumber && t.SeatNumber == seatNumber);
            if (hall is null) return false;
            return Seats.Remove(hall);
        }
        public void DeleteAllSeats()
        {
            Seats.Clear();
        }

        public bool AddPerformance(Performance performance)
        {
            if (performance is null || Performances.Contains(performance)) return false;
            Performances.Add(performance);
            return true;
        }
        public bool RemovePerformance(Performance performance)
        {
            if (Performances.Count == 0 || performance is null) return false;
            return Performances.Remove(performance);
        }
        public void RemoveAllPerformances()
        {
            Performances.Clear();
        }
    }
}
