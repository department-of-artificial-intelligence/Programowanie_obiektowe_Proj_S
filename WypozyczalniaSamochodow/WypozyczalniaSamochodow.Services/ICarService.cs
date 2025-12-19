using WypozyczalniaSamochodow.Model;

public interface ICarService
{
    IEnumerable<Car> GetCarByBranch(int branchId);
    void AddCar(Car car, Branch branch);
    void UpdateCar(Car car);
    void RemoveCar(int carId, int branchId);

    IEnumerable<Car> SearchCars(int branchId, string? brand, int? minPower, decimal? maxPrice, string? gearbox);
}
