using System.Globalization;
using Gemini.dataModel;

namespace Gemini.Services
{
    public static class PharmacyExtensions
    {
        public static string ToPln(this decimal amount)
        {
            return $"{amount:N2} zł";
        }

        public static IEnumerable<PharmacyInventory> WhereLowStock(this IEnumerable<PharmacyInventory> source, int threshold = 10)
        {
            return source.Where(item => item.Quantity < threshold);
        }

        public static decimal CalculateTotalValue(this IEnumerable<SaleItem> items)
        {
            return items.Sum(item => item.Quantity * item.UnitPrice);
        }

        public static bool IsValidPesel(this string? pesel)
        {
            return !string.IsNullOrWhiteSpace(pesel) && pesel.Length == 11 && pesel.All(char.IsDigit);
        }
    }
}