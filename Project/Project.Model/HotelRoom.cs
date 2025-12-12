using System.ComponentModel.DataAnnotations.Schema;
using Project.Model.Abstract;
using Project.Model.Utils;

namespace Project.Model
{
    public record HotelRoom : IdentifiableEntity<ulong>, IContainsResidents, IContainsCurrentResidents
    {
        public required int Number { get; set; }
    
        public required int Floor { get; set; }

        public required decimal PricePerDay { get; set; }

        public required List<Resident> Residents { get; set; }

        public required List<RoomHistoricResident> HistoricResidents { get; set; }

        [NotMapped]
        public bool IsReserved => this.Residents.Any();

        [NotMapped]
        public List<IResident> AllResidents => new List<IResident>()
                .Concat(this.Residents)
                .Concat(this.HistoricResidents)
                .ToList();
        
        public HotelRoom() : base(UlongIdGenerator.GenerateId()) { }
        
        public HotelRoom(int number, int floor, decimal pricePerDay, List<Resident> residents, List<RoomHistoricResident> historicResidents)
            : base(UlongIdGenerator.GenerateId())
            => (this.Number, this.Floor, this.PricePerDay, this.Residents, this.HistoricResidents) = (number, floor, pricePerDay, residents, historicResidents);
    }
}
