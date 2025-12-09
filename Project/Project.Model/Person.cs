using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public abstract class Person {
    public string _firstName;
    public string _lastName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Imię nie może być null lub puste", nameof(FirstName));
            _firstName = value;
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwisko nie może być null lub puste", nameof(LastName));
            _lastName = value;
        }
    }

    //protected Person() 
    //{
    //    FirstName = string.Empty;
    //    LastName = string.Empty;
    //}

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
