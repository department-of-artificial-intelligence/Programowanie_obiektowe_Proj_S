using Gemini.dataModel;

namespace Gemini.DAL
{
    public class FakeDataStore
    {
        public static List<Pharmacy> Pharmacies { get; set; } = new();
        public static List<Category> Categories { get; set; } = new();
        public static List<Medicine> Medicines { get; set; } = new();
        public static List<PharmacyInventory> Inventories { get; set; } = new();
        public static List<Sale> Sales { get; set; } = new();
        public static List<SaleItem> SaleItems { get; set; } = new();


        public static void Initialize()
        {
            var p1 = new Pharmacy { Id = 1, Name = "Apteka Gemini Centrum", City = "Częstochowa", Address = "Wolności 10", PostalCode = "42-200", Phone = "123456789" };
            var p2 = new Pharmacy { Id = 2, Name = "Apteka Gemini Północ", City = "Częstochowa", Address = "Armii Krajowej 15", PostalCode = "42-201", Phone = "987654321" };
            Pharmacies.AddRange(new[] { p1, p2 });

            var cat1 = new Category { Id = 1, Name = "Przeciwbólowe" };
            var cat2 = new Category { Id = 2, Name = "Antybiotyki" };
            var cat3 = new Category { Id = 3, Name = "Witaminy" };
            Categories.AddRange(new[] { cat1, cat2, cat3 });

            var m1 = new Medicine { Id = 1, Name = "Apap 500mg", Price = 14.99m, CategoryId = 1, Category = cat1, RequiresPrescription = false };
            var m2 = new Medicine { Id = 2, Name = "Ibuprom Max", Price = 22.50m, CategoryId = 1, Category = cat1, RequiresPrescription = false };
            var m3 = new Medicine { Id = 3, Name = "Amotaks 1g", Price = 35.00m, CategoryId = 2, Category = cat2, RequiresPrescription = true };
            var m4 = new Medicine { Id = 4, Name = "Witamin C 1000", Price = 18.00m, CategoryId = 3, Category = cat3, RequiresPrescription = false };
            Medicines.AddRange(new[] { m1, m2, m3, m4 });

            Inventories.AddRange(new[]
            {
                new PharmacyInventory { Id = 1, PharmacyId = 1, MedicineId = 1, Quantity = 50, ExpiryDate = DateTime.Now.AddMonths(12) },
                new PharmacyInventory { Id = 2, PharmacyId = 1, MedicineId = 3, Quantity = 5, ExpiryDate = DateTime.Now.AddMonths(2) },
                new PharmacyInventory { Id = 3, PharmacyId = 2, MedicineId = 2, Quantity = 30, ExpiryDate = DateTime.Now.AddMonths(8) },
                new PharmacyInventory { Id = 4, PharmacyId = 2, MedicineId = 4, Quantity = 100, ExpiryDate = DateTime.Now.AddMonths(18) }
            });

            var s1 = new Sale { Id = 1, PharmacyId = 1, UserId = 1, SaleDate = DateTime.Now.AddDays(-2), TotalAmount = 52.49m };
            Sales.Add(s1);

            SaleItems.AddRange(new[]
            {
                new SaleItem { Id = 1, SaleId = 1, MedicineId = 1, Quantity = 2, UnitPrice = 14.99m },
                new SaleItem { Id = 2, SaleId = 1, MedicineId = 2, Quantity = 1, UnitPrice = 22.50m }
            });
        }
    }
}
