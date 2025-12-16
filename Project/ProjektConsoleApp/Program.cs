using Project.Model;
using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Dal;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
}).Build();


var context = _host.Services.GetService<ApplicationDbContext>();
if (context != null)
{
    context.Database.Migrate();
    if (!context.Pharmacies.Any())
    {
        //Dane do pierwszej apteki

        Address adr1 = new Address("Czestochowa", "Kilinskiego", "00-000", 15);
        Pharmacy phar1 = new Pharmacy("Ładna apteka", adr1);
        Employee e1 = new Employee("Jan", "Kowalski", "Wlasciciel", phar1);
        Employee e2 = new Employee("Olek", "Szczepanik", "Magister Farmacji", phar1);
        Employee e3 = new Employee("Mateusz", "Wyrazik", "Technik Farmacji", phar1);
        Drug d1 = new Drug("Apap", "Przeciwbólowy", "12.50zł", "Przeciwbolowy lek oparty na paracetamolu", phar1);
        Drug d2 = new PrescriptionDrug("Betesda", "Przeciwdepresyjny", "50.21zł", "Antydepresyjny lek", phar1);
        Drug d3 = new PrescriptionDrug("Abirateron", "Przeciwnowotworowe", "1200.51zł", "Stosowany w leczeniu raka", phar1);
        Drug d4 = new Drug("Paracetamol", "Przeciwbólowy", "12.99zł", "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy", phar1);
        phar1.Employees.Add(e1);
        phar1.Employees.Add(e2);
        phar1.Employees.Add(e3);
        phar1.Drugs.Add(d1);
        phar1.Drugs.Add(d2);
        phar1.Drugs.Add(d3);
        phar1.Drugs.Add(d4);
        context.Pharmacies.Add(phar1);
        context.SaveChanges();
    }
}
void whatsNext()
{
    Console.WriteLine(
    """
    1.Sekcja: Informacje ogolne
    2.Sekcja: Pracownicy
    3.Sekcja: Leki
    4.Powrot do wyboru aptek
    5.Wyjście z programu
    """
    );
    Console.Write("Którą z opcji wybierasz:");
}
void sekcjaPracownicy()
{
    Console.WriteLine("-------------------Sekcja Pracownicy-------------------");
    Console.WriteLine("1.Wyswietl wszystkich pracownikow.\n" +
        "2.Dodaj nowego pracownika do apteki.\n" +
        "3.Usun pracownika z apteki - usuwamy podając jego id\n" +
        "4.Wróć do wyboru z opcji z apteki\n" +
        "5.Wróć do wyboru apteki.");
    Console.Write("Którą z opcji wybierasz: ");
}
static void sekcjaLeki()
{
    Console.WriteLine("-------------------Sekcja Leki-------------------");
    Console.WriteLine("1.Wyswietl wszystkie dostępne leki.\n" +
        "2.Dodaj nowy lek do apteki.\n" +
        "3.Usun lek z apteki - usuwamy po id\n" +
        "4.Wypisz alfabetycznie wszystkie leki\n" +
        "5.Wypisz posortowane leki po typie\n" +
        "6.Wróc do wyboru opcji z apteki\n" +
        "7.Wróc do wyboru apteki"
    );
    Console.Write("Którą z opcji wybierasz: ");
}
// Źródła danych!!'
IPharmaciesSource pharmSource = new PharmaciesInDataBase(context);
ISourceEmployee empSource = new EmpInDataBase(context);
IDrugsSource drugSource = new DrugsInDataBase(context);
// Manager lekow pracownikow aptek

EmployeeManager empManager = new EmployeeManager(empSource);
DrugManager drugManager = new DrugManager(drugSource);
PharmacyChain pharChain = new PharmacyChain(pharmSource);
var pharmacies = pharmSource.AllPharmacies();

