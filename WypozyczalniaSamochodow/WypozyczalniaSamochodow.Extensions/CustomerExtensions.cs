using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Extensions
{
    public static class CustomerExtensions
    {
        public static bool HasActiveRentals(this Customer customer) => customer.Rentals.Any(r => !r.IsCompleted);
        public static bool HasEnoughPoints(this Customer customer, int points) => customer.LoyaltyPoints >= points;

        public static void ValidateCustomer(this Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("Imię jest wymagane");

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Nazwisko jest wymagane");

            if (string.IsNullOrWhiteSpace(customer.LicenseNumber))
                throw new ArgumentException("Numer prawa jazdy jest wymagany");

            if (!customer.Email.Contains('@'))
                throw new ArgumentException("Nieprawidłowy adres email");

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber) || customer.PhoneNumber.Length != 9 || !customer.PhoneNumber.All(char.IsDigit)) 
                throw new ArgumentException("Numer kontaktowy musi składać się z 9 cyfr");
        }
    }
}
