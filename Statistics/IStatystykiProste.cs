using Korepetycje.Enums;

namespace Korepetycje.Statistics;

public interface IStatystykiProste
{
    int LiczbaLekcji(StatusLekcji status);
    decimal LacznyPrzychod();
}