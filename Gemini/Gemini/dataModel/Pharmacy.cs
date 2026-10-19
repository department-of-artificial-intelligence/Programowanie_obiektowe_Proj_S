namespace Gemini.dataModel
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<User> users { get; set; } = new List<User>();
        public ICollection<PharmacyInventory> inventory { get; set; } = new List<PharmacyInventory>();
        public ICollection<Sale> sales { get; set; } = new List<Sale>();
    }
}