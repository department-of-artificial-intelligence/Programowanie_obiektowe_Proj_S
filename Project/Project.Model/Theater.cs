using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Theater
{
    private static int MaxId = 0;
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<Hall> Halls { get; set; }
    private static int GetNextId() => MaxId + 1;

    public Theater()
    {
        TheaterId = GetNextId();
        MaxId = TheaterId;
        Name = string.Empty;
        Address = new Address();
        Halls = new List<Hall>();
    }

    public Theater(string name, string country, string city, string street)
        : this(GetNextId(), name, country, city, street) { }

    public Theater(int theaterId, string name, string country, string city, string street)
    {
        if (theaterId <= MaxId) throw new Exception($"theaterId {theaterId} jest mniejsze lub równe MaxId {MaxId}");
        TheaterId = theaterId;
        MaxId = TheaterId;
        Name = name;
        Address = new Address(country, city, street);
        Halls = new List<Hall>();
    }

    public bool CreateHall(List<Performance>? performances = null)
    {
        Hall hall = new Hall(performances);
        Halls.Add(hall);
        return true;
    }
    public bool CreateHall(int hallId, List<Performance>? performances = null)
    {
        if (hallId <= 0) return false;
        Hall hall = new Hall(hallId, performances);
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

    public string GetHallsString()
    {
        return Halls.ListToString("Brak sal teatralnych", '-');
    }

    public override string ToString()
    {
        return $"{TheaterId}/{Name}/Adres: {Address}\nSale teatralne:\n" + GetHallsString();
    }
}
