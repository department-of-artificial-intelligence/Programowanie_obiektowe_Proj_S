using Project.Model;
using System.Runtime.Loader;
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
        "3.Usun lek z apteki - usuwamy po nazwie\n" +
        "4.Wypisz alfabetycznie wszystkie leki\n" +
        "5.Wypisz leki na recepte\n" +
        "6.Wróc do wyboru opcji z apteki\n" +
        "7.Wróc do wyboru apteki"
    );
    Console.Write("Którą z opcji wybierasz: ");
}
//Pracownicy
List<Employee> employees1 = new List<Employee>()
{
    new Employee(1, "Jan", "Kowalski", "Wlasciciel"),
    new Employee(2, "Olek", "Szczepanik", "Magister Farmacji"),
    new Employee(3, "Mateusz", "Wyrazik", "Technik Farmacji"),
};
ISourceEmployee pracownicy1 = new EmpInMemory(employees1);
IEmployeeManager empManager1 = new EmployeeManager(pracownicy1);

List<Employee> employees2 = new List<Employee>()
{
    new Employee(1, "Michal", "Kowalski", "Wlasciciel"),
    new Employee(2, "Piotr", "Szczepanik", "Magister Farmacji"),
    new Employee(3, "Bogumił", "Wyrazik", "Technik Farmacji"),
};
ISourceEmployee pracownicy2 = new EmpInMemory(employees2);
IEmployeeManager empManager2 = new EmployeeManager(pracownicy2);

List<Employee> employees3 = new List<Employee>()
{
    new Employee(1, "Kamil", "Kowalski", "Wlasciciel"),
    new Employee(2, "Patryk", "Szczepanik", "Magister Farmacji"),
    new Employee(3, "Kacper", "Wyrazik", "Technik Farmacji"),
};
ISourceEmployee pracownicy3 = new EmpInMemory(employees3);
IEmployeeManager empManager3 = new EmployeeManager(pracownicy3);

//Leki
List<Drug> leki1 = new List<Drug>()
{
    new Drug(1, "Apap", "Przeciwbolowy", "12.50zł", "Przeciwbolowy lek oparty na paracetamolu"),
    new PrescriptionDrug(2, "Betesda", "Przeciwdepresyjny", "50.21zł", "Antydepresyjny lek"),
    new PrescriptionDrug(3, "Abirateron", "Przeciwnowotworowe", "1200.51zł", "Stosowany w leczeniu raka"),
    new Drug(4, "Paracetamol", "Przeciwbólowy, Przeciwgorączkowy", "12.99zł", "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy")
};
IDrugsSource drugs1 = new DrugsInMemory(leki1);
IDrugManager drugManager1 = new DrugManager(drugs1);

List<Drug> leki2 = new List<Drug>()
{
    new Drug(1, "Loratadyna", "antyhistaminowy", "22.00zł", "Leczy objawy alergii: katar sienny, pokrzywka, swędzenie skóry."),
    new PrescriptionDrug(2, "Amoksycylina", "antybiotyk", "28.40zł", "Leczy infekcje bakteryjne: zapalenie oskrzeli, ucha, dróg moczowych."),
    new PrescriptionDrug(3, "Loperamid", "przeciwbiegunkowy", "9.90zł", "Zmniejsza częstotliwość wypróżnień, stosowany przy biegunce podróżnych."),
    new Drug(4, "Ibuprofen", "przeciwbólowy", "18.50zł", "Leczy ból, stan zapalny, gorączkę. Stosowany przy bólach mięśniowych, stawowych."),
};

IDrugsSource drugs2 = new DrugsInMemory(leki2);
IDrugManager drugManager2 = new DrugManager(drugs2);
List<Drug> leki3 = new List<Drug>()
{
    new Drug(1, "Ketonal", "przeciwbólowy", "24.90zł", "Silny lek na bóle stawów, mięśni i zębów. Działa przeciwzapalnie."),
    new Drug(2, "No-Spa", "rozkurczowy", "15.30zł", "Łagodzi skurcze mięśni gładkich brzucha, dróg żółciowych i moczowych."),
    new Drug(3, "Polopiryna S", "przeciwbólowy", "10.50zł", "Stosowana przy bólu, gorączce i przeziębieniu. Zawiera kwas acetylosalicylowy."),
    new PrescriptionDrug(4, "Metformina", "przeciwcukrzycowy", "19.80zł", "Pomaga regulować poziom cukru we krwi u osób z cukrzycą typu 2."),
    new PrescriptionDrug(5, "Somnifen", "lek nasenny", "33.20zł", "Pomaga zasnąć i poprawia jakość snu bez uczucia otępienia rano."),
    new PrescriptionDrug(6, "Bronchotil", "syrop na kaszel", "17.80zł", "Łagodzi kaszel mokry i ułatwia odkrztuszanie wydzieliny."),
    new Drug(7, "Ostevit", "suplement wapniowo-witaminowy", "29.50zł", "Wzmacnia kości i wspiera gospodarkę wapniową organizmu."),
    new Drug(8, "Gastroton", "lek na żołądek", "23.40zł", "Zmniejsza nadkwasotę i chroni błonę śluzową żołądka.")
};

