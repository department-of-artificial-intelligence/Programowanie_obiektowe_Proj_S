using System;

public class Program
{
    public static void Main()
    {
        var m = new MenuItem(1, "Margherita", 25m, "Classic pizza");
        Console.WriteLine(m.GetInfo());
        m.SetPrice(27.5m);
        Console.WriteLine("After price change:");
        Console.WriteLine(m.GetInfo());
    }


}