using System;
using System.Linq;
using Gemini.DAL;
using Gemini.dataModel;
using Gemini.Services;

namespace Gemini
{
    internal class Program
    {
        private static readonly IPharmacyStatisticsService _statsService = new PharmacyStatisticsService();
        private static int _currentPharmacyId = 1;

        static void Main(string[] args)
        {
            FakeDataStore.Initialize();

            bool exit = false;
            while (!exit)
            {
                var activePharmacy = GetActivePharmacy();

                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("       SYSTEM APTEKI GEMINI - CONSOLE PANEL       ");
                Console.WriteLine($"       ACTIVE BRANCH: [{activePharmacy.Name.ToUpper()}]");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Browse Medicine Catalog & Local Stock");
                Console.WriteLine("2. Check Inventory & Stock Status");
                Console.WriteLine("3. Run LINQ Analytics & Reports");
                Console.WriteLine("4. Process Sale (Deduct Stock)");
                Console.WriteLine("5. Admin Panel: Add New Medicine");
                Console.WriteLine("6. Switch Active Home Pharmacy Branch");
                Console.WriteLine("0. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowMedicineCatalog();
                        break;
                    case "2":
                        ShowInventory();
                        break;
                    case "3":
                        ShowAnalyticsMenu();
                        break;
                    case "4":
                        ProcessSale();
                        break;
                    case "5":
                        AddNewMedicine();
                        break;
                    case "6":
                        SwitchActivePharmacy();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;
                    default:
                        Pause("Invalid option. Press Any Key to try again...");
                        break;
                }
            }
        }

        private static Pharmacy GetActivePharmacy()
        {
            return FakeDataStore.Pharmacies.FirstOrDefault(p => p.Id == _currentPharmacyId)
                   ?? FakeDataStore.Pharmacies.First();
        }

        private static void SwitchActivePharmacy()
        {
            Console.Clear();
            Console.WriteLine("=== SELECT ACTIVE HOME PHARMACY ===");
            foreach (var p in FakeDataStore.Pharmacies)
            {
                string activeTag = p.Id == _currentPharmacyId ? " (CURRENT)" : "";
                Console.WriteLine($"ID: {p.Id} | {p.Name} - {p.Address}, {p.City}{activeTag}");
            }

            Console.Write("\nEnter Pharmacy ID to switch: ");
            if (int.TryParse(Console.ReadLine(), out int selectedId) && FakeDataStore.Pharmacies.Any(p => p.Id == selectedId))
            {
                _currentPharmacyId = selectedId;
                var newBranch = GetActivePharmacy();
                Console.WriteLine($"\n✅ Active branch changed to: {newBranch.Name}");
            }
            else
            {
                Console.WriteLine("\n❌ Invalid Pharmacy ID!");
            }
            Pause();
        }

        private static void ShowMedicineCatalog()
        {
            Console.Clear();
            var activeBranch = GetActivePharmacy();
            Console.WriteLine($"=== MEDICINE CATALOG & STOCK AT [{activeBranch.Name}] ===");

            foreach (var med in FakeDataStore.Medicines)
            {
                var inv = FakeDataStore.Inventories.FirstOrDefault(i => i.PharmacyId == _currentPharmacyId && i.MedicineId == med.Id);
                int stockQty = inv?.Quantity ?? 0;

                string rx = med.RequiresPrescription ? "[Rx Required]" : "[OTC]";
                string catName = med.Category?.Name ?? "General";

                Console.WriteLine($"ID: {med.Id,-3} | {med.Name,-18} | Stock: {stockQty,-4} pcs | Price: {med.Price.ToPln(),-10} | {rx}");
            }
            Pause();
        }

        private static void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("=== ALL PHARMACY INVENTORIES ===");
            foreach (var inv in FakeDataStore.Inventories)
            {
                var pharmacy = FakeDataStore.Pharmacies.FirstOrDefault(p => p.Id == inv.PharmacyId);
                var med = FakeDataStore.Medicines.FirstOrDefault(m => m.Id == inv.MedicineId);

                string currentMarker = inv.PharmacyId == _currentPharmacyId ? " ⭐ [HOME]" : "";
                string warning = inv.Quantity < 10 ? "⚠️ LOW STOCK!" : "OK";

                Console.WriteLine($"Branch: {pharmacy?.Name,-22}{currentMarker,-9} | Medicine: {med?.Name,-16} | Qty: {inv.Quantity,-4} | Status: {warning}");
            }
            Pause();
        }

