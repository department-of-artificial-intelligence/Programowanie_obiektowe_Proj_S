using Project.Model.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public record Hotel : IContainsResidents, IContainsCurrentResidents
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required string Address { get; set; }

        public required Manager Manager { get; set; }

        public required IEnumerable<HotelRoom> Rooms { get; set; }

        public IEnumerable<IResident> AllResidents => this.Rooms.SelectMany(x => x.AllResidents);

        public IEnumerable<Resident> Residents => this.Rooms.SelectMany(x => x.Residents);

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
