using System;
using Project.Model;

public class MenuItem : IShowInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }

    public MenuItem() : this(0, String.Empty, 0, String.Empty) { }

    public MenuItem(int id, string name, decimal price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public void SetPrice(decimal price) => Price = price;

    public string GetInfo()
    {
        return $"---- Menu Item ----\n" +
               $"Name: {Name}\n" +
               $"Price: {Price} zł\n" +
               $"Description: {Description}\n" +
               $"-------------------";
    }
}
