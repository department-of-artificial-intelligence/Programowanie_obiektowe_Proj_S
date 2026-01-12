using Project.Models.Common;

namespace Project.Models
{
    public enum PaymentMethod
    {
        Cash,
        CreditCard,
    }

    public class Reservation : Base
    {
        public string SeanceId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerFullName => $"{CustomerFirstName} {CustomerLastName}";

        public Reservation()
        {
            SeanceId = string.Empty;
            CustomerFirstName = string.Empty;
            CustomerLastName = string.Empty;
            CustomerEmail = string.Empty;
            CustomerPhone = string.Empty;
            PaymentMethod = string.Empty;
        }

        public Reservation(string seanceId, string customerFirstName, string customerLastName, string customerEmail, string customerPhone, string paymentMethod) : base()
        {
            ValidateReservationData(seanceId, customerFirstName, customerLastName, customerEmail, customerPhone, paymentMethod);

            SeanceId = seanceId;
            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            PaymentMethod = paymentMethod;
        }

        private static void ValidateReservationData(string seanceId, string firstName, string lastName, string email, string phone, string paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(seanceId)) throw new ArgumentException("Seance ID is required", nameof(seanceId));
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required", nameof(lastName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone is required", nameof(phone));
            if (string.IsNullOrWhiteSpace(paymentMethod)) throw new ArgumentException("Payment method is required", nameof(paymentMethod));
        }

        public void UpdateCustomerInfo(string firstName, string lastName, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required", nameof(lastName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone is required", nameof(phone));

            CustomerFirstName = firstName;
            CustomerLastName = lastName;
            CustomerEmail = email;
            CustomerPhone = phone;

            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Reservation for: {CustomerFullName}\n" +
                   $"Contact: {CustomerEmail} | {CustomerPhone}\n" +
                   $"Seance: {SeanceId}\n" +
                   $"Payment: {PaymentMethod}\n" +
                   $"ID: {Id}";
        }
    }
}