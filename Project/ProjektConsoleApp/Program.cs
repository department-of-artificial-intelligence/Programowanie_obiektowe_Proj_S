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
        "5.Wypisz leki posortowane po typie leku\n" +
        "6.Wróc do wyboru opcji z apteki\n" +
        "7.Wróc do wyboru apteki"
    );
    Console.Write("Którą z opcji wybierasz: ");
}
//Pracownicy
List<Employee> employees1 = new List<Employee>()
{
    new Employee(0, "Jan", "Kowalski", "Wlasciciel"),
    new Employee(1, "Olek", "Szczepanik", "Magister Farmacji"),
    new Employee(2, "Mateusz", "Wyrazik", "Technik Farmacji"),
};
ISourceEmployee pracownicy1 = new EmpInMemory(employees1);
IEmployeeManager empManager1 = new EmployeeManager(pracownicy1);

List<Employee> employees2 = new List<Employee>()
{
    new Employee(0, "Michal", "Kowalski", "Wlasciciel"),
    new Employee(1, "Piotr", "Szczepanik", "Magister Farmacji"),
    new Employee(2, "Bogumił", "Wyrazik", "Technik Farmacji"),
};
ISourceEmployee pracownicy2 = new EmpInMemory(employees2);
IEmployeeManager empManager2 = new EmployeeManager(pracownicy2);

List<Employee> employees3 = new List<Employee>()
{
    new Employee(0, "Kamil", "Kowalski", "Wlasciciel"),
    new Employee(1, "Patryk", "Szczepanik", "Magister Farmacji"),
    new Employee(2, "Kacper", "Wyrazik", "Technik Farmacji"),
};
ISourceEmployee pracownicy3 = new EmpInMemory(employees3);
IEmployeeManager empManager3 = new EmployeeManager(pracownicy3);

//Leki
List<Drug> leki1 = new List<Drug>()
{
    new Drug("Apap", "Przeciwbolowy", "12.50zł", "Przeciwbolowy lek oparty na paracetamolu"),
    new Drug("Betesda", "Przeciwdepresyjny", "50.21zł", "Antydepresyjny lek"),
    new Drug("Abirateron", "Przeciwnowotworowe", "1200.51zł", "Stosowany w leczeniu raka"),
    new Drug("Paracetamol", "Przeciwbólowy, Przeciwgorączkowy", "12.99zł", "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy")
};
IDrugsSource drugs1 = new DrugsInMemory(leki1);
IDrugManager drugManager1 = new DrugManager(drugs1);

List<Drug> leki2 = new List<Drug>()
{
    new Drug("Loratadyna", "antyhistaminowy", "22.00zł", "Leczy objawy alergii: katar sienny, pokrzywka, swędzenie skóry."),
    new Drug("Amoksycylina", "antybiotyk", "28.40zł", "Leczy infekcje bakteryjne: zapalenie oskrzeli, ucha, dróg moczowych."),
    new Drug("Loperamid", "przeciwbiegunkowy", "9.90zł", "Zmniejsza częstotliwość wypróżnień, stosowany przy biegunce podróżnych."),
    new Drug("Ibuprofen", "przeciwbólowy, przeciwzapalny", "18.50zł", "Leczy ból, stan zapalny, gorączkę. Stosowany przy bólach mięśniowych, stawowych."),
};

