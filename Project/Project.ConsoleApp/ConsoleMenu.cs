using System;
using System.Linq;
using Project.Model;
using Project.Services.Interfaces;

namespace Project.ConsoleApp
{
    public class ConsoleMenu
    {
        private readonly IBicycleService _bikeService;
        private readonly IStationService _stationService;
        private readonly IPersonService _personService;
        private readonly IRentalService _rentalService;

        public ConsoleMenu(IBicycleService bs, IStationService ss, IPersonService ps, IRentalService rs)
        {
            _bikeService = bs;
            _stationService = ss;
            _personService = ps;
            _rentalService = rs;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- BICYCLE RENTAL SYSTEM ---");
                Console.WriteLine("1. BICYCLES (List, Add, Sort)");
                Console.WriteLine("2. STATIONS (List, Add)");
                Console.WriteLine("3. PEOPLE (Customers, Staff)");
                Console.WriteLine("4. RENTALS (Rent, Return, History)");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect option: ");

                switch (Console.ReadLine())
                {
                    case "1": BicycleMenu(); break;
                    case "2": StationMenu(); break;
                    case "3": PersonMenu(); break;
                    case "4": RentalMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void BicycleMenu()
        {
            Console.Clear();
            Console.WriteLine("--- BICYCLE MANAGER ---");
            Console.WriteLine("1. Show All (Default)");
            Console.WriteLine("2. Show Sorted by Price (Cheapest first)");
            Console.WriteLine("3. Show Sorted by Price (Expensive first)");
            Console.WriteLine("4. Show Only Electric");
            Console.WriteLine("5. Add New Bike");
            Console.Write("Select: ");

            var input = Console.ReadLine();

            if (input == "5") { AddNewBike(); return; }

            var bikes = _bikeService.GetAllBicycles();

            switch (input)
            {
                case "2": bikes = bikes.OrderBy(b => b.Price).ToList(); break;
                case "3": bikes = bikes.OrderByDescending(b => b.Price).ToList(); break;
                case "4": bikes = bikes.Where(b => b.Type == BicycleType.Electric).ToList(); break;
            }

            Console.WriteLine($"\nBicycles List ({bikes.Count}):");
            foreach (var b in bikes)
            {
                // Заміна галочок на слова
                string status = b.Status == BicycleStatus.Available ? "[Available]" : "[Rented]";
                string extra = b.Type == BicycleType.Electric ? $" [Battery: {b.BatteryLevel}%]" : "";

                Console.WriteLine($"ID:{b.Id} | {b.Model} ({b.Type}){extra} | {b.Price} PLN/h | {status}");
            }
            Pause();
        }

        private void StationMenu()
        {
            Console.Clear();
            Console.WriteLine("--- STATION MANAGER ---");
            Console.WriteLine("1. Show All");
            Console.WriteLine("2. Show Sorted by City");
            Console.WriteLine("3. Add Station");
            Console.WriteLine("4. Park Bike (Move bike to station)"); 
            Console.Write("Select: ");

            var input = Console.ReadLine();

            if (input == "3") { AddNewStation(); return; }

           
            if (input == "4")
            {
                Console.Write("Enter Station ID: "); int sid = ParseInt();
                Console.Write("Enter Bike ID: "); int bid = ParseInt();
                Console.WriteLine(_stationService.ParkBicycle(sid, bid));
                Pause();
                return;
            }
           
            var stations = _stationService.GetAllStations();
            if (input == "2") stations = stations.OrderBy(s => s.City).ToList();

            Console.WriteLine($"\nStations List:");
            foreach (var s in stations)
            {
                int count = s.Bicycles?.Count ?? 0;
                string full = count >= s.Capacity ? "[FULL]" : "";

               
                string bikeIds = string.Join(", ", s.Bicycles.Select(b => b.Id));

                Console.WriteLine($"ID:{s.Id} | {s.Name} | Bikes: {count}/{s.Capacity} [{bikeIds}] {full}");
            }
            Pause();
        }

        private void PersonMenu()
        {
            Console.Clear();
            Console.WriteLine("--- PERSON MANAGER ---");
            Console.WriteLine("1. Show All");
            Console.WriteLine("2. Show Customers Only");
            Console.WriteLine("3. Show Employees Only");
            Console.WriteLine("4. Add Customer");
            Console.WriteLine("5. Add Employee");
            Console.Write("Select: ");

            var input = Console.ReadLine();
            if (input == "4") { AddCustomer(); return; }
            if (input == "5") { AddEmployee(); return; }

            var people = _personService.GetAllPeople();

            if (input == "2") people = people.OfType<Customer>().Cast<Person>().ToList();
            if (input == "3") people = people.OfType<Employee>().Cast<Person>().ToList();

            Console.WriteLine("\nPeople List:");
            foreach (var p in people)
            {
                string role = p is Customer ? "Customer" : "Staff";
                Console.WriteLine($"ID:{p.Id} | {p.FirstName} {p.LastName} | Role: {role}");
            }
            Pause();
        }

        private void RentalMenu()
        {
            Console.Clear();
            Console.WriteLine("--- RENTAL OPERATIONS ---");
            Console.WriteLine("1. Rent a Bike");
            Console.WriteLine("2. Return a Bike");
            Console.WriteLine("3. View History");
            Console.Write("Select: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter Customer ID: "); int cid = ParseInt();
                    Console.Write("Enter Bike ID: "); int bid = ParseInt();
                    Console.WriteLine(_rentalService.RentBicycle(bid, cid));
                    break;

                case "2":
                    Console.Write("Enter Bike ID to return: "); int rbid = ParseInt();
                    Console.Write("Enter Station ID where you are: "); int sid = ParseInt();
                    Console.WriteLine(_rentalService.ReturnBicycle(rbid, sid));
                    break;

                case "3":
                    var history = _rentalService.GetHistory();
                    Console.WriteLine("\nRENTAL HISTORY:");
                    foreach (var h in history)
                    {
                        string status = h.ReturnDate == null ? "IN USE" : $"DONE ({h.TotalCost:F2} PLN)";
                        Console.WriteLine($"#{h.Id} | {h.RentDate:g} | {h.Customer?.FirstName} {h.Customer?.LastName} -> {h.Bicycle?.Model} | {status}");
                    }
                    break;
            }
            Pause();
        }

        

        private void AddNewBike()
        {
            Console.Write("Model: "); string m = Console.ReadLine();
            Console.WriteLine("Type: 0-City, 1-Mountain, 2-Electric, 3-Road");
            Console.Write("Select Type ID: ");
            if (Enum.TryParse(Console.ReadLine(), out BicycleType t))
            {
                Console.Write("Price per hour (PLN): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal p))
                {
                    int? bat = null, range = null;
                    if (t == BicycleType.Electric)
                    {
                        Console.Write("Battery: "); bat = ParseInt();
                        Console.Write("Range: "); range = ParseInt();
                    }
                    _bikeService.AddBicycle(m, t, p, bat, range);
                }
            }
            Pause();
        }

        private void AddNewStation()
        {
            Console.Write("Name: "); string n = Console.ReadLine();
            Console.Write("City: "); string c = Console.ReadLine();
            Console.Write("Address: "); string a = Console.ReadLine();
            Console.Write("Capacity: "); int cap = ParseInt();
            _stationService.AddStation(n, c, a, cap);
            Pause();
        }

        private void AddCustomer()
        {
            Console.Write("First Name: "); string fn = Console.ReadLine();
            Console.Write("Last Name: "); string ln = Console.ReadLine();
            Console.Write("Address: "); string ad = Console.ReadLine();
            _personService.AddCustomer(fn, ln, ad);
            Console.WriteLine("Customer saved.");
            Pause();
        }

        private void AddEmployee()
        {
            Console.Write("First Name: "); string fn = Console.ReadLine();
            Console.Write("Last Name: "); string ln = Console.ReadLine();
            Console.WriteLine("Role: 0-Manager, 1-Mechanic");
            Console.Write("Select Role ID: ");
            EmployeeRole r = (EmployeeRole)ParseInt();
            _personService.AddEmployee(fn, ln, r);
            Console.WriteLine("Employee saved.");
            Pause();
        }

        private int ParseInt()
        {
            if (int.TryParse(Console.ReadLine(), out int res)) return res;
            return 0;
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}