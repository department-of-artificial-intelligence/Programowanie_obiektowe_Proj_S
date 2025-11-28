using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{

    public class Reservation
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

        public override string ToString()
        {
            return $"[{Id}] {Customer.FirstName} {Customer.LastName} wypożyczył [{Car.Id}] {Car.Brand} {Car.Model}\n" +
                   $"{ReservationStart:dd.MM.yyyy} - {ReservationEnd:dd.MM.yyyy}\n";
        }
    }
}
