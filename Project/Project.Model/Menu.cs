using System;
using Project.Model;

public class Menu : IShowInfo
{
    public int Id { get; set; }
    public IList<MenuItem> AvailableItems { get; set; }

    public Menu() : this([]) { }
    public Menu(IList<MenuItem> availableItems)
    {
        AvailableItems = availableItems;
    }

    public void AddItem(MenuItem item)
    {
        if (item == null)
            return;
        if (AvailableItems.Any(i => i.Name == item.Name))
        {
            Console.WriteLine($"Item '{item.Name}' already exists in menu!");
            return;
        }

        AvailableItems.Add(item);
    }

    public bool RemoveItem(string name)
    {
        var item = AvailableItems.FirstOrDefault(i => i.Name == name);
        if (item == null)
            return false;

        AvailableItems.Remove(item);
        return true;
    }

    public MenuItem? FindItem(string name)
    {
        return AvailableItems.FirstOrDefault(i => i.Name == name);
    }

    public string GetInfo()
    {
        var info = "======= MENU =======\n";
        if (AvailableItems.Count == 0)
        {
            info += "(menu is empty)\n";
        }
        else
        {
            foreach (var item in AvailableItems)
                info += $"- {item.Name}: {item.Price} zł\n";
        }
        info += "====================";
        return info;
    }
}
