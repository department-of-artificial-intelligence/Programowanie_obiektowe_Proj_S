namespace Projekt.Models;
using Projekt.Models.Enums;

public class Platnosc
{
    public int Id { get; set;}
    private static int NextId {get; set;} = 1;

    public TypPlatnosci PaymentMethod {get; set;}

    public bool IsPaid { get; set;}
    public decimal Amount {get; set;}
    public DateTime PaymentDate {get; set;}
    public string? Opis { get; set;}


    public Platnosc(TypPlatnosci paymentmethod, bool ispaid, decimal amount, DateTime pdate, string opis)
    {
        if(amount <= 0)
        {
            throw new ArgumentException("Platnosc nie mozze byc rowna lub mniejsza od 0!");
        }
        Id = NextId++;
        PaymentMethod = paymentmethod;
        IsPaid = ispaid;
        Amount = amount;
        PaymentDate = pdate;
        Opis = opis ?? string.Empty;             
    }
}