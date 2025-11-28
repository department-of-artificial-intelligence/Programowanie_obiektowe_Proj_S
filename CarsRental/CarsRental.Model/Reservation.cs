using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public class ReservationManager : IManager<Reservation>
    {
        private List<Reservation> _reservations = new List<Reservation>();
        private int _resCounter = 0;

        public void Add(Reservation reservation)
        {
            Console.WriteLine("Dodawanie rezerwacji...");

            if (reservation == null)
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return;
            }

            if (reservation.ReservationStart >= reservation.ReservationEnd)
            {
                Console.WriteLine($"Koniec rezerwacji nie może być wcześniej niż jej początek!\n");
                return;
            }

            if (!reservation.Car.IsAvailable)
            {
                Console.WriteLine($"Samochód {reservation.Car.Brand} {reservation.Car.Model} {reservation.Car.ProdYear} jest już wypożyczony!\n");
                return;
            }

            _resCounter++;
            reservation.Id = _resCounter;
            _reservations.Add(reservation);

            reservation.Car.Reserve(reservation.ReservationStart, reservation.ReservationEnd);

            Console.WriteLine($"Dodano rezerwację dla {reservation.Customer.FirstName} {reservation.Customer.LastName} na samochód {reservation.Car.Brand} {reservation.Car.Model} {reservation.Car.ProdYear}.\n");
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation? GetById(int id)
        {
            var res = _reservations.Find(r => r.Id == id);
            if (res == null)
            {
                Console.WriteLine($"Nie znaleziono rezerwacji ID({id})\n");
                return null;
            }
            return res;
        }

        public void Remove(int id)
        {
            Console.WriteLine("Usuwanie rezerwacji...");

            Reservation? resToRemove = GetById(id);
            if (resToRemove != null)
            {
                _reservations.Remove(resToRemove);
                Console.WriteLine($"Usunięto rezerwację ID({resToRemove.Id})\n");
            }
        }
    }

    public class Reservation : IIdentify
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private Customer _customer;
        public Customer Customer { get { return _customer; } set { _customer = value; } }

        private Car _car;
        public Car Car { get { return _car; } set { _car = value; } }

        private DateTime _reservationStart;
        public DateTime ReservationStart { get { return _reservationStart; } set { _reservationStart = value; } }

        private DateTime _reservationEnd;
        public DateTime ReservationEnd { get { return _reservationEnd; } set { _reservationEnd = value; } }


        public Reservation() : this(0, new Customer(), new Car(), DateTime.Now, DateTime.Now.AddDays(1)) { }
        public Reservation(int id, Customer customer, Car car, DateTime reservationStart, DateTime reservationEnd)
        {
            _id = id;
            _customer = customer;
            _car = car;
            _reservationStart = reservationStart;
            _reservationEnd = reservationEnd;
        }

        public double ObliczKoszt()
        {
            int liczbaDni = (ReservationEnd - ReservationStart).Days;
            if (liczbaDni < 1)
            {
                Console.WriteLine("Rezerwacja musi trwać minimum 1 dzień!\n");
                return 0;
            }
            else if (liczbaDni >= 1 && liczbaDni <= 3)
            {
                return liczbaDni * Car.BasePrice;
            }
            else if (liczbaDni >= 4 && liczbaDni <= 7)
            {
                return liczbaDni * (Car.BasePrice * 0.90);
            }
            else if (liczbaDni >= 8 && liczbaDni <= 14)
            {
                return liczbaDni * (Car.BasePrice * 0.85);
            }
            else if (liczbaDni >= 15 && liczbaDni <= 30)
            {
                return liczbaDni * (Car.BasePrice * 0.80);
            }
            else
            {
                return liczbaDni * (Car.BasePrice * 0.75);
            }
        }

        public override string ToString()
        {
            return $"[{Id}] {Customer.FirstName} {Customer.LastName} wypożyczył [{Car.Id}] {Car.Brand} {Car.Model}\n" +
                   $"{ReservationStart:dd.MM.yyyy} - {ReservationEnd:dd.MM.yyyy}, kwota: {ObliczKoszt()}zł\n";
        }
    }
}
