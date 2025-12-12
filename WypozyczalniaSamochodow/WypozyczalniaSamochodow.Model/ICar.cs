using WypozyczalniaSamochodow.Model;

public interface ICar
{
    void ShowCars(Branch branch);
    void AddCar(Car car, Branch branch);
    void RemoveCar(int carId, Branch branch);
    void ShowCarReservations(int carId, Branch branch);
    bool HasCars(Branch branch);
}
