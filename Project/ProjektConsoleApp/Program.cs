// See https://aka.ms/new-console-template for more information
using Project.Model;
using System.Runtime.Loader;
List<Pharmacy> pharmacies = new List<Pharmacy>()
{
    new Pharmacy(1, "Usmiechnieta Apteka"),
    new Pharmacy(2, "Smutna Apteka"),
    new Pharmacy(3, "Ladna Apteka"),
};

Console.WriteLine("Witamy w systemie do zarzadzania aptekami");
IPharmaciesSource source = new PharmaciesInMemory(pharmacies);
IPharmacyChain all_pharmacies = new PharmacyChain(source);
all_pharmacies.displayAllPharmacies();

Console.Write("Podaj którą apteke wybierasz - po id: ");
if(!int.TryParse(Console.ReadLine(), out int numer))
{
    Console.WriteLine("Nie podales cyrfy a id jest cyfra");
    //continue
}
Pharmacy? choosed = pharmacies.FirstOrDefault(x => x.Id == numer);
Console.WriteLine($"Wybrales apteke: {choosed}");

