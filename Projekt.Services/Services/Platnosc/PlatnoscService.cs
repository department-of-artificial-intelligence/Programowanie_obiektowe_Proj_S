
namespace Projekt.Services.Platnosc;

using Projekt.Models;
using Projekt.Models.Enums;
using Projekt.Repositories.Interfaces;

public class PlatnoscService : IPlatnoscService
{
    private readonly IPlatnoscRepository _platnoscRepository;
    private readonly ILekcjaRepository _lekcjaRepository;

    public PlatnoscService(
        IPlatnoscRepository platnoscRepository,
        ILekcjaRepository lekcjaRepository)
    {
        _platnoscRepository = platnoscRepository;
        _lekcjaRepository = lekcjaRepository;
    }

    public bool CreatePayment(Platnosc payment)
    {
        if (payment == null)
            return false;

        var lekcja = _lekcjaRepository.GetById(payment.LekcjaId);

        if (lekcja == null)
            return false;

        if (payment.Amount <= 0)
            return false;

        decimal remaining = GetRemainingAmount(payment.LekcjaId);

        if (payment.Amount > remaining)
            return false;

        _platnoscRepository.AddPayment(payment);

        return true;
    }

    public bool EditPaymentMethod(
        Platnosc payment,
        TypPlatnosci paymentmethod)
    {
        if (payment == null)
            return false;

        if (payment.PaymentStatus == StatusPlatnosci.Zrealizowana)
            return false;

        payment.PaymentMethod = paymentmethod;

        return _platnoscRepository.UpdatePayment(payment);
    }

    public bool EditPaymentStatus(
        Platnosc payment,
        StatusPlatnosci status)
    {
        if (payment == null)
            return false;

        if (payment.PaymentStatus == StatusPlatnosci.Zrealizowana)
            return false;

        payment.PaymentStatus = status;

        return _platnoscRepository.UpdatePayment(payment);
    }

    public bool EditText(Platnosc payment, string txt)
    {
        if (payment == null)
            return false;

        payment.Opis = txt ?? string.Empty;

        return _platnoscRepository.UpdatePayment(payment);
    }

    public Platnosc? GetPaymentById(int id)
    {
        return _platnoscRepository.GetPaymentById(id);
    }

    public IEnumerable<Platnosc> GetPaymentsByLessonId(int lekcjaId)
    {
        return _platnoscRepository.GetPaymentsByLessonId(lekcjaId);
    }

    public decimal GetTotalPaid(int lekcjaId)
    {
        return GetPaymentsByLessonId(lekcjaId)
            .Where(p => p.PaymentStatus == StatusPlatnosci.Zrealizowana)
            .Sum(p => p.Amount);
    }

    public decimal GetRemainingAmount(int lekcjaId)
    {
        var lekcja = _lekcjaRepository.GetById(lekcjaId);

        if (lekcja == null)
            return 0;

        return Math.Max(0, lekcja.Cost - GetTotalPaid(lekcjaId));
    }

    public bool IsLessonPaid(int lekcjaId)
    {
        var lekcja = _lekcjaRepository.GetById(lekcjaId);

        if (lekcja == null)
            return false;

        return GetRemainingAmount(lekcjaId) == 0;
    }
}
