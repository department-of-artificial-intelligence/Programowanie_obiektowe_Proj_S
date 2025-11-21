using WypozyczalniaSamochodow.Model;

public interface ICar
{
    void ShowCars();
    void AddCar(Car car, int branchId);
    void RemoveCar(int carId);
}