IDrugsSource drugs2 = new DrugsInMemory(leki2);
IDrugManager drugManager2 = new DrugManager(drugs2);
List<Drug> leki3 = new List<Drug>()
{
    new Drug("Ketonal", "przeciwbólowy, przeciwzapalny", "24.90zł", "Silny lek na bóle stawów, mięśni i zębów. Działa przeciwzapalnie."),
    new Drug("No-Spa", "rozkurczowy", "15.30zł", "Łagodzi skurcze mięśni gładkich brzucha, dróg żółciowych i moczowych."),
    new Drug("Polopiryna S", "przeciwbólowy, przeciwzapalny", "10.50zł", "Stosowana przy bólu, gorączce i przeziębieniu. Zawiera kwas acetylosalicylowy."),
    new Drug("Metformina", "przeciwcukrzycowy", "19.80zł", "Pomaga regulować poziom cukru we krwi u osób z cukrzycą typu 2."),
    new Drug("Somnifen", "lek nasenny", "33.20zł", "Pomaga zasnąć i poprawia jakość snu bez uczucia otępienia rano."),
    new Drug("Bronchotil", "syrop na kaszel", "17.80zł", "Łagodzi kaszel mokry i ułatwia odkrztuszanie wydzieliny."),
    new Drug("Ostevit", "suplement wapniowo-witaminowy", "29.50zł", "Wzmacnia kości i wspiera gospodarkę wapniową organizmu."),
    new Drug("Gastroton", "lek na żołądek", "23.40zł", "Zmniejsza nadkwasotę i chroni błonę śluzową żołądka.")
};

IDrugsSource drugs3 = new DrugsInMemory(leki3);
IDrugManager drugManager3 = new DrugManager(drugs2);

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
    all_pharmacies.displayAllPharmacies();

    Console.Write("Podaj którą apteke wybierasz - po id: ");
    if (!int.TryParse(Console.ReadLine(), out int numer))
    {
        Console.Clear();
        Console.WriteLine("Nie podales cyrfy a id jest cyfra");
        Console.WriteLine("Nacisnij enter aby kontynuoowac");
        Console.ReadKey();
        continue;
    }
    Pharmacy? choosed = null;
    choosed = pharmacies.FirstOrDefault(x => x.Id == numer)!;
    if (choosed == null)
    {
        Console.Clear();
        Console.WriteLine("Nie znaleziono apteki o takim Id. Sproboj ponownie");
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
            Console.WriteLine("Nie podales cyrfy, nacisnij enter aby kontynuoowac");
            Console.ReadKey();
            Console.Clear();
            continue;
        }
        switch (wybor)
        {
            case 1:
                choosed.Address.DisplayAddress();
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
                            if (!choosed.Employees.AddEmployee())
                            {
                                Console.WriteLine("Wystapil blad podczas dodawania nowego pracownika\n" +
                                    "Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Pomyslnie dodano nowego pracownika do apteki!");
                                Console.WriteLine("Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            if (!choosed.Employees.DeleteEmployee())
                            {
                                Console.WriteLine("Wystąpił błąd podczas usuwania pracownika.");
                                Console.WriteLine("Nacisnij enter aby kontynuowac");
                                Console.ReadLine();
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Pomyślnie usunięto pracownika z apteki");
                                Console.WriteLine("Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
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
                            Console.WriteLine("Nie podales numeru z przedzialu <1;5>");
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
                            if (!choosed.Drugs.AddDrug())
                            {
                                Console.WriteLine("Dodawanie leku sie nie powiodło\n" +
                                    "Naciśnij enter aby kontynuowac...");
                                Console.ReadLine();
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Pomyślnie dodano nowy lek do apteki!");
                                Console.WriteLine("Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            if (!choosed.Drugs.DeleteDrug())
                            {
                                Console.WriteLine("Usuwanie leku sie nie powiodlo\n" +
                                    "Naciśnij enter aby kontynuoowac");
                                Console.ReadLine();
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Usuwanie leku się powiodlo!\n");
                                Console.WriteLine("Nacisnij enter aby kontynuoowac");
                                Console.ReadKey();
                            }
                            break;
                        case 4:
                            choosed.Drugs.sortByFirstLetter();
                            Console.WriteLine("Nacisnij enter aby kontynuoowca");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            choosed.Drugs.sortByTypeOfDrug();
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
                            Console.WriteLine("Wpisales liczbe nie z przedzialu <1;5>!");
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
                Console.WriteLine("Nie podales liczby z przedzialu <1:6>");
                break;
        }
    }
}