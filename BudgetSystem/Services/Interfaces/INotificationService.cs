using Models;

namespace Services.Interfaces
{
    public interface INotificationService
    {
        void SendReminder(Expense recurringExpense);
    }
}
