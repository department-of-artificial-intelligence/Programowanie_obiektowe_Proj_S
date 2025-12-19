using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Extensions;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Services
{
    public class BranchService(ApplicationDbContext context) : IBranchService
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<Branch> GetAllBranches()
        {
            return _context.Branches.ToList();
        }
        public Branch GetBranchById(int id)
        {
            var branch = _context.Branches
                .Include(b => b.Cars)
                .Include(b => b.Customers)
                .Include(b => b.Rentals)
                    .ThenInclude(r => r.Car)
                .Include(b => b.Rentals)
                    .ThenInclude(r => r.Customer)
                .FirstOrDefault(b => b.Id == id);

            if (branch is null) throw new InvalidOperationException("Oddział o podanym ID nie istnieje");

            return branch;
        }
        public void AddBranch(Branch branch)
        {
            if (branch is null) throw new ArgumentNullException(nameof(branch));

            branch.ValidateBranch();

            _context.Branches.Add(branch);
            _context.SaveChanges();
        }

        public void UpdateBranch(Branch branch)
        {
            if (branch is null) throw new ArgumentNullException(nameof(branch));

            branch.ValidateBranch();

            var existingBranch = _context.Branches.FirstOrDefault(b => b.Id == branch.Id);
            if (existingBranch is null) throw new InvalidOperationException("Oddział nie istnieje");

            existingBranch.Name = branch.Name;
            existingBranch.City = branch.City;
            existingBranch.Address = branch.Address;
            existingBranch.ContactNumber = branch.ContactNumber;

            _context.SaveChanges();
        }

        public void RemoveBranch(int id)
        {
            var branch = _context.Branches
                .Include(b => b.Rentals)
                .FirstOrDefault(b => b.Id == id);

            if (branch is null) throw new InvalidOperationException("Oddział nie istnieje");
            if (branch.HasActiveRentals()) throw new InvalidOperationException("Nie można usunąć oddziału posiadającego aktywne wypożyczenia");

            _context.Branches.Remove(branch);
            _context.SaveChanges();
        }
    }
}
