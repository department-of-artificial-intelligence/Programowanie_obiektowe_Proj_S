using System;

public class KitchenWorker : Worker
{
	public string Station { get; set; }

	public KitchenWorker(string firstName, string lastName, decimal salary, string station) : base(firstName, lastName, salary, "Kitcher Stuff")
	{
		Station = station;
	}

	public new string GetInfo()
	{
		return base.GetInfo() + $"\n - Station: {Station}";
	}
}
