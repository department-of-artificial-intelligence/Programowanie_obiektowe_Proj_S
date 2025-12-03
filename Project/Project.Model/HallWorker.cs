using System;

public class HallWorker : Worker
{
    public int AssignedTables { get; set; }

    public HallWorker(string firstName, string lastName, decimal salary) : base(firstName, lastName, salary, "Hall Staff")
    {
        AssignedTables = 0;
    }

    public new string GetInfo()
    {
        return base.GetInfo() + $"\n - Assigned Tables: {AssignedTables}";
    }
}
