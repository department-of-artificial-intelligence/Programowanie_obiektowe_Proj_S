namespace CinemaNetworkManagement.Model
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string PostalCode { get; set; }

        public Address(string city, string street, string buildingNumber, string postalCode)
        {
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            PostalCode = postalCode;
        }
    }
}
