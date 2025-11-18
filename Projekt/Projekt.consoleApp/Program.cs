using Projekt.Model;
public class Program
{
    //Dane

    static List<Branch> BazaOddzialow = new List<Branch>();
    static List<Car> BazaSamochodow = new List<Car>();
    static List<Customer> BazaKlientow = new List<Customer>();
    static List<Employee> BazaPracownikow = new List<Employee>();
    static List<Rental> BazaWypozyczen = new List<Rental>();

    static int nextBranchId = 1;
    static int nextCarId = 1;
    static int nextCustomerId = 1;
    static int nextEmployeeId = 1;
    static int nextRentalId = 1;

    public void Main()
    {
        InicjalizujDane();

        MenuGlowne();
    }
    public void InicjalizujDane()
    {
        //Oddziały

        var oddzialCzew = new Branch
        {
            Id = nextBranchId++
            Name = "Częstochowa Centrum"
            Address = "ul. Warszawska 31"
        };
        var oddzialWawa = new Branch
        {
            Id = nextBranchId++,
            Name = "Warszawa Centrum",
            Address = "ul. Marszałkowska 1"
        };
        var oddzialKrk = new Branch
        {
            Id = nextBranchId++,
            Name = "Kraków Rynek",
            Address = "ul. Floriańska 2"
        };
        BazaOddzialow.AddRange(new[] { oddzialWawa, oddzialKrk });

        //Samochody

        var car1 = new Car 
        { 
            Id = nextCarId++,
            Marka = "Toyota",
            Model = "Yaris",
            Year = 2022,
            RegistrationNumber = "WA 12345",
            DailyRate = 100,
            Status = CarStatus.Available,
            CurrentBranchId = oddzialWawa.Id,
            CurrentBranch = oddzialWawa 
        };
        var car2 = new Car 
        { 
            Id = nextCarId++,
            Marka = "Skoda",
            Model = "Octavia",
            Year = 2023,
            RegistrationNumber = "KR 54321",
            DailyRate = 150,
            Status = CarStatus.Available,
            CurrentBranchId = oddzialKrk.Id,
            CurrentBranch = oddzialKrk
        };
        var car3 = new Car 
        { 
            Id = nextCarId++,
            Marka = "Ford",
            Model = "Mondeo",
            Year = 2021,
            RegistrationNumber = "WA 67890",
            DailyRate = 180,
            Status = CarStatus.Rented,
            CurrentBranchId = oddzialWawa.Id,
            CurrentBranch = oddzialWawa 
        };
        var car4 = new Car 
        { 
            Id = nextCarId++,
            Marka = "BMW",
            Model = "X5",
            Year = 2023,
            RegistrationNumber = "KR 98765",
            DailyRate = 300,
            Status = CarStatus.InService,
            CurrentBranchId = oddzialKrk.Id,
            CurrentBranch = oddzialKrk 
        };
        var car5 = new Car
        {
            Id = nextCarId++,
            Marka = "BMW",
            Model = "Seria 5",
            Year = 2025,
            RegistrationNumber = "SC 345CL",
            DailyRate = 500,
            Status = CarStatus.Available,
            CurrentBranchId = oddzialCzew.Id,
            CurrentBranch = oddzialCzew
        };
        BazaSamochodow.AddRange(new[] { car1, car2, car3, car4, car5 });

        oddzialWawa.Cars.Add(car1);
        oddzialWawa.Cars.Add(car3);
        oddzialKrk.Cars.Add(car2);
        oddzialKrk.Cars.Add(car4);
        oddzialCzew.Cars.Add(car5);

        //Klient

        var cust1 = new Customer
        { 
            Id = nextCustomerId++,
            FirstName = "Jan",
            LastName = "Kowalski",
            PhoneNumber = "111222333",
            DateOfBirth = new DateTime(1990, 5, 15) 
        };
        var cust2 = new Customer
        { 
            Id = nextCustomerId++,
            FirstName = "Anna",
            LastName = "Nowak",
            PhoneNumber = "444555666",
            DateOfBirth = new DateTime(1985, 10, 2) 
        };
        BazaKlientow.AddRange(new[] { cust1, cust2 });
        
        //Sprzedawcy

        var emp1 = new Employee 
        { 
            Id = nextEmployeeId++,
            FirstName = "Piotr",
            LastName = "Zieliński",
            BranchId = oddzialWawa.Id,
            Branch = oddzialWawa
        };
        var emp2 = new Employee 
        { 
            Id = nextEmployeeId++,
            FirstName = "Ewa",
            LastName = "Wiśniewska",
            BranchId = oddzialKrk.Id,
            Branch = oddzialKrk 
        };
        var emp3 = new Employee
        {
            Id = nextEmployeeId++,
            FirstName = "Jan",
            LastName = "Krawczyk",
            BranchId = oddzialCzew.Id,
            Branch = oddzialCzew
        };
        BazaPracownikow.AddRange(new[] { emp1, emp2 });
        oddzialWawa.Employees.Add(emp1);
        oddzialKrk.Employees.Add(emp2);
        oddzialCzew.Employees.Add(emp3);
        
        //Wypozyczenie

        var rental1 = new Rental
        {
            Id = nextRentalId++,
            CustomerId = cust1.Id,
            Customer = cust1,
            CarId = car3.Id,
            Car = car3,
            PickupBranchId = oddzialWawa.Id,
            PickupBranch = oddzialWawa,
            StartDate = DateTime.Now.AddDays(-2),
            EndDate = DateTime.Now.AddDays(3),
            Status = RentalStatus.Active,
            ActualReturnDate = null,
            TotalCost = 0
        };
        BazaWypozyczen.Add(rental1);
        cust1.RentalHistory.Add(rental1);
    }
    static void MenuGlowne()
    {
        bool dziala = true;
        while (dziala)
        {
            Console.Clear();
            Console.WriteLine("======= SYSTEM ZARZĄDZANIA WYPOŻYCZALNIĄ =======");
            Console.WriteLine("1. Wypożycz samochód");
            Console.WriteLine("2. Zwróć samochód");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("3. Pokaż wszystkie samochody");
            Console.WriteLine("4. Pokaż tylko dostępne samochody");
            Console.WriteLine("5. Pokaż oddziały");
            Console.WriteLine("6. Pokaż klientów");
            Console.WriteLine("7. Pokaż historię wypożyczeń");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("9.  Zakończ program");
            Console.Write("\nWybierz opcję: ");

            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    WypozyczSamochod();
                    break;
                case "2":
                    ZwrocSamochod();
                    break;
                case "3":
                    PokazWszystkieSamochody();
                    break;
                case "4":
                    PokazDostepneSamochody();
                    break;
                case "5":
                    PokazOddzialy();
                    break;
                case "6":
                    PokazKlientow();
                    break;
                case "7":
                    PokazHistorie();
                    break;
                case "9":
                    dziala = false;
                    break;
                default:
                    Powiadomienie("Nieznana opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
    static void WypozyczSamochod()
    {
        Console.Clear();
        Console.WriteLine("---- Nowe wypożyczenie ----");

        Console.WriteLine("Z którego oddziału chcesz odebrac auto?");
        PokazOddzialy(false);
        Console.Write("Podaj ID oddziału: ");
        int idOddzialu;
        if (!int.TryParse(Console.ReadLine(), out idOddzialu))
        {
            Powiadomienie("Błędne ID. Anulowano.");
            return;
        }
        var oddzial = BazaOddzialow.FirstOrDefault(b => b.Id == idOddzialu);
        if (oddzial == null)
        {
            Powiadomienie("Nie ma takiego oddziału. Anulowano.");
            return;
        }

        Console.WriteLine($"\nDostępne samochody w {oddzial.Name}:");
        var dostepneAuta = oddzial.Cars.Where(c => c.Status == CarStatus.Available).ToList();
        
        if (!dostepneAuta.Any())
        {
            Powiadomienie("Niestety, brak dostępnych aut w tym oddziale.");
            return;
        }

        foreach (var auto in dostepneAuta)
        {
            Console.WriteLine(auto.ToString());
        }

        Console.Write("Podaj ID samochodu: ");
        int idAuta;
        if (!int.TryParse(Console.ReadLine(), out idAuta))
        {
            Powiadomienie("Błędne ID. Anulowano.");
            return;
        }

        var samochod = dostepneAuta.FirstOrDefault(c => c.Id == idAuta);
        if (samochod == null)
        {
            Powiadomienie("Nie ma takiego dostępnego auta. Anulowano.");
            return;
        }

        Console.WriteLine("\nKlienci w systemie:");
        PokazKlientow(false);
        Console.Write("Podaj ID klienta: ");

        int idKlienta;
        if (!int.TryParse(Console.ReadLine(), out idKlienta))
        {
            Powiadomienie("Błędne ID. Anulowano.");
            return;
        }

        var klient = BazaKlientow.FirstOrDefault(k => k.Id == idKlienta);
        if (klient == null)
        {
            Powiadomienie("Nie ma takiego klienta. Anulowano.");
            return;
        }
        Console.Write("Na ile dni chcesz wypożyczyć? ");
        int dni;
        if (!int.TryParse(Console.ReadLine(), out dni) || dni <= 0)
        {
            Powiadomienie("Błędna liczba dni. Anulowano.");
            return;
        }

        DateTime dataStart = DateTime.Now;
        DateTime dataKoniec = dataStart.AddDays(dni);
        decimal koszt = samochod.DailyRate * dni;

        Console.WriteLine($"\n--- PODSUMOWANIE ---");
        Console.WriteLine($"Klient: {klient.FirstName} {klient.LastName}");
        Console.WriteLine($"Samochód: {samochod.Marka} {samochod.Model}");
        Console.WriteLine($"Okres: {dataStart.ToShortDateString()} do {dataKoniec.ToShortDateString()} ({dni} dni)");
        Console.WriteLine($"Całkowity koszt: {koszt:C}");
        Console.Write("Potwierdzasz? (T/N): ");

        if (Console.ReadLine().ToUpper() != "T")
        {
            Powiadomienie("Anulowano.");
            return;
        }

        samochod.Status = CarStatus.Rented;

        var noweWypozyczenie = new Rental
        {
            Id = nextRentalId++,
            CustomerId = klient.Id,
            Customer = klient,
            CarId = samochod.Id,
            Car = samochod,
            PickupBranchId = oddzial.Id,
            PickupBranch = oddzial,
            StartDate = dataStart,
            EndDate = dataKoniec,
            TotalCost = koszt,
            Status = RentalStatus.Active,
            ActualReturnDate = null
        };

        BazaWypozyczen.Add(noweWypozyczenie);
        klient.RentalHistory.Add(noweWypozyczenie);

        Powiadomienie($"\nSUKCES! Samochód {samochod.Marka} został wypożyczony.");
    }
}