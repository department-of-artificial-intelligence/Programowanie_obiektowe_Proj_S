namespace Projekt.Services.Korepetytor;
using Projekt.Models;

public interface IKorepetytorService
{
    bool AddDate(Korepetytor korepetytor, TerminDostepnosci date);
    bool RemoveDate(Korepetytor korepetytor, TerminDostepnosci date);
}