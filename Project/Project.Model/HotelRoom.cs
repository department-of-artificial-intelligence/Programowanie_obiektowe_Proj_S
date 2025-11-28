using Project.Model.Abstract;
using Project.Model.Utils;

namespace Project.Model
{
    public record HotelRoom : IdentifiableEntity<ulong>, IContainsResidents, IContainsCurrentResidents
    {
        public required int Number { get; set; }
    
        public required int Floor { get; set; }

        public required decimal PricePerDay { get; set; }

        public required IEnumerable<Resident> Residents { get; set; }

        public required IEnumerable<RoomHistoricResident> HistoricResidents { get; set; }

        public bool IsReserved => this.Residents.Any();

        public IEnumerable<IResident> AllResidents => new List<IResident>().Concat(this.Residents).Concat(this.HistoricResidents);
        
        public HotelRoom() : base(UlongIdGenerator.GenerateId()) { }
        
        public HotelRoom(int number, int floor, decimal pricePerDay, IEnumerable<Resident> residents, IEnumerable<RoomHistoricResident> historicResidents)
            : base(UlongIdGenerator.GenerateId())
            => (this.Number, this.Floor, this.PricePerDay, this.Residents, this.HistoricResidents) = (number, floor, pricePerDay, residents, historicResidents);
    }
}
