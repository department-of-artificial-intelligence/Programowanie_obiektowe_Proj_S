using WypozyczalniaSamochodow.Model;

public interface IBranch
{
    void ShowBranches();
    void AddBranch(string name, string city, string address, string contactNumber);
    void RemoveBranch(int branchId);
    bool HasBranches();
}