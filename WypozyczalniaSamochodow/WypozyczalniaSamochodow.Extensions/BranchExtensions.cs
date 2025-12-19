using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Extensions
{
    public static class BranchExtensions
    {
        public static bool HasActiveRentals(this Branch branch) => branch.Rentals.Any(r => !r.IsCompleted);

        public static void ValidateBranch(this Branch branch)
        {
            if (string.IsNullOrWhiteSpace(branch.Name))
                throw new ArgumentException("Nazwa oddziału jest wymagana");

            if (string.IsNullOrWhiteSpace(branch.City))
                throw new ArgumentException("Miasto oddziału jest wymagane");

            if (string.IsNullOrWhiteSpace(branch.Address))
                throw new ArgumentException("Adres oddziału jest wymagany");

            if (string.IsNullOrWhiteSpace(branch.ContactNumber) || branch.ContactNumber.Length != 9 || !branch.ContactNumber.All(char.IsDigit)) 
                throw new ArgumentException("Numer kontaktowy musi składać się z 9 cyfr");
        }
    }
}
