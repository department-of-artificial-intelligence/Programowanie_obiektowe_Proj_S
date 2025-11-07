using Project.Model.Abstract;

namespace Project.Model
{
    public record HotelRoom : IContainsResidents, IContainsCurrentResidents
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required int Number { get; set; }
    
        public required int Floor { get; set; }

        public required IEnumerable<Resident> Residents { get; set; }

        public required IEnumerable<RoomHistoricResident> HistoricResidents { get; set; }

        public IEnumerable<IResident> AllResidents => new List<IResident>().Concat(this.Residents).Concat(this.HistoricResidents);
    }
}
