using System;
using System.Xml.Linq;
using Project.Model;

public class PizzeriasNetwork
{
    public IList<Pizzeria> PizzeriasList { get; set; }

    public PizzeriasNetwork()
    {
        PizzeriasList = new List<Pizzeria>();
    }

    public void AddPizzeria(Pizzeria pizzeria)
    {
        if (pizzeria == null) return;

        if (PizzeriasList.Any(p => p.Name == pizzeria.Name))
        {
            Console.WriteLine($"Error: Pizzeria '{pizzeria.Name}' already exists in the network.");
            return;
        }

        PizzeriasList.Add(pizzeria);
    }

    public void RemovePizzeria(string name)
    {
        var pizzeriaToRemove = GetPizzeria(name);

        if (pizzeriaToRemove != null)
        {
            PizzeriasList.Remove(pizzeriaToRemove);
        }
        else
        {
            Console.WriteLine($"Error: Could not find pizzeria '{name}'.");
        }
    }

    public Pizzeria? GetPizzeria(string name)
    {
        return PizzeriasList.FirstOrDefault(p => p.Name == name);
    }

    public void DisplayAll()
    {
        Console.WriteLine("\n================ NETWORK OVERVIEW ================");
        if (PizzeriasList.Count == 0)
        {
            Console.WriteLine("No pizzerias currently in the network.");
        }
        else
        {
            foreach (var pizzeria in PizzeriasList)
            {
                Console.WriteLine(pizzeria.GetInfo());
            }
        }
        Console.WriteLine("==================================================");
    }
}