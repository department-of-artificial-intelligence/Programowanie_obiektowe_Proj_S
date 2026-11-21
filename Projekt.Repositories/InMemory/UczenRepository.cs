using Projekt.Models;
using Projekt.Models.Enums;
using Projekt.Repositories.Interfaces;

namespace Projekt.Repositories.InMemory;

public class UczenRepository : IUczenRepository
{
    private readonly List<Uczen> _data = new();

    public bool Add(Uczen uczen)
    {
        if(uczen != null)
        {
            _data.Add(uczen);
            return true;
        }
        return false;
    }

    public bool Delete(Uczen uczen)
    {
        if(uczen != null && _data.Contains(uczen))
        {
            _data.Remove(uczen);
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        Uczen? uczen = _data.FirstOrDefault(u => u.Id == id);
        if(uczen != null && _data.Contains(uczen))
        {
            _data.Remove(uczen);
            return true;
        }
        return false;
    }

    public Uczen? GetById(int id)
    {
        return _data.FirstOrDefault(u => u.Id == id);
    }

    public Uczen? Get(string fname, string lname)
    {
        return _data.FirstOrDefault(u => u.Name == fname && u.Surname == lname);
    }

    public List<Uczen> GetAll() => _data;

    public IEnumerable<Uczen> GetByStudyLevel(PoziomNauczania level)
    {
        return _data.Where(u => u.PoziomNauczania == level);
    }
}