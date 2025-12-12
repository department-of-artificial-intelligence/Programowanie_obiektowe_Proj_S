using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Abstraction;


namespace Project.Model
{
    public class Clinic : IIdentifiable, IContactable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<Veterinarian> Veterinarians { get; set; } = new();

        public Clinic() { }

        public Clinic(int id, string name, string? address = null, string? email = null, string? phone = null)
        {
            Id = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }
    }
}

