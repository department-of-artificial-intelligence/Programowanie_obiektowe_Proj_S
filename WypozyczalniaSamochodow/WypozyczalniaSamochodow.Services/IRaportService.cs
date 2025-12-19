using WypozyczalniaSamochodow.Model;

public interface IRaportService
{
    decimal GetTotalRevenue(int branchId);
    Car? GetMostRentedCar(int branchId);
    decimal GetAverageDailyRevenue(int branchId);
    Customer? GetBestCustomer(int branchId);
}