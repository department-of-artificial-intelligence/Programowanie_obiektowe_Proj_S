using System;

namespace WypozyczalniaSamochodow.Model
{
    public class Rental
    {
        public int Id { get; set; }
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
        public Branch? Branch { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return $"Wypożyczenie #{Id}: {Car?.Brand} {Car?.Model}, Klient: {Customer?.FirstName} {Customer?.LastName}, " +
                   $"Okres: {StartDate.ToShortDateString()} - {EndDate.ToShortDateString()} ({Days} dni), Koszt: {Cost} zł";
        }
    }
}