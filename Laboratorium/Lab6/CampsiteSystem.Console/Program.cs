using DAL;
using Microsoft.Extensions.DependencyInjection;
using Services.DTO.Cabin;
using Services.DTO.Campsite;
using Services.Interfaces;
using Services.Services;

namespace CampsiteSystem.Console;

internal class Program {
  static void Main() {

    //Tworzenie i konfiguracja kontenera DI
    using var serviceProvider = new ServiceCollection()
      .AddDbContext<AppDbContext>()
      //.AddTransient<ICampsiteService,CampsiteService>()
      //.AddTransient<ICabinService,CabinService>()
      .AddScoped<ICampsiteService, CampsiteService>() // dodane
      .AddScoped<ICabinService, CabinService>() // dodane
      .BuildServiceProvider();

    using var scope = serviceProvider.CreateScope();
    var campsiteService = scope.ServiceProvider.GetRequiredService<ICampsiteService>();
    var cabinService = scope.ServiceProvider.GetRequiredService<ICabinService>();
    //Pobranie serwisów z kontenera DI

    //Dodanie nowego kempingu
    var campsiteId = campsiteService.CreateAsync(new CreateCampsiteDto {
      Name="Kemping Bieszczady"
    }).Result;

    //Dodanie domku do nowego kempingu
    var cabinId = cabinService.CreateAsync(new CreateCabinDto {
      Number=1,
      Capacity=5,
      PricePerNight=320,
      CampsiteId=campsiteId
    }).Result;

    //Aktualizacja domku
    cabinService.UpdateAsync(cabinId,new UpdateCabinDto {
      Number=1,
      Capacity=6,
      PricePerNight=350,
      CampsiteId=campsiteId
    }).Wait();

    //Pobranie wybranego domku
    var selectedCabin = cabinService.GetByIdAsync(cabinId).Result;

    if(selectedCabin!=null) {
      System.Console.WriteLine(
        $"\n■ Numer domku: {selectedCabin.Number}, "+
        $"pojemność [liczba osób]: {selectedCabin.Capacity}, "+
        $"cena [zł. za noc]: {selectedCabin.PricePerNight} zł, "+
        $"kemping: {selectedCabin.CampsiteName}"
      );
    }

    //Wyświetlenie wszystkich kempingów
    var /*List<CampsiteDto>*/ campsites = campsiteService.GetAllAsync().Result;

    System.Console.WriteLine("\n■ Kempingi:");

    foreach(var campsite in campsites) {
      System.Console.WriteLine(
        $"Id kempingu: {campsite.CampsiteId}, nazwa: {campsite.Name}"
      );
    }

    //Wyświetlenie wszystkich domków
    var cabins = cabinService.GetAllAsync().Result;

    System.Console.WriteLine("\n■ Domki:");

    foreach(var cabin in cabins) {
      System.Console.WriteLine(
        $"Id domku: {cabin.CabinId}, "+
        $"numer domku: {cabin.Number}, "+
        $"pojemność [liczba osób]: {cabin.Capacity}, "+
        $"cena [zł. za noc]: {cabin.PricePerNight} zł, "+
        $"kemping: {cabin.CampsiteName}"
      );
    }

    //Usunięcie domku
    cabinService.DeleteAsync(cabinId).Wait();

    //Usunięcie kempingu
    campsiteService.DeleteAsync(campsiteId).Wait();
  }
}


// Install-Package Microsoft.EntityFrameworkCore -Version 9.0.15
// Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 9.0.15
// Install-Package Microsoft.EntityFrameworkCore.Tools -Version 9.0.15