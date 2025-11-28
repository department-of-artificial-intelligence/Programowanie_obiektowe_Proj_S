using Project.Interfaces;
using Project.Models.Common;
using Project.Utils;

namespace Project.Models
{
    public class Auditorium : Base, IRatable, IListManageable<string>
    {
        public string CinemaId { get; private set; }
        public string Name { get; private set; }
        public uint RoomNumber { get; private set; }
        public uint Rows { get; private set; }
        public uint SeatsPerRow { get; private set; }
        public uint Capacity => Rows * SeatsPerRow;

        public double Rating { get; private set; }
        public uint TotalRatings { get; private set; }

        private readonly List<string> _features;
        public IReadOnlyList<string> Features => _features.AsReadOnly();
        public IReadOnlyList<string> Items => Features;

        public Auditorium(string cinemaId, string name, uint roomNumber, uint rows, uint seatsPerRow) : base()
        {
            ValidateAuditoriumData(cinemaId, name, roomNumber, rows, seatsPerRow);

            CinemaId = cinemaId;
            Name = name;
            RoomNumber = roomNumber;
            Rows = rows;
            SeatsPerRow = seatsPerRow;
            _features = [];
        }

        public Auditorium(string id, string cinemaId, string name, uint roomNumber,
            uint rows, uint seatsPerRow, List<string> features, double rating,
            uint totalRatings, DateTime createdAt, DateTime updatedAt)
            : base(id, createdAt, updatedAt)
        {
            ValidateAuditoriumData(cinemaId, name, roomNumber, rows, seatsPerRow);

            CinemaId = cinemaId;
            Name = name;
            RoomNumber = roomNumber;
            Rows = rows;
            SeatsPerRow = seatsPerRow;
            _features = features ?? [];
            Rating = rating;
            TotalRatings = totalRatings;
        }

        private static void ValidateAuditoriumData(string cinemaId, string name, uint roomNumber, uint rows, uint seatsPerRow)
        {
            if (string.IsNullOrWhiteSpace(cinemaId))
                throw new ArgumentException("Cinema ID is required", nameof(cinemaId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Auditorium name is required", nameof(name));

            if(roomNumber == 0)
                throw new ArgumentException("Room number cannot be 0", nameof(name));

            if (rows == 0 || seatsPerRow == 0)
                throw new ArgumentException("Rows and seats per row must be greater than 0");
        }

        public void AddRating(uint rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            Rating = RatingCalculator.CalculateNewRating(TotalRatings, Rating, rating);
            TotalRatings++;

            MarkAsUpdated();
        }

        public bool AddItem(string feature)
        {
            if (CollectionHelper.AddUniqueItem(_features, feature, 5))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public bool RemoveItem(string feature)
        {
            if (CollectionHelper.RemoveItem(_features, feature))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public string GetItemsAsString() => CollectionHelper.ToString(_features);

        public void UpdateLayout(string name, uint roomNumber, uint rows, uint seatsPerRow)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            if (rows == 0 || seatsPerRow == 0)
                throw new ArgumentException("Rows and seats per row must be greater than 0");

            Name = name;
            RoomNumber = roomNumber;
            Rows = rows;
            SeatsPerRow = seatsPerRow;

            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Auditorium: {Name} (Room {RoomNumber})\n" +
                   $"Capacity: {Capacity} seats ({Rows}x{SeatsPerRow})\n" +
                   $"Features: {GetItemsAsString()}\n" +
                   $"Rating: {Rating} ({TotalRatings} ratings)\n" +
                   $"Cinema ID: {CinemaId}\n" +
                   $"ID: {Id}";
        }
    }
}