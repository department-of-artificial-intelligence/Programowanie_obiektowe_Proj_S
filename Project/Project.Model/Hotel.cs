using System.ComponentModel.DataAnnotations.Schema;
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

        public required List<HotelRoom> Rooms { get; set; }
        
        [NotMapped]
        public List<IResident> AllResidents => this.Rooms.SelectMany(x => x.AllResidents).ToList();

        [NotMapped]
        public List<Resident> Residents => this.Rooms.SelectMany(x => x.Residents).ToList();

        public Hotel() : base(UlongIdGenerator.GenerateId()) { }

        [SetsRequiredMembers]
        public Hotel(string name, string address, Manager manager, List<HotelRoom> rooms)
            : base(UlongIdGenerator.GenerateId())
            => (this.Name, this.Address, this.Manager, this.Rooms) = (name, address, manager, rooms);

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
