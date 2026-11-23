namespace Gemini.DTO
{
    public class MedicineSalesSummaryDto
    {
        public string MedicineName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}