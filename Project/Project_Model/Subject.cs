using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Subject() { 
            Id = 0;
            Name=string.Empty;
            Description = string.Empty;
        }

        public Subject(int id, string name, string description) {
            Id = id;
            Name = name;
            Description = description;
        }
        public override string ToString() => $"Subject {Id}: {Name}";
    }
}
