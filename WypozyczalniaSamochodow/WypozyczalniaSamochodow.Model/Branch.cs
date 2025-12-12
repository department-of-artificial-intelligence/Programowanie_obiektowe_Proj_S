
namespace WypozyczalniaSamochodow.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Rental> Rentals { get; set; } = new List<Rental>();

        public Branch() { }
        public Branch(int id, string name, string city, string address, string contactNumber)
        {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            ContactNumber = contactNumber;
        }

        public override string ToString()
        {
            return $"[{Id}]: {Name} ({City}) | Adres: {Address} | Tel: {ContactNumber}";

        }
    }
}
