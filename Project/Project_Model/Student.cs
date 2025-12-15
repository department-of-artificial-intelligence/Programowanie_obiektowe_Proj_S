using System.Collections.Generic;

namespace Project.Model
{
    public class Student : User
    {
        public string? EducationalLevel { get; set; }

        public Student(string firstName, string lastName, string email, string educationalLevel)
            : base(firstName, lastName, email)
        {
            EducationalLevel = educationalLevel;
        }
        public Student() { }

        public override string ToString() => $"{base.ToString()}, Poziom: {EducationalLevel}";
    }
}