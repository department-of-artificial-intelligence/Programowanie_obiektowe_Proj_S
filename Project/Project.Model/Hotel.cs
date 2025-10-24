using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public record Hotel
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required string Address { get; set; }

        public required Manager Manager { get; set; }

        public required IEnumerable<HotelRoom> Rooms { get; set; }
    
        public Hotel() { }

        [SetsRequiredMembers]
        public Hotel(Guid guid, string name, string address, Manager manager, IEnumerable<HotelRoom> rooms)
        {
            Name = name;
            Address = address;
            Manager = manager;
            Rooms = rooms;
            Guid = guid;
        }

        [SetsRequiredMembers]
        public Hotel(string name, string address, Manager manager, IEnumerable<HotelRoom> rooms) : this(Guid.NewGuid(), name, address, manager, rooms) { }
    }
}
