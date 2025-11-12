using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Trainer : User
    {
        public required string Specialization { get; set; }
        public required int Experience { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();

        public Trainer(string name, string lastName, int role, int contactDetails, string specialization, int experience)
            : base(name, lastName, role, contactDetails)
        {
            Specialization = specialization;
            Experience = experience;
        }


        public Trainer() : base()
        {
            Specialization = string.Empty;
            Experience = 0;
            Clients = new List<Client>();
        }
        public static List<Trainer> SortTrainers(List<Trainer> trainers, string sortBy = "Last_Name", bool ascending = true)
        {

            switch (sortBy)
            {
                case "Last_Name":
                    return ascending ? trainers.OrderBy(t => t.Last_Name).ToList() : trainers.OrderByDescending(t => t.Last_Name).ToList();
                case "Name":
                    return ascending ? trainers.OrderBy(t => t.Name).ToList() : trainers.OrderByDescending(t => t.Name).ToList();
                default:
                    Console.WriteLine("Nieznane kryterium sortowania. Sortowanie po nazwisku.");
                    return trainers.OrderBy(t => t.Last_Name).ToList();
            }
        }
    }
}


