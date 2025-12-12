using WypozyczalniaSamochodow.Model;

public class CarLogic : ICar
{
    private readonly List<Car> _cars;

    public CarLogic(List<Car> cars)
    {
        _cars = cars;
    }

    public void ShowCars(Branch branch)
    {
        if (branch.Cars.Count <= 0)
        {
            Console.WriteLine("Brak samochodów w tym oddziale.");
            return;
        }

        foreach (var car in branch.Cars)
        {
            Console.WriteLine(car);
        }
    }

    public void AddCar(Car car, Branch branch)
    {
        if (car.PricePerDay <= 0)
        {
            Console.WriteLine("Cena za dzień musi być większa od 0.");
            return;
        }
        if (car.ProductionYear < 1900 || car.ProductionYear > DateTime.Now.Year)
        {
            Console.WriteLine("Nieprawidłowy rok produkcji.");
            return;
        }

        car.Id = _cars.Any() ? _cars.Max(c => c.Id) + 1 : 1;
        car.BranchId = branch.Id;
        car.IsAvailable = true;

        _cars.Add(car);
        branch.Cars.Add(car);

        Console.WriteLine($"\nDodano samochód {car.Brand} {car.Model} do oddziału {branch.Name} {branch.City}");
    }

    public void RemoveCar(int carId, Branch branch)
    {
        var car = branch.Cars.FirstOrDefault(c => c.Id == carId);
        if (car is null)
        {
            Console.WriteLine("Nie znaleziono samochodu w tym oddziale.");
            return;
        }

        if(car.Reservations.Count > 0)
        {
            Console.WriteLine("Nie można usunąć samochodu, ponieważ posiada aktywne wypożyczenie.");
            return;
        }

        branch.Cars.Remove(car);
        _cars.Remove(car);

        Console.WriteLine($"\nUsunięto samochód {car.Brand} {car.Model} z oddziału {branch.Name} {branch.City}");
    }

    public void ShowCarReservations(int carId, Branch branch)
    {
        var car = branch.Cars.FirstOrDefault(c => c.Id == carId);
        if (car is null)
        {
            Console.WriteLine("Nie znaleziono samochodu w tym oddziale.");
            return;
        }

        Console.WriteLine($"\nIstniejące wypożyczenia dla {car.Brand} {car.Model}:");

        if (car.Reservations.Count <= 0)
        {
            Console.WriteLine("  Brak wypożyczeń – samochód dostępny.");
            return;
        }

        foreach (var r in car.Reservations.OrderBy(r => r.StartDate))
        {
            Console.WriteLine($" - {r.StartDate:dd.MM.yyyy} do {r.EndDate:dd.MM.yyyy} (klient: {r.Customer?.FirstName} {r.Customer?.LastName})");
        }
    }

    public bool HasCars(Branch branch)
    {
        return branch.Cars.Count > 0;
    }
}
