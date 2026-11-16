using Korepetycje.Models.Enums;
using Korepetycje.Models.Extensions;
using Korepetycje.Models.Repositories;

namespace Korepetycje.Models.Statistics;

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