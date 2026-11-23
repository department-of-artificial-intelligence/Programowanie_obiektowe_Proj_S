using Gemini.DAL;
using Gemini.dataModel;
using Gemini.DTO;

namespace Gemini.Services
{
    public class PharmacyStatisticsService : IPharmacyStatisticsService
    {
        public List<Medicine> GetMedicinesByCategory(int categoryId)
        {
            return FakeDataStore.Medicines
                .Where(m => m.CategoryId == categoryId)
                .OrderBy(m => m.Name)
                .ToList();
        }

        public List<Medicine> GetPrescriptionMedicines()
        {
            return FakeDataStore.Medicines
                .Where(m => m.RequiresPrescription)
                .ToList();
        }

        public decimal GetAverageMedicinePrice()
        {
            if (!FakeDataStore.Medicines.Any()) return 0;
            return FakeDataStore.Medicines.Average(m => m.Price);
        }

        public List<PharmacyInventory> GetLowStockItems(int threshold = 10)
        {
            return FakeDataStore.Inventories
                .Where(i => i.Quantity < threshold)
                .ToList();
        }

        public Dictionary<string, decimal> GetTotalSalesByPharmacy()
        {
            return FakeDataStore.Sales
                .GroupBy(s => s.PharmacyId)
                .ToDictionary(
                    g => FakeDataStore.Pharmacies.FirstOrDefault(p => p.Id == g.Key)?.Name ?? $"Pharmacy #{g.Key}",
                    g => g.Sum(s => s.TotalAmount)
                );
        }

        public Dictionary<string, int> GetMedicineCountByCategory()
        {
            return FakeDataStore.Medicines
                .GroupBy(m => m.CategoryId)
                .ToDictionary(
                    g => FakeDataStore.Categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Uncategorized",
                    g => g.Count()
                );
        }

        public List<MedicineSalesSummaryDto> GetTopSellingMedicines()
        {
            return FakeDataStore.SaleItems
                .GroupBy(si => si.MedicineId)
                .Select(g => new MedicineSalesSummaryDto
                {
                    MedicineName = FakeDataStore.Medicines.FirstOrDefault(m => m.Id == g.Key)?.Name ?? "Unknown",
                    TotalQuantitySold = g.Sum(si => si.Quantity),
                    TotalRevenue = g.Sum(si => si.Quantity * si.UnitPrice)
                })
                .OrderByDescending(dto => dto.TotalQuantitySold)
                .ToList();
        }
    }
}