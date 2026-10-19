namespace Gemini.dataModel
{
    public class Sale
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int UserId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public int? PrescriptionId { get; set; }
    }
}