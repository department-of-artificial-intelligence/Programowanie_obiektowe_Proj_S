using Project.Model.Abstract;
using Project.Model.Utils;

namespace Project.Model
{
    public record HotelRoom : IdentifiableEntity<ulong>, IContainsResidents, IContainsCurrentResidents
    {
        public required int Number { get; set; }
    
        public required int Floor { get; set; }

        public required IEnumerable<Resident> Residents { get; set; }

        public required IEnumerable<RoomHistoricResident> HistoricResidents { get; set; }

        public IEnumerable<IResident> AllResidents => new List<IResident>().Concat(this.Residents).Concat(this.HistoricResidents);
        
        public HotelRoom() : base(UlongIdGenerator.GenerateId()) { }
        
        public HotelRoom(int number, int floor, IEnumerable<Resident> residents, IEnumerable<RoomHistoricResident> historicResidents)
            : base(UlongIdGenerator.GenerateId())
            => (this.Number, this.Floor, this.Residents, this.HistoricResidents) = (number, floor, residents, historicResidents);
    }
}
