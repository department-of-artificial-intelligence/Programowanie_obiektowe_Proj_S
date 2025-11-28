using CarsRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Logic
{
    public class ReservationService : IReservationService
    {
        private List<Reservation> _reservations = new List<Reservation>();
        private ICarService _carService;
        private int _resCounter = 0;

        public ReservationService(ICarService carService) 
        { 
            _carService = carService;
        }

        public bool CreateReservation(Reservation reservation)
        {
            Console.WriteLine("Dodawanie rezerwacji...");

            if (reservation == null)
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return false;
            }

            if (reservation.ReservationStart >= reservation.ReservationEnd)
            {
                Console.WriteLine($"Koniec rezerwacji nie może być wcześniej niż jej początek!\n");
                return false;
            }

            if (!reservation.Car.IsAvailable)
            {
                Console.WriteLine($"Samochód {reservation.Car.Brand} {reservation.Car.Model} {reservation.Car.ProdYear} jest już wypożyczony!\n");
                return false;
            }

            _carService.RentCar(reservation.Car.Id);

            _resCounter++;
            reservation.Id = _resCounter;
            _reservations.Add(reservation);

            Console.WriteLine($"Dodano rezerwację dla {reservation.Customer.FirstName} {reservation.Customer.LastName} na samochód {reservation.Car.Brand} {reservation.Car.Model} {reservation.Car.ProdYear}.\n");
            return true;
        }

        public Reservation? GetReservation(int reservationId)
        {
            var res = _reservations.Find(r => r.Id == reservationId);
            if (res == null)
            {
                Console.WriteLine($"Nie znaleziono rezerwacji ID({reservationId})\n");
                return null;
            }
            return res;
        }

        public void RemoveReservation(int reservationId)
        {
            Console.WriteLine("Usuwanie rezerwacji...");

            Reservation? resToRemove = GetReservation(reservationId);
            if (resToRemove != null)
            {
                _reservations.Remove(resToRemove);
                Console.WriteLine($"Usunięto rezerwację ID({resToRemove.Id})\n");
            }
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservations;
        }

        public double CalculateCost(int reservationId)
        {
            var res = GetReservation(reservationId);
            if (res == null)
            {
                Console.WriteLine($"Nie znaleziono rezerwacji o ID({reservationId})!\n");
                return 0;
            }

            var car = _carService.GetCar(res.Car.Id);
            if(car == null)
            {
                Console.WriteLine($"Nie znaleziono auta o ID({res.Car.Id})!\n");
                return 0;
            }

            int days = (res.ReservationEnd - res.ReservationStart).Days;
            if (days < 1)
            {
                Console.WriteLine("Rezerwacja musi trwać minimum 1 dzień!\n");
                return 0;
            }
            else if (days >= 1 && days <= 3)
            {
                return days * car.BasePrice;
            }
            else if (days >= 4 && days <= 7)
            {
                return days * (car.BasePrice * 0.90);
            }
            else if (days >= 8 && days <= 14)
            {
                return days * (car.BasePrice * 0.85);
            }
            else if (days >= 15 && days <= 30)
            {
                return days * (car.BasePrice * 0.80);
            }
            else
            {
                return days * (car.BasePrice * 0.75);
            }
        }
    }
}
