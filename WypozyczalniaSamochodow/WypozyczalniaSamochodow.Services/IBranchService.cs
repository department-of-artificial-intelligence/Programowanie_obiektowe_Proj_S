using WypozyczalniaSamochodow.Model;

public interface IBranchService
{
    IEnumerable<Branch> GetAllBranches();
    Branch GetBranchById(int id);
    void AddBranch(Branch branch);
    void UpdateBranch(Branch branch);
    void RemoveBranch(int id);
}
