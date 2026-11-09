using BikeRental.Services;

var manager = new BikeRentalManager(new HourlyRentalPriceCalculator());

var centralStation = manager.AddStation("Central Station");
var parkStation = manager.AddStation("City Park");

manager.AddCustomer("Anna Kowalska", "anna@example.com");
manager.AddCustomer("Jan Nowak", "jan@example.com");

var bike1 = manager.AddBike("City Bike", 5.00m);
var bike2 = manager.AddBike("Mountain Bike", 8.00m);
var bike3 = manager.AddBike("Trekking Bike", 7.00m);

manager.AddBikeToStation(bike1.Id, centralStation.Id);
manager.AddBikeToStation(bike2.Id, centralStation.Id);
manager.AddBikeToStation(bike3.Id, parkStation.Id);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("===== BIKE RENTAL SYSTEM =====");
    Console.WriteLine("1. Show stations and bikes");
    Console.WriteLine("2. Show customers");
    Console.WriteLine("3. Add customer");
    Console.WriteLine("4. Add bike");
    Console.WriteLine("5. Rent a bike");
    Console.WriteLine("6. Return a bike");
    Console.WriteLine("7. Show rentals");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                ShowStations();
                break;
            case "2":
                ShowCustomers();
                break;
            case "3":
                AddCustomer();
                break;
            case "4":
                AddBike();
                break;
            case "5":
                RentBike();
                break;
            case "6":
                ReturnBike();
                break;
            case "7":
                ShowRentals();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Unknown option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void ShowStations()
{
    foreach (var station in manager.Stations)
    {
        Console.WriteLine($"\n{station.Id}. {station.Name}");

        if (station.Bikes.Count == 0)
        {
            Console.WriteLine("   No bikes at this station.");
        }

        foreach (var bike in station.Bikes)
        {
            Console.WriteLine($"   Bike ID: {bike.Id}, {bike.Model}, {bike.PricePerHour} PLN/hour");
        }
    }
}

void ShowCustomers()
{
    foreach (var customer in manager.Customers)
    {
        Console.WriteLine($"{customer.Id}. {customer.Name} ({customer.Email})");
    }
}

void AddCustomer()
{
    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";
    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    var customer = manager.AddCustomer(name, email);
    Console.WriteLine($"Customer added. ID: {customer.Id}");
}

void AddBike()
{
    Console.Write("Bike model: ");
    string model = Console.ReadLine() ?? "";
    Console.Write("Price per hour (PLN): ");
    decimal price = decimal.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("Available stations:");
    foreach (var station in manager.Stations)
        Console.WriteLine($"{station.Id}. {station.Name}");

    Console.Write("Station ID: ");
    int stationId = int.Parse(Console.ReadLine() ?? "0");

    var bike = manager.AddBike(model, price);
    manager.AddBikeToStation(bike.Id, stationId);
    Console.WriteLine($"Bike added. ID: {bike.Id}");
}

void RentBike()
{
    ShowCustomers();
    Console.Write("Customer ID: ");
    int customerId = int.Parse(Console.ReadLine() ?? "0");

    ShowStations();
    Console.Write("Bike ID: ");
    int bikeId = int.Parse(Console.ReadLine() ?? "0");

    var rental = manager.RentBike(customerId, bikeId);
    Console.WriteLine($"Rental created. Rental ID: {rental.Id}");
}

void ReturnBike()
{
    ShowRentals();
    Console.Write("Rental ID: ");
    int rentalId = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("Available stations:");
    foreach (var station in manager.Stations)
        Console.WriteLine($"{station.Id}. {station.Name}");

    Console.Write("Return station ID: ");
    int stationId = int.Parse(Console.ReadLine() ?? "0");

    decimal price = manager.ReturnBike(rentalId, stationId);
    Console.WriteLine($"Bike returned. Price: {price} PLN");
}

void ShowRentals()
{
    foreach (var rental in manager.Rentals)
    {
        string status = rental.IsActive ? "Active" : "Completed";
        Console.WriteLine(
            $"Rental {rental.Id}: {rental.Customer.Name}, {rental.Bike.Model}, " +
            $"status: {status}, price: {rental.TotalPrice} PLN");
    }
}
