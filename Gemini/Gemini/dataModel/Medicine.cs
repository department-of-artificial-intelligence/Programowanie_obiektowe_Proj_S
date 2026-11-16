namespace Gemini.dataModel
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ActiveSubstance { get; set; }
        public string? Manufacturer { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool RequiresPrescription { get; set; }
        public int CategoryId { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public ICollection<PharmacyInventory> Inventories { get; set; } = new List<PharmacyInventory>();
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}