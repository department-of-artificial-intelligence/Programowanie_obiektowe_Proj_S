using Korepetycje.Models;
using Korepetycje.Repositories;

namespace Korepetycje.Statistics;

public abstract class StatystykiBazowe
{
    private readonly IRepozytoriumLekcji _repo;

    protected StatystykiBazowe(IRepozytoriumLekcji repo)
    {
        _repo = repo;
    }

    protected IEnumerable<Lekcja> Lekcje => _repo.PobierzWszystkie();

}