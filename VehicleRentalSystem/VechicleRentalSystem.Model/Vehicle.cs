namespace VehicleRentalSystem.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int ProdYear { get; set; }
        public WheelDrive Drive { get; set; }
        public Transmission Transmission { get; set; }
        public int SeatsAmount { get; set; }
        public FuelType Fuel { get; set; }
        public double EngineVolume { get; set; }
        public int HorsePower { get; set; }
        public int Torque { get; set; }
        public double PriceForDay { get; set; }
        public bool IsRented { get; set; }
        public bool IsActive { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime LastService { get; set; }
        public DateTime NextService { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Vehicle() { }
        public Vehicle(string brand, string model, int prodYear, WheelDrive drive, Transmission transmission, int seatsAmount, 
            FuelType fuel, double engineVolume, int horsePower, int torque, double priceForDay, string registrationNumber, DateTime lastService, DateTime nextService)
        {
            Brand = brand;
            Model = model;
            ProdYear = prodYear;
            Drive = drive;
            Transmission = transmission;
            SeatsAmount = seatsAmount;
            IsRented = false;
            Fuel = fuel;
            EngineVolume = engineVolume;
            HorsePower = horsePower;
            Torque = torque;
            PriceForDay = priceForDay;
            RegistrationNumber = registrationNumber;
            LastService = lastService;
            NextService = nextService;
        }

        public override string ToString()
        {
            return $"[POJAZD ID: {Id}]\n" +
                    $"    MARKA: {Brand}\n" +
                    $"    MODEL: {Model}\n" +
                    $"    ROK PRODUKCJI: {ProdYear}\n" +
                    $"    NAPĘD: {Drive} \n" +
                    $"    SKRZYNIA BIEGÓW: {Transmission} \n" +
                    $"    LICZBA MIEJSC: {SeatsAmount} \n" +
                    $"    RODZAJ PALIWA: {Fuel} \n" +
                    $"    POJEMNOŚĆ SILNIKA: {EngineVolume} L\n" +
                    $"    MOC: {HorsePower} KM\n" +
                    $"    MOMENT OBROTOWY: {Torque} Nm\n" +
                    $"    CENA ZA DZIEŃ: {PriceForDay:F2} PLN\n" +
                    $"    NUMER REJESTRACYJNY: {RegistrationNumber} \n" +
                    $"    STATUS: {(IsActive ? "Aktywny" : "Zarchiwizowany")}, {(IsRented ? "Wypożyczony" : "Dostępny")}\n" +
                    $"    OSTATNI SERWIS: {LastService:yyyy-MM-dd} \n" +
                    $"    NASTĘPNY SERWIS: {NextService:yyyy-MM-dd}\n" +
                    $"    PLACÓWKA: {Department?.Name ?? "Brak"}\n";
        }
    }
}
