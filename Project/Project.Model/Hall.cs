using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Hall
{
    public int HallId { get; set; }
    public List<Seat> Seats { get; set; } // tworzenie siedzeń poprzez podanie liczby rzędów i miejsc???
    public List<Performance> Performances { get; set; }

    public Hall()
    {
        HallId = 0;
        Seats = new List<Seat>();
        Performances = new List<Performance>();
    }

    public Hall(int hallId)
    {
        HallId = hallId;
        Seats = new List<Seat>();
        Performances = new List<Performance>();
    }

    public bool CreateSeat(int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0 || seatNumber <= 0) return false;
        if (Seats.Any(s => s.RowNumber == rowNumber && s.SeatNumber == seatNumber)) return false;
        Seat seat = new Seat(rowNumber, seatNumber, this);
        Seats.Add(seat);
        return true;
    }
    public bool DeleteSeat(int rowNumber, int seatNumber)
    {
        if (Seats.Count == 0) return false;
        var seat = Seats.FirstOrDefault(t => t.RowNumber == rowNumber && t.SeatNumber == seatNumber);
        if (seat is null) return false;
        return Seats.Remove(seat);
    }
    public void DeleteAllSeats()
    {
        Seats.Clear();
    }

    public bool AddPerformance(Performance performance)
    {
        if (performance is null || Performances.Contains(performance)) return false;
        performance.Hall = this;
        Performances.Add(performance);
        return true;
    }
    public bool RemovePerformance(Performance performance)
    {
        if (Performances.Count == 0 || performance is null) return false;
        performance.Hall = null;
        return Performances.Remove(performance);
    }
    public bool RemovePerformance(int performanceId)
    {
        if (Performances.Count == 0) return false;
        var performance = Performances.FirstOrDefault(p => p.PerformanceId == performanceId);
        if (performance is null) return false;
        performance.Hall = null;
        return Performances.Remove(performance);
    }
    public void RemoveAllPerformances()
    {
        foreach (var performance in Performances)
        {
            performance.Hall = null;
        }
        Performances.Clear();
    }

    public string GetSeats() => Seats.ListToString();

    public string GetPerformances() => Performances.ListToString();

    public override string ToString()
    {
        return $"Sala nr.{HallId}";
    }
}
