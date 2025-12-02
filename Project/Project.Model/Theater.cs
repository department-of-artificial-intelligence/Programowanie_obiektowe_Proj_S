using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Theater
{
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<Hall> Halls { get; set; }

    public Theater()
    {
        TheaterId = 0;
        Name = string.Empty;
        Address = new Address();
        Halls = new List<Hall>();
    }

    public Theater(int theaterId, string name, string country, string city, string street)
    {
        TheaterId = theaterId;
        Name = name;
        Address = new Address(country, city, street);
        Halls = new List<Hall>();
    }

    public bool CreateHall(int hallId)
    {
        if (hallId <= 0) return false;
        Hall hall = new Hall(hallId);
        Halls.Add(hall);
        return true;
    }
    public bool DeleteHall(int hallId)
    {
        if (Halls.Count == 0 || hallId <= 0) return false;
        var hall = Halls.FirstOrDefault(t => t.HallId == hallId);
        if (hall is null) return false;
        return Halls.Remove(hall);
    }
    public void DeleteAllHalls()
    {
        Halls.Clear();
    }

    public override string ToString()
    {
        return $"{TheaterId}/{Name}/Adres: {Address}\nSale teatralne:\n" + Halls.ListToString("Brak sal teatralnych");
    }
}
