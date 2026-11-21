using Projekt.Models;
using Projekt.Repositories.Interfaces;

namespace Projekt.Repositories.InMemory;

public class KorepetytorRepository : IKorepetytorRepository
{
    private readonly List<Korepetytor> _data = new();

    public List<Korepetytor> GetAll() => _data;
    public bool Add(Korepetytor tutor)
    {
        if(tutor != null)
        {
            _data.Add(tutor);
            return true;
        }
        return false;
    }

    public bool Delete(Korepetytor tutor)
    {
        if(tutor != null && _data.Contains(tutor))
        {
            _data.Remove(tutor);
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        Korepetytor? tutor = _data.FirstOrDefault(t => t.Id == id);

        if(tutor != null)
        {
            _data.Remove(tutor);
            return true;
        }
        return false;
    }

    public Korepetytor? GetById(int id)
    {
        return _data.FirstOrDefault(t => t.Id == id);
    }
}