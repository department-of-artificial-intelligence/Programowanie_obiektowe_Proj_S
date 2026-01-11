using System.Text.RegularExpressions;
using Project.Model.People;
using Project.Model.Interfaces;
using Project.Model.Orders;


namespace Project.Logic.Payments
{
    public class BlikPayment : IPayment
    {
        private readonly string _blikCode;

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
            Console.WriteLine($"[BLIK] Łączenie z systemem płatności... (Kod: {_blikCode})");

            if (customer.WalletBalance < amount)
            {
                Console.WriteLine($"[BLIK BŁĄD] Brak środków. Masz: {customer.WalletBalance} PLN, Wymagane: {amount} PLN");
                return false;
            }

            customer.WalletBalance -= amount;
            Console.WriteLine($"[BLIK SUKCES] Pobrano {amount} PLN. Pozostało: {customer.WalletBalance} PLN");
            return true;
        }
    }
}