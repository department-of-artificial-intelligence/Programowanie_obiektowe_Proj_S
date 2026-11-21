namespace Projekt.Repositories.Interfaces;
using Projekt.Models;

public interface IKorepetytorRepository
{
    bool Add(Korepetytor tutor);
    bool Delete(Korepetytor tutor);
    bool Delete(int id);
    Korepetytor? GetById(int id);
    List<Korepetytor> GetAll();
}