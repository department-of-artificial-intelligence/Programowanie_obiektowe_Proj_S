using System;
using System.Diagnostics;
using System.Xml.Linq;
using Project.Model;

public class Order : IShowInfo
{
    public int OrderId { get; set; }
    public IList<MenuItem> Items { get; set; } = [];
    public DateTime OrderTime { get; set; }
    public Client Client { get; set; }

    public Order() : this(0, new Client(), []) { }
    public Order(int orderId, Client client) : this(orderId, client, new List<MenuItem>()) { }
    public Order(int orderId, Client client, IList<MenuItem> items)
    {
        OrderId = orderId;
        Items = items;
        Client = client;
        OrderTime = DateTime.Now;
    }

    public void AddItem(MenuItem item)
    {
        if (item != null)
        {
            Items.Add(item);
        }
    }
    public bool RemoveItem(string name)
    {
        var item = Items.FirstOrDefault(i => i.Name == name);
        if (item == null)
            return false;

        Items.Remove(item);
        return true;
    }

    public string GetInfo()
    {
        var info = $"---- Order #{OrderId} ----\n";
        info += $"Client: {Client.FirstName} {Client.LastName}\n";
        info += $"Time: {OrderTime}\n";
        info += "Items:\n";

        foreach (var item in Items)
            info += $" - {item.Name} ({item.Price} zł)\n";

        info += $"Total: {Items.Sum(i => i.Price)} zł\n";
        info += "-------------------";

        return info;
    }
}
