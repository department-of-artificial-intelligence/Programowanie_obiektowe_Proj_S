using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Extensions;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Services
{
    public class CarService(ApplicationDbContext context) : ICarService
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<Car> GetCarByBranch(int branchId)
        {
            return _context.Cars
                .Include(c => c.Rentals)
                .Where(c => c.BranchId == branchId)
                .ToList();
        }

        public void AddCar(Car car, Branch branch)
        {
            if (car is null) throw new ArgumentNullException(nameof(car));

            car.ValidateCar();

            bool branchExists = _context.Branches.Any(b => b.Id == branch.Id);
            if (!branchExists) throw new InvalidOperationException("Oddział o podanym ID nie istnieje");

            car.BranchId = branch.Id;
            car.IsAvailable = true;

            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            if (car is null) throw new ArgumentNullException(nameof(car));

            car.ValidateCar();

            var existingCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
            if (existingCar is null) throw new InvalidOperationException("Samochód nie istnieje");

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.ProductionYear = car.ProductionYear;
            existingCar.Power = car.Power;
            existingCar.EngineVolume = car.EngineVolume;
            existingCar.AvgConsumption = car.AvgConsumption;
            existingCar.Gearbox = car.Gearbox;
            existingCar.FuelType = car.FuelType;
            existingCar.PricePerDay = car.PricePerDay;

            _context.SaveChanges();
        }


        public void RemoveCar(int carId, int branchId)
        {
            var car = _context.Cars
                .Include(c => c.Rentals)
                .FirstOrDefault(c => c.Id == carId && c.BranchId == branchId);

            if (car is null) throw new InvalidOperationException("Samochód o takim ID nie istnieje w tym oddziale");

            if (car.Rentals.Any(r => !r.IsCompleted))
                throw new InvalidOperationException("Nie można usunąć samochodu posiadającego aktywne wypożyczenia");

            _context.Cars.Remove(car);
            _context.SaveChanges();
        }

        public IEnumerable<Car> SearchCars(int branchId, string? brand, int? minPower, decimal? maxPrice, string? gearbox)
        {
            var query = _context.Cars
                .Include(c => c.Rentals)
                .Where(c => c.BranchId == branchId);

            if (!string.IsNullOrWhiteSpace(brand)) query = query.Where(c => c.Brand.Contains(brand));
            if (minPower.HasValue) query = query.Where(c => c.Power >= minPower.Value);
            if (maxPrice.HasValue) query = query.Where(c => c.PricePerDay <= maxPrice.Value);
            if (!string.IsNullOrWhiteSpace(gearbox)) query = query.Where(c => c.Gearbox.Contains(gearbox));

            return query.ToList();
        }
    }
}
