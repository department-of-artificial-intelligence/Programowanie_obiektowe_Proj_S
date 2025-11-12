using System;

public class MenuItem
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public decimal? Price { get; set; }

	public MenuItem() : this(String.Empty, String.Empty, 0) { }

	public MenuItem(string name, string description, decimal price)
	{
		Name = name;
		Description = description;
		Price = price;
	}

	public void SetPrice(decimal price)
	{
		Price = price;
	}

	public void GetInfo()
	{
		Console.WriteLine($"Name: {Name}\nDesctription: {Description}\nPrice: {Price}");
	}

}
