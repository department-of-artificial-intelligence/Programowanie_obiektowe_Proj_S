using WypozyczalniaSamochodow.Model;

public interface IRentalService
{
    IEnumerable<Rental> GetActiveRentals(int branchId);
    IEnumerable<Rental> GetCompletedRentals(int branchId);
    void RentCar(Rental rental);
    void ReturnCar(int rentalId, int branchId);
    void RentCarWithPoints(Rental rental);
}
