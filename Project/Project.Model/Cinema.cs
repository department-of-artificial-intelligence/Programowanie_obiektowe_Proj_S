using Project.Models.Common;
using Project.Interfaces;
using Project.Utils;

namespace Project.Models
{
    public class Cinema : Base, IRatable, IListManageable<string>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ManagerName { get; set; }

        public double Rating { get; set; }
        public uint TotalRatings { get; set; }

        private readonly List<string> _availableFilmIds;
        public IReadOnlyList<string> AvailableFilmIds => _availableFilmIds.AsReadOnly();
        public IReadOnlyList<string> Items => AvailableFilmIds;

        public Cinema()
        {
            _availableFilmIds = [];
            Name = string.Empty;
            Address = string.Empty;
            ContactPhone = string.Empty;
            ContactEmail = string.Empty;
            ManagerName = string.Empty;
            Rating = 0;
            TotalRatings = 0;
        }

        public Cinema(string name, string address, string contactPhone, string contactEmail, string managerName) : base()
        {
            ValidateCinemaData(name, address, contactPhone, contactEmail, managerName);

            Name = name;
            Address = address;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            ManagerName = managerName;
            _availableFilmIds = [];
        }

        private static void ValidateCinemaData(string name, string address, string contactPhone, string contactEmail, string managerName)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Cinema name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address is required", nameof(address));
            if (string.IsNullOrWhiteSpace(contactPhone)) throw new ArgumentException("Contact phone is required", nameof(contactPhone));
            if (string.IsNullOrWhiteSpace(contactEmail)) throw new ArgumentException("Contact email is required", nameof(contactEmail));
            if (string.IsNullOrWhiteSpace(managerName)) throw new ArgumentException("Manager name is required", nameof(managerName));
        }

        public void AddRating(uint rating)
        {
            if (rating < 1 || rating > 5) throw new ArgumentException("Rating must be between 1 and 5");

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

        public void UpdateInfo(string name, string address, string contactPhone, string contactEmail, string managerName)
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