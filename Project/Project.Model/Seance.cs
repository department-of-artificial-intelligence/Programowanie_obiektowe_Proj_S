using Project.Entities;
using Project.Utils;

namespace Project.Models
{
    public class Seance : BaseEntity
    {
        public string FilmId { get; private set; }
        public string AuditoriumId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public decimal Price { get; private set; }

        private readonly List<string> _occupiedSeatIds;
        public IReadOnlyList<string> OccupiedSeatIds => _occupiedSeatIds.AsReadOnly();

        public Seance(string filmId, string auditoriumId, DateTime startTime,
            decimal price, uint filmDurationMinutes) : base()
        {
            ValidateSeanceData(filmId, auditoriumId, startTime, price, filmDurationMinutes);

            FilmId = filmId;
            AuditoriumId = auditoriumId;
            StartTime = startTime;
            EndTime = startTime.AddMinutes(filmDurationMinutes);
            Price = price;
            _occupiedSeatIds = [];
        }

        public Seance(string id, string filmId, string auditoriumId, DateTime startTime,
            decimal price, List<string> occupiedSeatIds, DateTime createdAt, DateTime updatedAt,
            uint filmDurationMinutes) : base(id, createdAt, updatedAt)
        {
            ValidateSeanceData(filmId, auditoriumId, startTime, price, filmDurationMinutes);

            FilmId = filmId;
            AuditoriumId = auditoriumId;
            StartTime = startTime;
            EndTime = startTime.AddMinutes(filmDurationMinutes);
            Price = price;
            _occupiedSeatIds = occupiedSeatIds ?? [];
        }

        private static void ValidateSeanceData(string filmId, string auditoriumId, DateTime startTime,
            decimal price, uint filmDurationMinutes)
        {
            if (string.IsNullOrWhiteSpace(filmId))
                throw new ArgumentException("Film ID is required", nameof(filmId));

            if (string.IsNullOrWhiteSpace(auditoriumId))
                throw new ArgumentException("Auditorium ID is required", nameof(auditoriumId));

            if (startTime <= DateTime.Now)
                throw new ArgumentException("Start time must be in the future", nameof(startTime));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0", nameof(price));

            if (filmDurationMinutes < 1)
                throw new ArgumentException("Film duration must be at least 1 minute", nameof(filmDurationMinutes));
        }

        public bool ReserveSeat(string seatId, uint auditoriumCapacity)
        {
            if (_occupiedSeatIds.Count >= auditoriumCapacity)
                return false;

            if (CollectionHelper.AddUniqueItem(_occupiedSeatIds, seatId))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public bool CancelSeatReservation(string seatId)
        {
            if (CollectionHelper.RemoveItem(_occupiedSeatIds, seatId))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public int AvailableSeats(uint auditoriumCapacity) =>
            (int)auditoriumCapacity - _occupiedSeatIds.Count;

        public void UpdateTime(DateTime newStartTime, uint filmDurationMinutes)
        {
            if (newStartTime <= DateTime.Now)
                throw new ArgumentException("Start time must be in the future");

            StartTime = newStartTime;
            EndTime = newStartTime.AddMinutes(filmDurationMinutes);

            MarkAsUpdated();
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be greater than 0");

            Price = newPrice;
            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Seance: {StartTime:g} - {EndTime:t}\n" +
                   $"Film: {FilmId} | Auditorium: {AuditoriumId}\n" +
                   $"Price: {Price:C} | Occupied Seats: {_occupiedSeatIds.Count}\n" +
                   $"ID: {Id}";
        }
    }
}
