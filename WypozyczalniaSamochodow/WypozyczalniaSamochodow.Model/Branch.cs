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
        public Branch(int id, string name, string city, string address, string contactNumber, List<Car> cars, List<Customer> customers, List<Rental> rentals)
        {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            ContactNumber = contactNumber;
            Cars = cars;
            Customers = customers;
            Rentals = rentals;
        }

        public override string ToString()
        {
            return $"  [{Id}] {Name}\n" +
                   $"      📍 {City}, {Address}\n" +
                   $"      📞  +48 {ContactNumber}\n";
        }
    }
}