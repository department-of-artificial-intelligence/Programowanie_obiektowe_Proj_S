using Projekt.Models;
using Projekt.Models.Enums;

namespace Projekt.Repositories.Interfaces;

public interface ILekcjaRepository
{
    bool Add(Lekcja lekcja);
    bool Delete(Lekcja lekcje);
    bool Delete(int id);
    Lekcja? GetById(int id);
    Lekcja? GetByDate(DateTime date);
    List<Lekcja> GetAll();
    IEnumerable<Lekcja> GetWithStatus(StatusLekcji status);
}