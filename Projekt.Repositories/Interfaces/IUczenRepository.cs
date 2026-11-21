using Projekt.Models;
using Projekt.Models.Enums;

namespace Projekt.Repositories.Interfaces;

public interface IUczenRepository
{
    bool Add(Uczen uczen);
    bool Delete(Uczen uczen);
    bool Delete(int id);
    Uczen? GetById(int id);
    Uczen? Get(string fname, string lname);
    List<Uczen> GetAll();
    IEnumerable<Uczen> GetByStudyLevel(PoziomNauczania level);
}