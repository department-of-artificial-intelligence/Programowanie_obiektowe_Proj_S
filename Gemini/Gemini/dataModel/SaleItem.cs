namespace Gemini.dataModel
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}