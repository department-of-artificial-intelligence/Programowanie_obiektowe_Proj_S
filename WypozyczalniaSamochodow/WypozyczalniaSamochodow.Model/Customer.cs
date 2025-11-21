namespace WypozyczalniaSamochodow.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[{Id}] {FirstName} {LastName} | Numer prawa jazdy: {LicenseNumber}";
        }
    }
}