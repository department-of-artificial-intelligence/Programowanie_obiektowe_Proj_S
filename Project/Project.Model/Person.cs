using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public abstract class Person {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    protected Person()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    protected Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
