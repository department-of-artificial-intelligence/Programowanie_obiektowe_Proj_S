using RestaurantManagement.Models.Interfaces;

namespace RestaurantManagement.Models
{
    public class Address: IAddress
    {
        public int Id { get; set; } // PK dla EF

        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }



        public Address(string country, string zipCode, string city, string street)
        {
            Country = country;
            ZipCode = zipCode;
            City = city;
            Street = street;
        }

        public override string ToString()
        {
            return $"Kraj: {Country}, Kod: {ZipCode} - {City}\n Ulica: {Street}";
        }
    }
}
