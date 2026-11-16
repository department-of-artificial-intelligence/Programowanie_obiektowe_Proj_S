using Korepetycje.Models.Enums;

namespace Korepetycje.Models.Statistics;

public interface IStatystykiProste
{
    int LiczbaLekcji(StatusLekcji status);
    decimal LacznyPrzychod();
}