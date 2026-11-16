using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Model.DataModels
{
    public class Sector
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Animal> Animals { get; set; }
        public Sector(int id, int shelterId, List<Employee> employees, List<Animal> animals)
        {
            Id = id;
            ShelterId = shelterId;
            Employees = employees;
            Animals = animals;
        }
    }
}
