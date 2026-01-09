using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Extensions;

namespace VehicleRentalSystem.Model.Service
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleService _vehicleService;

        public ReservationService(ApplicationDbContext context, IVehicleService vehicleService)
        {
            _context = context;
            _vehicleService = vehicleService;
        }

        public async Task<Reservation?> CreateReservation(int customerId, int vehicleId, DateTime startDate, DateTime endDate)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null || !customer.HasValidDriverLicense())
            {
                Console.WriteLine("\n[BŁĄD] Klient nie istnieje lub ma nieważne prawo jazdy!");
                return null;
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Department)
                .FirstOrDefaultAsync(v => v.Id == vehicleId && !v.IsRented && v.DepartmentId > 0);

            if (vehicle == null)
            {
                Console.WriteLine("\n[BŁĄD] Pojazd niedostępny!");
                return null;
            }

            if (startDate >= endDate || startDate < DateTime.Now.Date)
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe daty rezerwacji!");
                return null;
            }

            if (!vehicle.CanBeRentedForPeriod(startDate, endDate, 14))
            {
                Console.WriteLine($"\n[BŁĄD] Pojazd może być maksymalnie wypożyczony na {vehicle.DaysToService() - 14} dni! Ponieważ musi wrócić na serwis");
                return null;
            }

            int days = (endDate - startDate).Days;
            double discount = days switch
            {
                >= 31 => 0.75,
                >= 15 => 0.80,
                >= 8 => 0.85,
                >= 4 => 0.90,
                _ => 1.0
            };
            double totalPrice = days * vehicle.PriceForDay * discount;

            var reservation = new Reservation
            {
                CustomerId = customerId,
                VehicleId = vehicleId,
                DepartmentId = vehicle.DepartmentId,
                StartDate = startDate,
                EndDate = endDate,
                Price = totalPrice,
                Status = ActualStatus.Potwierdzona
            };

            vehicle.IsRented = true;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Rezerwacja utworzona pomyślnie! ID: {reservation.Id}");
            return reservation;
        }

        public async Task<Reservation?> GetReservation(int reservationId)
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .Include(r => r.Department)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }


        public async Task<bool> CompleteReservation(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Vehicle)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono rezerwacji!");
                return false;
            }

            if (reservation.Status == ActualStatus.Zakończona)
            {
                Console.WriteLine("\n[BŁĄD] Rezerwacja jest już zakończona!");
                return false;
            }

            if (reservation.Status == ActualStatus.Anulowana)
            {
                Console.WriteLine("\n[BŁĄD] Nie można zakończyć anulowanej rezerwacji!");
                return false;
            }

            reservation.Status = ActualStatus.Zakończona;
            if (reservation.Vehicle != null)
                reservation.Vehicle.IsRented = false;

            await _context.SaveChangesAsync();
            Console.WriteLine($"\n[INFO] Rezerwacja [{reservation.Id}] zakończona pomyślnie!");
            return true;
        }


        public async Task<List<Reservation>> GetAllReservations()
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .Include(r => r.Department)
                .ToListAsync();
        }

        public async Task<bool> CancelReservation(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Vehicle)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono rezerwacji!");
                return false;
            }

            if (!reservation.CanBeCancelled())
            {
                Console.WriteLine("\n[BŁĄD] Nie można anulować tej rezerwacji! (jest już zakończona lub anulowana)");
                return false;
            }

            reservation.Status = ActualStatus.Anulowana;
            if (reservation.Vehicle != null)
                reservation.Vehicle.IsRented = false;

            await _context.SaveChangesAsync();
            Console.WriteLine($"\n[INFO] Rezerwacja [{reservation.Id}] została anulowana pomyślnie!");
            return true;
        }

        public async Task<double> CalculateCost(int reservationId)
        {
            var res = await GetReservation(reservationId);
            if (res == null)
            {
                Console.WriteLine($"\n[BŁĄD] Nie znaleziono rezerwacji o ID({reservationId})!\n");
                return 0;
            }

            if(res.Vehicle == null)
            {
                Console.WriteLine($"\n[BŁĄD] Nie znaleziono pojazdu przypisanego do rezerwacji o ID({reservationId})!\n");
                return 0;
            }

            var vehicle = await _vehicleService.GetVehicleById(res.Vehicle.Id);
            if (vehicle == null)
            {
                Console.WriteLine($"\n[BŁĄD] Nie znaleziono auta o ID({res.Vehicle.Id})!\n");
                return 0;
            }

            int days = (res.EndDate - res.StartDate).Days;
            if (days < 1)
            {
                Console.WriteLine("\n[BŁĄD] Rezerwacja musi trwać minimum 1 dzień!\n");
                return 0;
            }
            else if (days <= 3)
            {
                return days * vehicle.PriceForDay;
            }
            else if (days <= 7)
            {
                return days * (vehicle.PriceForDay * 0.90);
            }
            else if (days <= 14)
            {
                return days * (vehicle.PriceForDay * 0.85);
            }
            else if (days <= 30)
            {
                return days * (vehicle.PriceForDay * 0.80);
            }
            else
            {
                return days * (vehicle.PriceForDay * 0.75);
            }
        }
        public async Task<int> GetReservationCount()
        {
            return await _context.Reservations.CountAsync();
        }

        public async Task<int> GetActiveReservationCount()
        {
            return await _context.Reservations
                .CountAsync(r => r.Status == ActualStatus.Potwierdzona || r.Status == ActualStatus.Rozpoczęta);
        }


        public async Task<int> GetCompletedReservationCount()
        {
            return await _context.Reservations
                .CountAsync(r => r.Status == ActualStatus.Zakończona);
        }
        public async Task<int> GetCanceledReservationCount()
        {
            return await _context.Reservations
                .CountAsync(r => r.Status == ActualStatus.Anulowana);
        }
    }
}