        private static void ProcessSale()
        {
            Console.Clear();
            var activeBranch = GetActivePharmacy();
            Console.WriteLine($"=== NEW SALE REGISTER - [{activeBranch.Name}] ===");

            foreach (var m in FakeDataStore.Medicines)
            {
                var stock = FakeDataStore.Inventories.FirstOrDefault(i => i.PharmacyId == _currentPharmacyId && i.MedicineId == m.Id);
                Console.WriteLine($"ID: {m.Id,-3} | {m.Name,-18} | Price: {m.Price.ToPln(),-10} | In Stock: {(stock?.Quantity ?? 0)} pcs");
            }

            Console.Write("\nEnter Medicine ID to purchase: ");
            if (!int.TryParse(Console.ReadLine(), out int medId) || !FakeDataStore.Medicines.Any(m => m.Id == medId))
            {
                Pause("❌ Invalid Medicine ID!");
                return;
            }

            var selectedMed = FakeDataStore.Medicines.First(m => m.Id == medId);

            var inventory = FakeDataStore.Inventories.FirstOrDefault(i => i.PharmacyId == _currentPharmacyId && i.MedicineId == medId);

            if (inventory == null || inventory.Quantity <= 0)
            {
                Pause($"❌ Item '{selectedMed.Name}' is OUT OF STOCK at {activeBranch.Name}!");
                return;
            }

            Console.Write($"Enter Quantity for {selectedMed.Name} (Available: {inventory.Quantity}): ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Pause("❌ Invalid Quantity!");
                return;
            }

            if (qty > inventory.Quantity)
            {
                Pause($"❌ Insufficient stock! Requested: {qty}, Available: {inventory.Quantity} pcs.");
                return;
            }

            if (selectedMed.RequiresPrescription)
            {
                Console.Write("This medicine requires a prescription. Enter Patient PESEL: ");
                string? pesel = Console.ReadLine();

                if (!pesel.IsValidPesel())
                {
                    Pause("❌ Invalid PESEL! Transaction aborted.");
                    return;
                }
            }

            decimal totalCost = selectedMed.Price * qty;

            inventory.Quantity -= qty;

            var newSale = new Sale
            {
                Id = FakeDataStore.Sales.Count + 1,
                PharmacyId = _currentPharmacyId,
                UserId = 1,
                SaleDate = DateTime.Now,
                TotalAmount = totalCost
            };
            FakeDataStore.Sales.Add(newSale);

            FakeDataStore.SaleItems.Add(new SaleItem
            {
                Id = FakeDataStore.SaleItems.Count + 1,
                SaleId = newSale.Id,
                MedicineId = selectedMed.Id,
                Quantity = qty,
                UnitPrice = selectedMed.Price
            });

            Console.WriteLine($"\n✅ TRANSACTION SUCCESSFUL!");
            Console.WriteLine($"Deducted {qty} box(es) of {selectedMed.Name} from {activeBranch.Name}.");
            Console.WriteLine($"Remaining Stock: {inventory.Quantity} pcs.");
            Console.WriteLine($"Total Paid: {totalCost.ToPln()}");
            Pause();
        }

        private static void ShowAnalyticsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== LINQ ANALYTICS & REPORTS ===");

            Console.WriteLine($"Average Medicine Price: {_statsService.GetAverageMedicinePrice().ToPln()}");

            Console.WriteLine("\n--- Medicine Count by Category ---");
            foreach (var kvp in _statsService.GetMedicineCountByCategory())
            {
                Console.WriteLine($"• {kvp.Key,-20}: {kvp.Value} items");
            }

            Console.WriteLine("\n--- Total Revenue by Pharmacy Branch ---");
            foreach (var kvp in _statsService.GetTotalSalesByPharmacy())
            {
                Console.WriteLine($"• {kvp.Key,-25}: {kvp.Value.ToPln()}");
            }

            Console.WriteLine("\n--- Top Selling Products (DTO) ---");
            foreach (var item in _statsService.GetTopSellingMedicines())
            {
                Console.WriteLine($"• {item.MedicineName,-18} | Units Sold: {item.TotalQuantitySold,-4} | Revenue: {item.TotalRevenue.ToPln()}");
            }

            Pause();
        }
        private static void AddNewMedicine()
        {
            Console.Clear();
            Console.WriteLine("=== ADMIN PANEL: ADD NEW MEDICINE ===");

            Console.Write("Enter Medicine Name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter Price (e.g. 19.99): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Pause("Invalid price format!");
                return;
            }

            Console.Write("Requires Prescription? (y/n): ");
            bool requiresRx = Console.ReadLine()?.Trim().ToLower() == "y";

            int newId = FakeDataStore.Medicines.Max(m => m.Id) + 1;
            var newMed = new Medicine
            {
                Id = newId,
                Name = name ?? "Unnamed Medicine",
                Price = price,
                CategoryId = 1,
                RequiresPrescription = requiresRx
            };

            FakeDataStore.Medicines.Add(newMed);

            foreach (var pharmacy in FakeDataStore.Pharmacies)
            {
                FakeDataStore.Inventories.Add(new PharmacyInventory
                {
                    Id = FakeDataStore.Inventories.Count + 1,
                    PharmacyId = pharmacy.Id,
                    MedicineId = newMed.Id,
                    Quantity = 20,
                    ExpiryDate = DateTime.Now.AddMonths(12),
                    BatchNumber = $"BATCH-{newId}00"
                });
            }

            Console.WriteLine($"\n✅ Medicine '{newMed.Name}' added with ID #{newMed.Id} and 20 initial stock per branch!");
            Pause();
        }

        private static void Pause(string message = "\nPress Any Key to return to menu...")
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}