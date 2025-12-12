using WypozyczalniaSamochodow.Model;

public interface IRental
{
    void ShowRentals(Branch branch);
    void ShowHistory(Branch branch);
    void RentCar(int carId, int customerId, DateTime startDate, DateTime endDate, Branch branch);
    void ReturnCar(int rentalId, Branch branch);
    bool HasRentals(Branch branch);

    double TotalRevenue(Branch branch);
    Car? MostRentedCar(Branch branch);
    Customer? TopCustomer(Branch branch);
}
