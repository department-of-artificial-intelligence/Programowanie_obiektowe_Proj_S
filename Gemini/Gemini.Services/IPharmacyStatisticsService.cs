using Gemini.dataModel;
using Gemini.DTO;

namespace Gemini.Services
{
    public interface IPharmacyStatisticsService
    {
        List<Medicine> GetMedicinesByCategory(int categoryId);
        List<Medicine> GetPrescriptionMedicines();
        decimal GetAverageMedicinePrice();
        List<PharmacyInventory> GetLowStockItems(int threshold = 10);

        Dictionary<string, decimal> GetTotalSalesByPharmacy();
        Dictionary<string, int> GetMedicineCountByCategory();
        List<MedicineSalesSummaryDto> GetTopSellingMedicines();
    }
}