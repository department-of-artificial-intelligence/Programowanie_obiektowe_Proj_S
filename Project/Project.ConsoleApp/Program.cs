using System;
using Project.Model;

public class Program
{
    public static void Main()
    {
        var pizzeria = new Pizzeria("Pizza Tower", "Dąbrowskiego");

        pizzeria.Menu.AddItem(new MenuItem(1, "Margherita", 25, "Classic"));
        pizzeria.Menu.AddItem(new MenuItem(2, "Pepperoni", 30, "Spicy"));

        var client = new Client(10, "John", "Doe", "555-123");
        var order = pizzeria.PlaceOrder(client, new List<string> { "Margherita", "Pepperoni" });

        Console.WriteLine(order.GetInfo());
        Console.WriteLine(pizzeria.GetInfo());
    }
}