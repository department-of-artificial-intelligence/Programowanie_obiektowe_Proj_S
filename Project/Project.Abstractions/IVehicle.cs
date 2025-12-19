namespace Project.Abstractions
{
    public enum VehicleStatus
    {
        Available,
        InTransit,
        UnderMaintenance
    }

    public enum VehicleType
    {
        CompanyCar,
        DeliveryVan,
        Truck,
        SemiTrailer
    }

    public interface IVehicle
    {
        int Id { get; set; }
        string VinNumber { get; set; }
        int ProductionYear { get; set; }
        float EngineSize { get; set; }
        int Mileage { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        string RegistrationNumber { get; set; }

        VehicleStatus VStatus { get; set; }
        VehicleType VType { get; }

        IDriver? AssignedDriver { get; set; }

        bool IsAvailable { get; }

        void AssignDriver(IDriver? driver);
        void MarkAsAvailable();

        float CalculateWearRate();

        string ToString();
    }
}