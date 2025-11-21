using WypozyczalniaSamochodow.Model;

public interface IRental
{
    void ShowRentals();
    void RentCar(int carId, int customerId, int days = 7);
    void ReturnCar(int rentalId);
}