using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Services
{
    public class RaportService(ApplicationDbContext context) : IRaportService
    {
        private readonly ApplicationDbContext _context = context;

        public decimal GetTotalRevenue(int branchId)
        {
            return _context.Rentals
                .Where(r => r.BranchId == branchId && r.IsCompleted)
                .Sum(r => r.Cost);
        }

        public Car? GetMostRentedCar(int branchId)
        {
            return _context.Rentals
                .Where(r => r.BranchId == branchId && r.IsCompleted)
                .GroupBy(r => r.CarId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.First().Car)
                .FirstOrDefault();
        }

        public decimal GetAverageDailyRevenue(int branchId)
        {
            var rentals = _context.Rentals
                .Where(r => r.BranchId == branchId && r.IsCompleted)
                .ToList();

            if (rentals.Count == 0) return 0;

            var minDate = rentals.Min(r => r.StartDate);
            var maxDate = rentals.Max(r => r.EndDate);
            var totalDays = (maxDate - minDate).Days + 1;

            var totalRevenue = rentals.Sum(r => r.Cost);

            return totalDays > 0 ? totalRevenue / totalDays : 0;
        }

        public Customer? GetBestCustomer(int branchId)
        {
            return _context.Rentals
                .Where(r => r.BranchId == branchId && r.IsCompleted)
                .GroupBy(r => r.CustomerId)
                .OrderByDescending(g => g.Sum(r => r.Cost))
                .Select(g => g.First().Customer)
                .FirstOrDefault();
        }
    }

}
