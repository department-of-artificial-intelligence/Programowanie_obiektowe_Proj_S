using WypozyczalniaSamochodow.Model;

public interface IBranch
{
    void ShowBranches();
    void AddBranch(string name, string city);
    void RemoveBranch(int branchId);
}