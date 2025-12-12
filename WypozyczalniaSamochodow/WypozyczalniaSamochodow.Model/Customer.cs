namespace WypozyczalniaSamochodow.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Customer() { }
        public Customer(int id, string firstName, string lastName, string licenseNumber, string email, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"[{Id}] {FirstName} {LastName} | Prawo jazdy: {LicenseNumber} | Email: {Email} | Tel: {PhoneNumber}";
        }
    }
}