bool start_program = true;
while (start_program)
{
    //Pobranie wszystkich aptek
    Console.Clear();
    Console.WriteLine($"Witamy w systemie do zarzadzania siecią aptek aktualnie posiadamy {pharmacies.Count} aptek.");
    Console.WriteLine(pharChain);

    Console.Write("Podaj którą apteke wybierasz - po id: ");
    if (!int.TryParse(Console.ReadLine(), out int numer))
    {
        Console.Clear();
        Console.WriteLine("Nie podales cyfry.");
        Console.WriteLine("Nacisnij enter aby kontynuoowac");
        Console.ReadKey();
        continue;
    }
    Pharmacy? choosed = null;
    choosed = pharmacies.FirstOrDefault(x => x.Id == numer)!;
    if (choosed == null)
    {
        Console.Clear();
        Console.WriteLine("Nie znaleziono apteki o takim Id. Sprobuj ponownie");
        Console.WriteLine("Nacisnij enter aby kontynuowac");
        Console.ReadKey();
        continue;
    }
    Console.Clear();
    bool kontynuacja = true;
    while (kontynuacja)
    {
        Console.WriteLine($"Wybrales apteke: {choosed}");
        whatsNext();
        if (!int.TryParse(Console.ReadLine(), out int wybor))
        {
            Console.WriteLine("Nie podales cyfry, nacisnij enter aby kontynuoowac");
            Console.ReadKey();
            Console.Clear();
            continue;
        }
        switch (wybor)
        {
            case 1:
                Console.WriteLine(choosed.Address.ToString());
                Console.WriteLine("\nNacisnij enter aby kontynuowac");
                Console.ReadKey();
                Console.Clear();
                break;
            case 2:
                bool kontynuacja2 = true;
                while (kontynuacja2)
                {
                    Console.Clear();
                    sekcjaPracownicy();
                    if (!int.TryParse(Console.ReadLine(), out int wybor1))
                    {
                        Console.WriteLine("Nie wpisałeś cyfry");
                        Console.WriteLine("Nacisnij enter aby kontynuowac");
                        Console.ReadKey();
                        continue;
                    }
                    switch (wybor1)
                    {
                        case 1:
                            foreach (var e in empSource.AllEmployees().Where(x => x.Pharmacy == choosed))
                            {
                                Console.WriteLine(e);
                            }
                            Console.WriteLine("\n" + "Nacisnij enter aby kontynuowac");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Write("Podaj imie nowego pracownika: ");
                            string? imie = Console.ReadLine();
                            Console.Write("Podaj nazwisko nowego pracownika: ");
                            string? nazwisko = Console.ReadLine();
                            Console.Write("Podaj stanowisko nowego pracownika: ");
                            string? stanowisko = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) || string.IsNullOrWhiteSpace(stanowisko))
                            {
                                Console.WriteLine("Nic nie wpisales w ktoras rubryczke. Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                                continue;
                            }
                            if (empManager.AddEmployee(imie, nazwisko, stanowisko, choosed))
                            {
                                Console.WriteLine($"Pomyslnie dodano nowego pracownika {imie}, {nazwisko}, {stanowisko} Do apteki {choosed.Id}");
                                Console.WriteLine("Nacisnij enter aby kontynuowac");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Blad podczas dodawania nowego pracownika");
                                Console.WriteLine("Nacisnij enter aby kontynuowac");
                                Console.ReadKey();
                                continue;
                            }
                            break;
                        case 3:
                            Console.Write("Podaj id pracownika do usuniecia: ");
                            if (!int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.WriteLine("Nie wpisales cyrfy!!!. Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                                continue;
                            }
                            else
                            {
                                if (empManager.RemoveEmployee(id, choosed))
                                {
                                    Console.WriteLine("Pomyslnie usunięto pracownika. Nacisnij enter aby kontunyoowac");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Błąd podczas usuwania pracownika. Nacisnij enter aby kontynuoowac");
                                    Console.ReadKey();
                                }
                            }
                            break;
                        case 4:
                            kontynuacja2 = false;
                            break;
                        case 5:
                            kontynuacja2 = false;
                            kontynuacja = false;
                            break;
                        default:
                            Console.WriteLine("Nie podales numeru z przedzialu <1;5>. Nacisnij enter aby kontynuoowac");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                Console.WriteLine("Nacisnij enter aby kontynuoowac");
                Console.ReadKey();
                Console.Clear();
                break;
            case 3:
                bool kontynuacja3 = true;
                while (kontynuacja3)
                {
                    Console.Clear();
                    sekcjaLeki();
                    if (!int.TryParse(Console.ReadLine(), out int wybor1))
                    {
                        Console.WriteLine("Nie wpisales liczby!!");
                        Console.ReadLine();
                        continue;
                    }
                    switch (wybor1)
                    {
                        case 1:
                            foreach (var d in drugSource.AllDrugs().Where(x => x.Pharmacy == choosed))
                            {
                                Console.WriteLine(d);
                            }
                            Console.WriteLine("Nacisnij enter aby kontynuowac...");
                            Console.ReadKey();
                            break;
                        case 2:
                            //bool czyRecepta
                            Console.Write("Podaj nazwe dodawanego leku: ");
                            string? nazwa = Console.ReadLine();
                            Console.Write("Podaj typ dodawanego leku: ");
                            string? typ = Console.ReadLine();
                            Console.Write("Podaj cene dodawanego leku: ");
                            string? cena = Console.ReadLine();
                            Console.Write("Podaj opis dodawanego leku: ");
                            string? opis = Console.ReadLine();
                            Console.Write("Czy ten lek jest na recepte (tak/nie): ");
                            string? warunek = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(nazwa) || string.IsNullOrWhiteSpace(typ) || string.IsNullOrWhiteSpace(cena) || string.IsNullOrWhiteSpace(opis) || string.IsNullOrWhiteSpace(warunek))
                            {
                                Console.WriteLine("Nie wprowadziles ktorejs z danych bądz wproawdziles to blednie");
                                Console.ReadLine();
                                continue;
                            }
                            else
                            {
                                if(warunek == "tak")
                                {
                                    if(drugManager.AddPrescriptionDrug(nazwa, typ, cena, opis, choosed))
                                    {
                                        Console.WriteLine($"Pomyslnie dodano nowy lek na recepte: {nazwa} {typ} {cena} {opis} do apteki {choosed.Id}");
                                    }else
                                    {
                                        Console.WriteLine("Posiadamy w aptece juz lek o takiej nazwie, dodawanie sie nie powiodlo.");
                                    }
                                }else if(warunek == "nie")
                                {
                                    if (drugManager.AddDrug(nazwa, typ, cena, opis, choosed))
                                    {
                                        Console.WriteLine($"Pomyslnie dodano nowy lek: {nazwa} {typ} {cena} {opis} do apteki {choosed.Id}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Posiadamy w aptece juz lek o takiej nazwie, dodawanie sie nie powiodlo.");
                                    }
                                }else
                                {
                                    Console.WriteLine("Podales w polu na recepte cos innego niz tak/nie");
                                }

                            }
                            Console.WriteLine("Nacisnij enter aby kontynuoowac");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Write("Podaj id leku do usuniecia: ");
                            if (!int.TryParse(Console.ReadLine(), out int doUsuniecia))
                            {
                                Console.WriteLine("nie wpisales cyfry. Nacisnij entery aby kontynuoowac");
                                Console.ReadKey();
                                continue;
                            }
                            else
                            {
                                if (drugManager.RemoveDrug(doUsuniecia, choosed))
                                {
                                    Console.WriteLine("Pomyslnie usunięto lek");
                                }
                                else
                                {
                                    Console.WriteLine("Błąd podczasa usuwania leku");
                                }
                            }
                            Console.WriteLine("Nacisnij enter aby kontynuowac");
                            Console.ReadKey();
                            break;
                        case 4:
                            drugManager.sortByFirstLetter(choosed);
                            Console.WriteLine("Nacisnij enter aby kontynuoowca");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            drugManager.sortByTypeOfDrug(choosed);
                            Console.WriteLine("Nacisnij enter aby kontynuoowca");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 6:
                            kontynuacja3 = false;
                            break;
                        case 7:
                            kontynuacja3 = false;
                            kontynuacja = false;
                            break;
                        default:
                            Console.WriteLine("Nie podales numeru z przedzialu <1;5>. Nacisnij enter aby kontynuoowac");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                Console.WriteLine("Naciśnij enter aby kontynuowac...");
                Console.ReadKey();
                Console.Clear();
                break;
            case 4:
                Console.Clear();
                kontynuacja = false;
                break;
            case 5:
                kontynuacja = false;
                start_program = false;
                break;
            default:
                Console.WriteLine("Nie podales liczby z przedzialu <1:5>");
                Console.ReadKey();
                Console.Clear();
                break;
        }
    }
}