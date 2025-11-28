using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Logic;
public class BranchLogic : IBranch
{
    private readonly List<Branch> _branches;

    public BranchLogic(List<Branch> branches)
    {
        _branches = branches;
    }

    public void ShowBranches()
    {
        if (!_branches.Any())
        {
            Console.WriteLine("Brak oddziałów.");
            return;
        }

        _branches.ForEach(b => Console.WriteLine(b));
    }

    public void AddBranch(string name, string city)
    {
        int newId = _branches.Count > 0 ? _branches.Max(b => b.Id) + 1 : 1;
        _branches.Add(new Branch(newId, name, city));
        Console.WriteLine($"Dodano oddział: {name} ({city})");
    }

    public void RemoveBranch(int branchId)
    {
        var branch = _branches.FirstOrDefault(b => b.Id == branchId);
        if (branch == null)
        {
            Console.WriteLine("Nie znaleziono oddziału.");
            return;
        }
        _branches.Remove(branch);
        Console.WriteLine($"Usunięto oddział {branch.Name}");
    }
}
