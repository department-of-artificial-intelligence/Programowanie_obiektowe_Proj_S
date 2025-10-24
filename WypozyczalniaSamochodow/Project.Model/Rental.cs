using System;

namespace WypozyczalniaSamochodow.Model
{
    interface IRental
    {
        void ShowRentals();
        void RentCar(int carId, int customerId, int branchId);
        void ReturnCar(int rentalId);
    }

    public class Rental
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Branch Branch { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return $"{Customer.FirstName} {Customer.LastName} wypożyczył {Car.Brand} {Car.Model} w oddziale {Branch.Name} {Branch.City} od {StartDate.ToShortDateString()} do {EndDate.ToShortDateString()}";
        }
    }
}

