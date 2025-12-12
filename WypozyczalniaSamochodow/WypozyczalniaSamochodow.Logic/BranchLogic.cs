using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Logic
{
    public class BranchLogic : IBranch
    {
        private readonly List<Branch> _branches;
        
        public BranchLogic(List<Branch> branches)
        {
            _branches = branches;
        }

        public void ShowBranches()
        {
            if (_branches.Count <= 0)
            {
                Console.WriteLine("Brak oddziałów.");
                return;
            }

            foreach (var branch in _branches)
            {
                Console.WriteLine(branch);
            }
        }

        public void AddBranch(string name, string city, string address, string contactNumber)
        {
            int newId = _branches.Count > 0 ? _branches.Max(b => b.Id) + 1 : 1;
            var branch = new Branch(newId, name, city, address, contactNumber);

            _branches.Add(branch);
            Console.WriteLine($"\nDodano oddział: {name} ({city})");
        }

        public void RemoveBranch(int branchId)
        {
            var branch = _branches.FirstOrDefault(b => b.Id == branchId);
            if (branch == null)
            {
                Console.WriteLine("\nNie znaleziono oddziału.");
                return;
            }

            if (branch.Rentals.Count > 0)
            {
                Console.WriteLine($"\nNie można usunąć oddziału {branch.Name} {branch.City}, ponieważ posiada aktywne wypożyczenia.");
                return;
            }

            _branches.Remove(branch);
            Console.WriteLine($"\nUsunięto oddział {branch.Name} {branch.City}");
        }

        public bool HasBranches()
        {
            return _branches.Count > 0;
        }
    }
}
