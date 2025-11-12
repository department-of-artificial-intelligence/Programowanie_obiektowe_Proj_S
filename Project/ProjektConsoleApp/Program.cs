// See https://aka.ms/new-console-template for more information
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
    new Drug("Apap", "Przeciwbolowy", 12.50, "Przeciwbolowy lek oparty na paracetamolu"),
    new Drug("Betesda", "Przeciwdepresyjny", 50.21, "Antydepresyjny lek"),
    new Drug("Abirateron", "Przeciwnowotworowe", 1200.51, "Stosowany w leczeniu raka"),
    new Drug("Paracetamol", "Przeciwbólowy, Przeciwgorączkowy", 12.99, "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy")
};
IDrugsSource drugs1 = new DrugsInMemory(leki1);
IDrugManager drugManager1 = new DrugManager(drugs1);

List<Drug> leki2 = new List<Drug>()
{
    new Drug("Loratadyna", "antyhistaminowy", 22.00, "Leczy objawy alergii: katar sienny, pokrzywka, swędzenie skóry."),
    new Drug("Amoksycylina", "antybiotyk", 28.40, "Leczy infekcje bakteryjne: zapalenie oskrzeli, ucha, dróg moczowych."),
    new Drug("Loperamid", "przeciwbiegunkowy", 9.90, "Zmniejsza częstotliwość wypróżnień, stosowany przy biegunce podróżnych."),
    new Drug("Ibuprofen", "przeciwbólowy, przeciwzapalny", 18.50, "Leczy ból, stan zapalny, gorączkę. Stosowany przy bólach mięśniowych, stawowych."),
};

IDrugsSource drugs2 = new DrugsInMemory(leki2);
IDrugManager drugManager2 = new DrugManager(drugs2);
List<Drug> leki3 = new List<Drug>()
{
    new Drug("Ketonal", "przeciwbólowy, przeciwzapalny", 24.90, "Silny lek na bóle stawów, mięśni i zębów. Działa przeciwzapalnie."),
    new Drug("No-Spa", "rozkurczowy", 15.30, "Łagodzi skurcze mięśni gładkich brzucha, dróg żółciowych i moczowych."),
    new Drug("Polopiryna S", "przeciwbólowy, przeciwzapalny", 10.50, "Stosowana przy bólu, gorączce i przeziębieniu. Zawiera kwas acetylosalicylowy."),
    new Drug("Metformina", "przeciwcukrzycowy", 19.80, "Pomaga regulować poziom cukru we krwi u osób z cukrzycą typu 2.")
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
bool start_program = true;
bool kontynuacja = true;
while(start_program)
{
    Console.WriteLine("Witamy w systemie do zarzadzania aptekami");
    IPharmaciesSource source = new PharmaciesInMemory(pharmacies);
    IPharmacyChain all_pharmacies = new PharmacyChain(source);
    all_pharmacies.displayAllPharmacies();

    Console.Write("Podaj którą apteke wybierasz - po id: ");
    if (!int.TryParse(Console.ReadLine(), out int numer))
    {
        Console.WriteLine("Nie podales cyrfy a id jest cyfra");
        //continue
    }
    Pharmacy? choosed = null;
    choosed = pharmacies.FirstOrDefault(x => x.Id == numer)!;
    while(kontynuacja)
    {
        Console.WriteLine($"Wybrales apteke: {choosed}");
        whatsNext();
        if (!int.TryParse(Console.ReadLine(), out int wybor))
        {
            Console.WriteLine("Nie podales cyrfy, nacisnij enter aby kontynuoowac");
            Console.ReadKey();
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

            case 3:
                break;
            case 4:
                kontynuacja = false;
                break;
            case 5:
                kontynuacja = false;
                start_program = false;
                break;
        }
    }
}

