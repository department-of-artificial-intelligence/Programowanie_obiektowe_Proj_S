using WypozyczalniaSamochodow.Model;

Car car1 = new Car() { Id=1, Brand = "BMW", Model="M4", ProductionYear=2024, EngineVolume=3.0f, AvgConsumption=14.4, Power=510, Gearbox="Automatic", FuelType="Gas", IsAvailable = false };

Car car2 = new Car();
car2.Id = 2;
car2.Brand = "Mercedes";
car2.Model = "AMG GT 63 S";
car2.ProductionYear = 2025;
car2.EngineVolume = 3.0f;
car2.AvgConsumption = 16.3;
car2.Power = 730;
car2.Gearbox = "Automatic";
car2.FuelType = "Gas";
car2.IsAvailable = true;

Branch branch1 = new Branch(1, "CarRental", "Częstochowa");
branch1.AddCarToOffer(car2);

Console.WriteLine(branch1);

/*Console.WriteLine("Usuwanie samochodu z oferty...");
branch1.RemoveCarFromOffer(car2);*/

Branch branch2 = new Branch(2, "CarRental", "Myszków");
branch2.AddCarToOffer(car1);
branch2.AddCarToOffer(car2);
Console.WriteLine(branch2);
//Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");
