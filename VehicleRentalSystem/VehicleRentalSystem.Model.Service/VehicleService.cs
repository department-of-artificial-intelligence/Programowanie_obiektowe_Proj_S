using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model.Extensions;

namespace VehicleRentalSystem.Model.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            Console.WriteLine("\n[INFO] Dodawanie pojazdu...\n");

            try
            {
                if (await _context.Vehicles.AnyAsync(v => v.RegistrationNumber == vehicle.RegistrationNumber))
                {
                    throw new ArgumentException("\n[BŁĄD]Pojazd o takich numerach rejestracyjnych już istnieje!");
                }

                vehicle.IsActive = true;
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                Console.WriteLine($"\n[INFO] Pomyślnie dodano {vehicle.Brand} {vehicle.Model} ({vehicle.RegistrationNumber}) do wypożyczalni\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task<bool> DeleteVehicle(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono takiego pojazdu!");
                return false;
            }
            if (vehicle.IsRented)
            {
                Console.WriteLine("\n[BŁĄD] Nie można usunąć pojazdu - pojazd jest aktualnie wypożyczony!");
                return false;
            }

            vehicle.IsActive = false;
            await _context.SaveChangesAsync();
            Console.WriteLine($"\n[INFO] Pomyślnie usunięto {vehicle.Brand} {vehicle.Model} [{vehicle.RegistrationNumber}] z wypożyczalni!");
            return true;
        }

        public async Task<Vehicle?> GetVehicleById(int vehicleId)
        {
            return await _context.Vehicles
                .Where(v => v.IsActive)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicles
                .Include(v => v.Department)
                .Where(v => v.IsActive)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> SearchVehicles(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllVehicles();

            searchTerm = searchTerm.ToLower();

            return await _context.Vehicles
                .Include(v => v.Department)
                .Where(v => v.IsActive &&
                    ((v.Brand != null && v.Brand.ToLower().Contains(searchTerm)) ||
                    (v.Model != null && v.Model.ToLower().Contains(searchTerm)) ||
                    v.ProdYear.ToString().Contains(searchTerm)))
                .ToListAsync();
        }

        public async Task<bool> ServiceVehicle(int vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.IsActive)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);
            if (vehicle == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pojazdu!");
                return false;
            }

            vehicle.NextService = DateTime.Now.AddMonths(6);
            vehicle.LastService = DateTime.Now;
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Pomyślnie oznaczono {vehicle.Brand} {vehicle.Model} {vehicle.ProdYear} [{vehicle.RegistrationNumber}] jako zserwisowany!");
            return true;
        }

        public async Task<int> GetVehicleCount()
        {
            return await _context.Vehicles.CountAsync(v => v.IsActive);
        }

        public async Task<int> GetRentedVehicleCount()
        {
            return await _context.Vehicles.CountAsync(v => v.IsActive && v.IsRented);
        }

        public async Task<int> GetAvailableVehicleCount()
        {
            var total = await GetVehicleCount();
            var rented = await GetRentedVehicleCount();
            return total - rented;
        }
    }
}
