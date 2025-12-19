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
        public int LoyaltyPoints { get; set; } = 0;

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        public List<Rental> Rentals { get; set; } = new List<Rental>();

        public Customer() { }
        public Customer(int id, string firstName, string lastName, string licenseNumber, string email, string phoneNumber, int branchId, Branch? branch, List<Rental> rentals)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            BranchId = branchId;
            Branch = branch;
            Rentals = rentals;
        }

        public override string ToString()
        {
            return $"  [{Id}] {FirstName} {LastName}\n" +
                   $"      🪪 Prawo jazdy: {LicenseNumber}\n" +
                   $"      📧 {Email} | 📞  +48 {PhoneNumber}\n" +
                   $"      ⭐ Punkty lojalnościowe: {LoyaltyPoints}\n";
        }
    }
}