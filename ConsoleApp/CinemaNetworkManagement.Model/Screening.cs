using System;
using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public class Screening
    {
        private readonly List<Ticket> _tickets;

        public int Id { get; }
        public Movie Movie { get; }
        public ScreeningRoom Room { get; }
        public DateTime StartTime { get; }
        public decimal BasePrice { get; }
        public bool IsCancelled { get; private set; }

        public DateTime EndTime
        {
            get { return StartTime.AddMinutes(Movie.DurationMinutes); }
        }

        public List<Ticket> Tickets
        {
            get { return new List<Ticket>(_tickets); }
        }

        public Screening(int id, Movie movie, ScreeningRoom room, DateTime startTime, decimal basePrice)
        {
            Id = id;
            Movie = movie;
            Room = room;
            StartTime = startTime;
            BasePrice = basePrice;
            IsCancelled = false;
            _tickets = new List<Ticket>();
        }

    }
}
