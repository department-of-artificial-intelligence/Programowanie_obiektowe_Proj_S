namespace Project.Model
{
    
    public enum EmployeeRole
    {
        Manager,
        Mechanic,
        
    }

    public class Employee : Person
    {
        public EmployeeRole Role { get; set; }
        

        
        public Employee() { }
        public Employee(string firstName, string lastName, EmployeeRole role)
            : base(firstName, lastName)
        {
            Role = role;
        }

        public override string ToString()
        {
            return $"[Staff] {Role}: {FirstName} {LastName} (ID: {Id})";
        }
    }
}