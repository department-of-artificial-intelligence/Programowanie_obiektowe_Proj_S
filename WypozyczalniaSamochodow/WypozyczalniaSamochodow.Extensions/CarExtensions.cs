using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Extensions
{
    public static class CarExtensions
    {
        public static bool HasActiveRentals(this Car car) => car.Rentals.Any(r => !r.IsCompleted);

        public static void ValidateCar(this Car car)
        {
            if (string.IsNullOrWhiteSpace(car.Brand))
                throw new ArgumentException("Marka pojazdu jest wymagana");

            if (string.IsNullOrWhiteSpace(car.Model))
                throw new ArgumentException("Model pojazdu jest wymagany");

            if (car.ProductionYear < 1900 || car.ProductionYear > DateTime.Now.Year)
                throw new ArgumentException($"Rok produkcji musi być większy od 1900 i =<{DateTime.Now.Year}");

            if (car.Power <= 0 || car.Power > 2000)
                throw new ArgumentException("Moc pojazdu musi być większa od 0 i mniejsza od 2000 KM");

            if (car.EngineVolume <= 0 || car.EngineVolume > 10)
                throw new ArgumentException("Pojemność silnika musi być większa od 0 i mniejsza od 10l");

            if (car.AvgConsumption <= 0 || car.AvgConsumption > 50)
                throw new ArgumentException("Średnie spalanie musi być większe od 0 i mniejsze od 50l/100km");

            if (string.IsNullOrWhiteSpace(car.Gearbox) ||
                !(car.Gearbox.Equals("automatyczna", StringComparison.OrdinalIgnoreCase) ||
                  car.Gearbox.Equals("manualna", StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Typ skrzyni biegów musi być jednym z: 'automatyczna' lub 'manualna'");

            if (string.IsNullOrWhiteSpace(car.FuelType) ||
                !(car.FuelType.Equals("benzyna", StringComparison.OrdinalIgnoreCase) ||
                  car.FuelType.Equals("diesel", StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Typ paliwa musi być jednym z: 'benzyna' lub 'diesel'");

            if (car.PricePerDay <= 0 || car.PricePerDay > 10000)
                throw new ArgumentException("Cena za dzień musi być większa od 0 i mniejsza od 10 000 zł");
        }
    }
}
