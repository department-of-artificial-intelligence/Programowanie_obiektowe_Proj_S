using System;

namespace Project.Model
{
    public class CashPayment : IPayment
    {
        public bool Pay(decimal amount, Customer customer)
        {
            Console.WriteLine("[GOTÓWKA] Wybrano płatność przy odbiorze.");
            Console.WriteLine($"[GOTÓWKA] Prosimy przygotować odliczoną kwotę: {amount:C} dla kuriera.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[INFO] Pieniądze nie zostały pobrane z wirtualnego portfela.");
            Console.ResetColor();

            return true;
        }
    }
}