using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Extensions;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Services
{
    public class RentalService(ApplicationDbContext context) : IRentalService
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<Rental> GetActiveRentals(int branchId)
        {
            return _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Where(r => r.BranchId == branchId && !r.IsCompleted)
                .OrderBy(r => r.StartDate)
                .ToList();
        }

        public IEnumerable<Rental> GetCompletedRentals(int branchId)
        {
            return _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Where(r => r.BranchId == branchId && r.IsCompleted)
                .OrderByDescending(r => r.CompletedDate)
                .ToList();
        }

        public void RentCar(Rental rental)
        {
            rental.ValidateRental();

            var car = _context.Cars
                .Include(c => c.Rentals)
                .FirstOrDefault(c => c.Id == rental.CarId && c.BranchId == rental.BranchId);

            if (car is null) throw new InvalidOperationException("Samochód nie istnieje w tym oddziale");

            var customer = _context.Customers
                .FirstOrDefault(c => c.Id == rental.CustomerId && c.BranchId == rental.BranchId);

            if (customer is null) throw new InvalidOperationException("Klient nie istnieje w tym oddziale");

            if (car.Rentals.ActiveRentals().Any(r => r.Overlaps(rental.StartDate, rental.EndDate)))
                throw new InvalidOperationException("Samochód jest już zarezerwowany w tym okresie");

            if (rental.StartDate.Date == DateTime.Today)
            {
                car.IsAvailable = false;
            }
            else
            {
                car.IsAvailable = true;
            }

            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public void ReturnCar(int rentalId, int branchId)
        {
            var rental = _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefault(r => r.Id == rentalId && r.BranchId == branchId && !r.IsCompleted);

            if (rental is null)
                throw new InvalidOperationException("Brak aktywnego wypożyczenia o podanym ID");

            if (rental.StartDate.Date > DateTime.Today)
            {
                rental.Cost = 0;
                rental.IsCompleted = true;
                rental.IsCancelledBeforeStart = true;
                rental.CompletedDate = DateTime.Now;

                bool hasOtherActive = _context.Rentals
                    .Any(r => r.CarId == rental.CarId && !r.IsCompleted && r.Id != rental.Id);

                rental.Car!.IsAvailable = !hasOtherActive;

                _context.SaveChanges();
                return;
            }

            rental.IsCancelledBeforeStart = false;
            rental.IsCompleted = true;
            rental.CompletedDate = DateTime.Now;

            rental.Customer!.LoyaltyPoints += rental.Days;

            bool hasOtherActiveRentals = _context.Rentals
                .Any(r => r.CarId == rental.CarId
                       && !r.IsCompleted
                       && r.Id != rental.Id
                       && r.StartDate.Date <= DateTime.Today
                       && r.EndDate.Date >= DateTime.Today);

            rental.Car!.IsAvailable = !hasOtherActiveRentals;

            _context.SaveChanges();
        }


        public void RentCarWithPoints(Rental rental)
        {
            rental.ValidateRental();

            var car = _context.Cars
                .Include(c => c.Rentals)
                .FirstOrDefault(c => c.Id == rental.CarId && c.BranchId == rental.BranchId);
            if (car is null) throw new InvalidOperationException("Samochód nie istnieje w tym oddziale");

            var customer = _context.Customers.FirstOrDefault(c => c.Id == rental.CustomerId && c.BranchId == rental.BranchId);
            if (customer is null) throw new InvalidOperationException("Klient nie istnieje w tym oddziale");

            if (!customer.HasEnoughPoints(20))
                throw new InvalidOperationException($"Klient posiada {customer.LoyaltyPoints} punktów – wymagane 20");

            if (car.Rentals.ActiveRentals().Any(r => r.Overlaps(rental.StartDate, rental.EndDate)))
                throw new InvalidOperationException("Samochód jest niedostępny w tym okresie");

            car.IsAvailable = false;
            customer.LoyaltyPoints -= 20;

            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

    }
}
