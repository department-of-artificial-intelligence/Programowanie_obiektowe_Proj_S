
namespace Projekt.Repositories.InMemory;

using Projekt.Models;
using Projekt.Repositories.Interfaces;

public class PlatnoscRepository : IPlatnoscRepository
{
    private readonly List<Platnosc> _payments = new();

    public void AddPayment(Platnosc payment)
    {
        if (payment == null)
            throw new ArgumentNullException(nameof(payment));

        _payments.Add(payment);
    }

    public Platnosc? GetPaymentById(int id)
    {
        return _payments.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Platnosc> GetAllPayments()
    {
        return _payments;
    }

    public IEnumerable<Platnosc> GetPaymentsByLessonId(int lekcjaId)
    {
        return _payments.Where(p => p.LekcjaId == lekcjaId);
    }

    public bool UpdatePayment(Platnosc payment)
    {
        if (payment == null)
            return false;

        var existingPayment = GetPaymentById(payment.Id);

        if (existingPayment == null)
            return false;


        return true;
    }
}
