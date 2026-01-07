using System;

namespace Project.Model
{
    public class CreditCardPayment : IPayment
    {
        private string _cardNumber;
        private string _owner;

        public CreditCardPayment(string cardNumber, string owner)
        {
            if (cardNumber.Length < 16)
                throw new ArgumentException("Numer karty jest za krótki.");

            _cardNumber = cardNumber;
            _owner = owner;
        }

        public bool Pay(decimal amount, Customer customer)
        {
            string masked = $"****-****-****-{_cardNumber.Substring(_cardNumber.Length - 4)}";

            Console.WriteLine($"[KARTA] Weryfikacja karty {masked} ({_owner})...");

            if (customer.WalletBalance < amount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[BŁĄD] Transakcja odrzucona przez bank. Brak środków.");
                Console.ResetColor();
                return false;
            }

            customer.WalletBalance -= amount;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[KARTA] Pobrano {amount:C}. Dziękujemy.");
            Console.ResetColor();
            return true;
        }
    }
}