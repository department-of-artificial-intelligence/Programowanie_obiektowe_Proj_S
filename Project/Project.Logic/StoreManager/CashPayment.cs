using Project.Model.Interfaces;
using Project.Model.People;
using Project.Model.Interfaces;
using Project.Model.Orders;


using System;

namespace Project.Logic.Payments
{
    public class CashPayment : IPayment
    {
        public bool Pay(decimal amount, Customer customer)
        {
            Console.WriteLine($"[GOTÓWKA] Wybrano płatność przy odbiorze.");
            Console.WriteLine($"[GOTÓWKA] Proszę przygotować kwotę: {amount} PLN dla kuriera.");

            return true;
        }
    }
}