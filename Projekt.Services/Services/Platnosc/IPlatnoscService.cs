namespace Projekt.Services.Platnosc;

using Projekt.Models;
using Projekt.Models.Enums;

public interface IPlatnoscService
{
    bool CreatePayment(Platnosc payment);

    bool EditPaymentMethod(Platnosc payment, TypPlatnosci paymentmethod);
    bool EditPaymentStatus(Platnosc payment, StatusPlatnosci status);
    bool EditText(Platnosc payment, string txt);

    Platnosc? GetPaymentById(int id);
    IEnumerable<Platnosc> GetPaymentsByLessonId(int lekcjaId);

    decimal GetTotalPaid(int lekcjaId);
    decimal GetRemainingAmount(int lekcjaId);
    bool IsLessonPaid(int lekcjaId);
}