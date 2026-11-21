
namespace Projekt.Repositories.Interfaces;

using Projekt.Models;

public interface IPlatnoscRepository
{
    void AddPayment(Platnosc payment);

    Platnosc? GetPaymentById(int id);

    IEnumerable<Platnosc> GetAllPayments();

    IEnumerable<Platnosc> GetPaymentsByLessonId(int lekcjaId);

    bool UpdatePayment(Platnosc payment);
}
