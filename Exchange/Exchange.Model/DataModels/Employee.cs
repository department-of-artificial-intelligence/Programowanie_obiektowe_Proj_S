using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Model.Enums;

namespace Exchange.Model.DataModels;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
    public Branch? Branch { get; set; }

    public override string ToString()
    {
            return $"{Id} {FirstName} {LastName} {Branch}";
    }
}
