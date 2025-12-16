using System;

public class KitchenWorker : Worker
{
	public string Station { get; set; }

	public KitchenWorker(string firstName, string lastName, decimal salary, string station) : base(firstName, lastName, salary, "Kitcher Stuff")
	{
		Station = station;
	}

    public void PrepareFood()
    {
        Console.WriteLine($"{FirstName} is preparing food at the {Station}");
    }
}
