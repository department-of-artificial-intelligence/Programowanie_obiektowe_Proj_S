using Project.Model.Abstract;
using System.Diagnostics.CodeAnalysis;
using Project.Model.Utils;

namespace Project.Model
{
    public record Hotel : IdentifiableEntity<ulong>, IContainsResidents, IContainsCurrentResidents
    {
        public required string Name { get; set; }

        public required string Address { get; set; }

        public required Manager Manager { get; set; }

        public required IEnumerable<HotelRoom> Rooms { get; set; }

        public IEnumerable<IResident> AllResidents => this.Rooms.SelectMany(x => x.AllResidents);

        public IEnumerable<Resident> Residents => this.Rooms.SelectMany(x => x.Residents);

        public Hotel() : base(UlongIdGenerator.GenerateId()) { }

        [SetsRequiredMembers]
        public Hotel(string name, string address, Manager manager, IEnumerable<HotelRoom> rooms)
            : base(UlongIdGenerator.GenerateId())
            => (this.Name, this.Address, this.Manager, this.Rooms) = (name, address, manager, rooms);
    }
}
