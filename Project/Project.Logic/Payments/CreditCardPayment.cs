using Project.Model.Interfaces;
using Project.Model.People;
using Project.Model.Interfaces;
using Project.Model.Orders;

using System;

namespace Project.Logic.Payments
{
    public class CreditCardPayment : IPayment
    {
        private readonly string _cardNumber;
        private readonly string _owner;

        public CreditCardPayment(string cardNumber, string owner)
        {
            if (cardNumber.Length < 16)
            {
                throw new ArgumentException("Numer karty musi mieć co najmniej 16 cyfr.");
            }
            _cardNumber = cardNumber;
            _owner = owner;
        }

        public bool Pay(decimal amount, Customer customer)
        {
            string maskedCard = $"****-****-****-{_cardNumber.Substring(_cardNumber.Length - 4)}";
            Console.WriteLine($"[KARTA] Autoryzacja karty {maskedCard} (Właściciel: {_owner})...");

            if (customer.WalletBalance < amount)
            {
                Console.WriteLine($"[KARTA BŁĄD] Odrzucenie transakcji. Brak środków.");
                return false;
            }

            customer.WalletBalance -= amount;
            Console.WriteLine($"[KARTA SUKCES] Transakcja na kwotę {amount} PLN zaakceptowana.");
            return true;
        }
    }
}
