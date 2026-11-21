using Projekt.Models;
using Projekt.Models.Enums;
using Projekt.Repositories.Interfaces;

namespace Projekt.Repositories.InMemory;

public class LekcjaRepository : ILekcjaRepository
{
    private readonly List<Lekcja> _data = new();

    public bool Add(Lekcja lekcja)
    {
        if(lekcja != null)
        {
            _data.Add(lekcja);
            return true;
        }
        return false;
    }

    public bool Delete(Lekcja lekcja)
    {
        if(lekcja != null && _data.Contains(lekcja))
        {
            _data.Remove(lekcja);
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        Lekcja? lekcja = _data.FirstOrDefault(l => l.Id == id);
        if(lekcja != null)
        {
            _data.Remove(lekcja);
            return true;
        }
        return false;
    }

    public Lekcja? GetById(int id)
    {
        return _data.FirstOrDefault(l => l.Id == id);
    }

    public Lekcja? GetByDate(DateTime date)
    {
        return _data.FirstOrDefault(l => l.StartDate == date);
    }

    public List<Lekcja> GetAll() => _data;

    public IEnumerable<Lekcja> GetWithStatus(StatusLekcji status)
    {
        return _data.Where(l => l.Status == status);
    }
}