using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Theater
    {
        public string Name { get; set; } // czy potrzebne skoro ta sama sieć?
        public Address Address { get; set; }
        public List<Hall> Halls { get; set; }

        public Theater() : this(string.Empty, new Address(), new List<Hall>()) { }
        public Theater(string name, Address address, List<Hall> halls)
        {
            Name = name;
            Address = address;
            Halls = halls ?? new List<Hall>();
        }

        public bool AddHall(Hall hall)
        {
            if (hall is null) return false;
            Halls.Add(hall);
            return true;
        }
        public bool AddTheater(int hallNumber, List<Seat> seats, List<Performance> performances) //todo
        {
            if (hallNumber <= 0 || seats is null || performances is null) return false;
            Hall hall = new Hall(hallNumber, seats, performances);
            Halls.Add(hall);
            return true;
        }
    }
}
