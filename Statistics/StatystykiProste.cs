using Korepetycje.Enums;
using Korepetycje.Extensions;
using Korepetycje.Repositories;

namespace Korepetycje.Statistics;

public class StatystykiProste : StatystykiBazowe, IStatystykiProste
{
    public StatystykiProste(IRepozytoriumLekcji repo): base(repo){}

    public int LiczbaLekcji(StatusLekcji status)
    {
        return Lekcje.ZeStatusem(status).Count();
    }

    public decimal LacznyPrzychod()
    {
        return Lekcje.Zakonczone().LacznyPrzychod();
    }

    public decimal SredniaCenaLekcji()
    {
        return Lekcje.SredniaCena();
    }
}