using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Extensions
{
    public static class RentalExtensions
    {
        public static bool Overlaps(this Rental rental, DateTime start, DateTime end)
            => !(end <= rental.StartDate || start >= rental.EndDate);

        public static IEnumerable<Rental> ActiveRentals(this IEnumerable<Rental> rentals)
            => rentals.Where(r => !r.IsCompleted);

        public static IEnumerable<Rental> CompletedRentals(this IEnumerable<Rental> rentals)
            => rentals.Where(r => r.IsCompleted);

        public static void ValidateRental(this Rental rental)
        {
            if (rental.CustomerId <= 0)
                throw new ArgumentException("Nieprawidłowy klient");

            if (rental.CarId <= 0)
                throw new ArgumentException("Nieprawidłowy samochód");

            if (rental.BranchId <= 0)
                throw new ArgumentException("Nieprawidłowy oddział");

            if (rental.StartDate == default)
                throw new ArgumentException("Data rozpoczęcia jest wymagana");

            if (rental.EndDate == default)
                throw new ArgumentException("Data zakończenia jest wymagana");

            if (rental.EndDate <= rental.StartDate)
                throw new ArgumentException("Data zakończenia musi być późniejsza niż rozpoczęcia");

            if (rental.StartDate.Date < DateTime.Today) 
                throw new ArgumentException("Nie można utworzyć wypożyczenia w przeszłości");

            if (rental.Days <= 0)
                throw new ArgumentException("Okres wypożyczenia musi być większy od 0 dni");

            if (rental.Cost < 0)
                throw new ArgumentException("Koszt wypożyczenia musi być większy lub równy 0 zł");
        }
    }
}
