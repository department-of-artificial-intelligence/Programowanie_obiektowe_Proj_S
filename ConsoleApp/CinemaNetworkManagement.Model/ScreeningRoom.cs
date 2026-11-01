using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public class ScreeningRoom
    {
        private readonly List<Seat> _seats;
        private readonly List<Screening> _screenings;

        public int Id { get; }
        public string Name { get; set; }
        public Cinema Cinema { get; }

        public int Capacity
        {
            get { return _seats.Count; }
        }

        public List<Seat> Seats
        {
            get { return new List<Seat>(_seats); }
        }

        public List<Screening> Screenings
        {
            get { return new List<Screening>(_screenings); }
        }

        public ScreeningRoom(int id, string name, Cinema cinema)
        {
            Id = id;
            Name = name;
            Cinema = cinema;
            _seats = new List<Seat>();
            _screenings = new List<Screening>();
        }

        
    }
}
