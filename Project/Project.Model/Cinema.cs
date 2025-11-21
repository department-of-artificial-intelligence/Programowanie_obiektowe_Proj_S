using Project.Entities;
using Project.Interfaces;
using Project.Utils;

namespace Project.Models
{
    public class Cinema : BaseEntity, IRatable, IListManageable<string>
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string ContactPhone { get; private set; }
        public string ContactEmail { get; private set; }
        public string ManagerName { get; private set; }

        public double Rating { get; private set; }
        public uint TotalRatings { get; private set; }

        private readonly List<string> _availableFilmIds;
        public IReadOnlyList<string> AvailableFilmIds => _availableFilmIds.AsReadOnly();
        public IReadOnlyList<string> Items => AvailableFilmIds;

        public Cinema(string name, string address, string contactPhone,
            string contactEmail, string managerName) : base()
        {
            ValidateCinemaData(name, address, contactPhone, contactEmail, managerName);

            Name = name;
            Address = address;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            ManagerName = managerName;
            _availableFilmIds = [];
        }

        public Cinema(string id, string name, string address, string contactPhone,
            string contactEmail, string managerName, List<string> availableFilmIds,
            double rating, uint totalRatings, DateTime createdAt, DateTime updatedAt)
            : base(id, createdAt, updatedAt)
        {
            ValidateCinemaData(name, address, contactPhone, contactEmail, managerName);

            Name = name;
            Address = address;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            ManagerName = managerName;
            _availableFilmIds = availableFilmIds ?? [];
            Rating = rating;
            TotalRatings = totalRatings;
        }

        private static void ValidateCinemaData(string name, string address, string contactPhone,
            string contactEmail, string managerName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cinema name is required", nameof(name));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address is required", nameof(address));

            if (string.IsNullOrWhiteSpace(contactPhone))
                throw new ArgumentException("Contact phone is required", nameof(contactPhone));

            if (string.IsNullOrWhiteSpace(contactEmail))
                throw new ArgumentException("Contact number name is required", nameof(contactEmail));

            if (string.IsNullOrWhiteSpace(managerName))
                throw new ArgumentException("Manager name is required", nameof(managerName));
        }

        public void AddRating(uint rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            Rating = RatingCalculator.CalculateNewRating(TotalRatings, Rating, rating);
            TotalRatings++;
            MarkAsUpdated();
        }

        public bool AddItem(string filmId)
        {
            if (CollectionHelper.AddUniqueItem(_availableFilmIds, filmId, 10))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public bool RemoveItem(string filmId)
        {
            if (CollectionHelper.RemoveItem(_availableFilmIds, filmId))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public string GetItemsAsString() => CollectionHelper.ToString(_availableFilmIds);

        public void UpdateInfo(string name, string address, string contactPhone,
            string contactEmail, string managerName)
        {
            ValidateCinemaData(name, address, contactPhone, contactEmail, managerName);

            Name = name;
            Address = address;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            ManagerName = managerName;

            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Cinema: {Name}\n" +
                   $"Address: {Address}\n" +
                   $"Contact: {ContactPhone} | {ContactEmail}\n" +
                   $"Manager: {ManagerName}\n" +
                   $"Rating: {Rating} ({TotalRatings} ratings)\n" +
                   $"Available Films: {GetItemsAsString()}\n" +
                   $"ID: {Id}";
        }
    }
}