IDrugsSource drugs3 = new DrugsInMemory(leki3);
IDrugManager drugManager3 = new DrugManager(drugs3);

//Adresy
Address phar1 = new Address("Warszawa", "Jana Kazimierza", "00-000", 120);
Address phar2 = new Address("Czestochowa", "Kilinskiego", "12-123", 20);
Address phar3 = new Address("Klobuck", "Orzeszkowej", "42-100", 50);
List<Pharmacy> pharmacies = new List<Pharmacy>()
{
    new Pharmacy(1, "Usmiechnieta Apteka",phar1, empManager1, drugManager1),
    new Pharmacy(2, "Smutna Apteka", phar2, empManager2, drugManager2),
    new Pharmacy(3, "Ladna Apteka", phar3, empManager3, drugManager3),
};
IPharmaciesSource source = new PharmaciesInMemory(pharmacies);
PharmacyChain all_pharmacies = new PharmacyChain(source);
bool start_program = true;
while (start_program)
{
    Console.Clear();
    Console.WriteLine($"Witamy w systemie do zarzadzania siecią aptek aktualnie posiadamy {pharmacies.Count()} aptek.");
    Console.WriteLine(all_pharmacies);

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
                Console.WriteLine("\nNacisnij enter aby kontynuoowac");
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
                        Console.WriteLine("Nacisnij enter aby kontynuoowac");
                        Console.ReadKey();
                        continue;
                    }
                    switch (wybor1)
                    {
                        case 1:
                            choosed.Employees.DisplayEmployees();
                            Console.WriteLine("\n" + "Nacisnij enter aby kontynuoowac");
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
                            if (choosed.Employees.AddEmployee(imie, nazwisko, stanowisko))
                            {
                                Console.WriteLine($"Pomyslnie dodano nowego pracownika {imie}, {nazwisko}, {stanowisko}");
                                Console.WriteLine("Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Blad podczas dodawania nowego pracownika");
                                Console.WriteLine("Nacisnij enter aby kontynuoowac");
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
                                if (choosed.Employees.RemoveEmployee(id))
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
                            choosed.Drugs.DisplayDrugs();
                            Console.WriteLine("Nacisnij enter aby kontynuowac...");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Write("Podaj nazwe dodawanego leku: ");
                            string? nazwa = Console.ReadLine();
                            Console.Write("Podaj typ dodawanego leku: ");
                            string? typ = Console.ReadLine();
                            Console.Write("Podaj cene dodawanego leku: ");
                            string? cena = Console.ReadLine();
                            Console.Write("Podaj opis dodawanego leku: ");
                            string? opis = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(nazwa) || string.IsNullOrWhiteSpace(typ) || string.IsNullOrWhiteSpace(cena) || string.IsNullOrWhiteSpace(opis))
                            {
                                Console.WriteLine("Nie wprowadziles ktorejs z danych bądz wproawdziles to blednie");
                                continue;
                            }
                            else
                            {
                                if (choosed.Drugs.AddDrug(nazwa, typ, cena, opis))
                                {
                                    Console.WriteLine($"Pomyslnie dodano nowy lek: {nazwa} {typ} {cena} {opis}");
                                }
                                else
                                {
                                    Console.WriteLine("Posiadamy w aptece juz lek o takiej nazwie, dodawanie sie nie powiodlo");
                                }
                            }
                            Console.WriteLine("Nacisnij enter aby kontynuoowac");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Write("Podaj nazwe leku do usuniecia: ");
                            string? doUsuniecia = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(doUsuniecia))
                            {
                                Console.WriteLine("Nie wprowadziles poprawanie nazwy leku");
                            }
                            if (!choosed.Drugs.RemoveDrug(doUsuniecia!))
                            {
                                Console.WriteLine("Błąd podczas usuwania leku");
                            }
                            else
                            {
                                Console.WriteLine("Pomyslnie usunięto lek");
                            }
                            Console.WriteLine("Nacisnij enter aby kontynuoowac");
                            Console.ReadKey();
                            break;
                        case 4:
                            choosed.Drugs.sortByFirstLetter();
                            Console.WriteLine("Nacisnij enter aby kontynuoowca");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            choosed.Drugs.sortWhetherDrugIsOnPrescription();
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