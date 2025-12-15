using System;
namespace Project.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // relacja do tutors 
        public List<Tutor> Tutors { get; set; } = new();

        public Subject(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Subject() { } 
        public override string ToString() => $"Przedmiot {Id}: {Name}";
    }
}
