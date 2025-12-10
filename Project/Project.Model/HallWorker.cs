using System;

public class HallWorker : Worker
{
    public int AssignedTables { get; set; }

    public HallWorker(string firstName, string lastName, decimal salary) : base(firstName, lastName, salary, "Hall Staff")
    {
        AssignedTables = 0;
    }

    public void ServeClient(Client client)
    {
        Console.WriteLine($"{FirstName} is serving {client.GetFullName()}.");
    }

    public void CleanTable(int tableNumber)
    {
        Console.WriteLine($"{FirstName} is cleaning table {tableNumber}.");
    }
}
