using System;
using Project.Model;

public class Worker : Person, IShowInfo
{
    public decimal Salary { get; set; }
    public string Position { get; set; }

    public Worker() : this(string.Empty, string.Empty, 0, string.Empty) { }
    public Worker(string firstName, string lastName, decimal salary, string position) : base(firstName, lastName)
    {
        Salary = salary;
        Position = position;
    }

    public string GetInfo()
    {
        var info = $"---- Worker {FirstName} {LastName}----\n";
        info += $"Salary: {Salary}\n";
        info += $"Position: {Position}\n";
        info += "-------------------------";

        return info;
    }
}
