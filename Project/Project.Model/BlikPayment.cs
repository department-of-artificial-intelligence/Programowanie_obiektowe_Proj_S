using System;
using System.Text.RegularExpressions;

namespace Project.Model
{
    public class BlikPayment : IPayment
    {
        private string _blikCode;

        public BlikPayment(string code)
        {
            if (!Regex.IsMatch(code, @"^\d{6}$"))
            {
                throw new ArgumentException("Kod BLIK musi składać się z 6 cyfr.");
            }
            _blikCode = code;
        }

        public bool Pay(decimal amount, Customer customer)
        {
            Console.WriteLine($"[BLIK] Łączenie z systemem płatności...");

            if (customer.WalletBalance < amount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[BŁĄD] Brak środków na koncie! Masz: {customer.WalletBalance:C}, Potrzebujesz: {amount:C}");
                Console.ResetColor();
                return false;
            }

            customer.WalletBalance -= amount;

            Console.WriteLine($"[BLIK] Autoryzacja kodu {_blikCode}...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[BLIK] Pobrano {amount:C} z portfela. Pozostało: {customer.WalletBalance:C}");
            Console.ResetColor();
            return true;
        }
    }
}