
namespace Projekt.Models;

using Projekt.Models.Enums;

public class Platnosc
{
    private static int NextId = 1;

    public int Id { get; private set; }

    public TypPlatnosci PaymentMethod { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? Opis { get; set; }

    public StatusPlatnosci PaymentStatus { get; set; }

    public int LekcjaId { get; private set; }

    public Lekcja Lekcja { get; private set; } = null!;

    public Platnosc(
        TypPlatnosci paymentMethod,
        decimal amount,
        Lekcja lekcja,
        string? opis = null)
    {
        if (amount <= 0)
            throw new ArgumentException(
                "Platnosc nie moze byc rowna lub mniejsza od 0!");

        ArgumentNullException.ThrowIfNull(lekcja);

        Id = NextId++;

        PaymentMethod = paymentMethod;
        Amount = amount;
        PaymentDate = DateTime.Now;
        Opis = opis;
        Lekcja = lekcja;
        LekcjaId = lekcja.Id;

        PaymentStatus = StatusPlatnosci.Oczekujaca;
    }
}
