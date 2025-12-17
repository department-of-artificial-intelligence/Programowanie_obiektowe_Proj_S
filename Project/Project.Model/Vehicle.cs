using Project.Abstractions;
using Project.Model;
using System.ComponentModel.DataAnnotations.Schema;
public abstract class Vehicle : IVehicle
{
    public int Id { get; set; } = int.MinValue;
    public string VinNumber { get; set; } = string.Empty;
    public int ProductionYear { get; set; } = int.MinValue;
    public float EngineSize { get; set; } = float.MinValue;
    public int Mileage { get; set; } = int.MinValue;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;

    public VehicleStatus VStatus { get; set; }
    public abstract VehicleType VType { get; }
    public Driver? AssignedDriver { get; set; }
    IDriver? IVehicle.AssignedDriver
    {
        get => AssignedDriver;
        set => AssignedDriver = value as Driver;
    }

    public Vehicle() {}
    protected Vehicle(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registrationNumber)
    {
        Id = id;
        VinNumber = vinNumber;
        ProductionYear = productionYear;
        EngineSize = engineSize;
        Mileage = mileage;
        Brand = brand;
        Model = model;
        RegistrationNumber = registrationNumber;
    }

    public bool IsAvailable => VStatus == VehicleStatus.Available;

    public void AssignDriver(IDriver? driver)
    {
        if (driver is null)
            throw new ArgumentNullException(nameof(driver));

        if (!driver.IsAvailable)
            throw new InvalidOperationException("ERROR - Driver is not available.");

        this.AssignedDriver = driver as Driver;
        VStatus = VehicleStatus.InTransit;
    }


    public void MarkAsAvailable()
    {
        VStatus = VehicleStatus.Available;
        AssignedDriver = null;
    }

    public abstract float CalculateWearRate();

    public override string ToString()
    {
        return $"Vehicle ID: {Id}, VIN: {VinNumber}, Brand: {Brand}, Model: {Model}, " +
               $"Production year: {ProductionYear}, Engine: {EngineSize}l, Mileage: {Mileage} km, " +
               $"Registration: {RegistrationNumber}, Type: {VType}, OStatus: {VStatus}";
    }

}