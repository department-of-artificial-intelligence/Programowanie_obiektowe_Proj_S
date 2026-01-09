#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;




namespace Project.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var cs = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cs));
                })
                .Build();

            var db = _host.Services.GetService<ApplicationDbContext>();
            if (db != null)
            {
                db.Database.Migrate();
            }

            RunMenu(db);
        }


        //MENU 
        static void RunMenu(ApplicationDbContext db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Siec gabinetów weterynaryjnych ===");
                Console.WriteLine("1. Kliniki");
                Console.WriteLine("2. Weterynarze");
                Console.WriteLine("3. Właściciele");
                Console.WriteLine("4. Zwierzęta");
                Console.WriteLine("5. Wizyty");
                Console.WriteLine("0. Wyjście");

                switch (Console.ReadLine())
                {
                    case "1": ClinicsMenu(db); break;
                    case "2": VeterinariansMenu(db); break;
                    case "3": OwnersMenu(db); break;
                    case "4": AnimalsMenu(db); break;
                    case "5": AppointmentsMenu(db); break;
                    case "0": return;
                }
            }
        }

        //KLINIKI
        static void ClinicsMenu(ApplicationDbContext db)
        {
            Console.Clear();
            Console.WriteLine("1. Dodaj");
            Console.WriteLine("2. Lista");
            Console.WriteLine("3. Usuń");

            switch (Console.ReadLine())
            {
                case "1":
                    db.Clinics.Add(new Clinic
                    {
                        Name = Read("Nazwa"),
                        Address = Read("Adres"),
                        Email = Read("Email"),
                        Phone = Read("Telefon")
                    });
                    db.SaveChanges();
                    break;

                case "2":
                    foreach (var c in db.Clinics.Include(c => c.Veterinarians))
                    {
                        Console.WriteLine($"{c.Id}: {c.Name}");
                        foreach (var v in c.Veterinarians)
                            Console.WriteLine($"   {v.FirstName} {v.LastName}");
                    }
                    Console.ReadKey();
                    break;

                case "3":
                    DeleteEntity(db.Clinics, db);
                    break;
            }
        }

        //WETERYNARZE
        static void VeterinariansMenu(ApplicationDbContext db)
        {
            Console.Clear();
            Console.WriteLine("1. Dodaj");
            Console.WriteLine("2. Lista");
            Console.WriteLine("3. Usuń");

            switch (Console.ReadLine())
            {
                case "1":
                    var clinic = Choose(db.Clinics.ToList(), "kliniki");
                    if (clinic == null) return;

                    db.Veterinarians.Add(new Veterinarian
                    {
                        FirstName = Read("Imię"),
                        LastName = Read("Nazwisko"),
                        Email = Read("Email"),
                        Phone = Read("Telefon"),
                        LicenseNumber = Read("Licencja"),
                        Specialty = Read("Specjalność"),
                        ClinicId = clinic.Id
                    });
                    db.SaveChanges();
                    break;

                case "2":
                    foreach (var v in db.Veterinarians.Include(v => v.Clinic))
                        Console.WriteLine($"{v.Id}: {v.FirstName} {v.LastName} | {v.Clinic?.Name}");
                    Console.ReadKey();
                    break;

                case "3":
                    DeleteEntity(db.Veterinarians, db);
                    break;
            }
        }

        //WŁAŚCICIELE
        static void OwnersMenu(ApplicationDbContext db)
        {
            Console.Clear();
            Console.WriteLine("1. Dodaj");
            Console.WriteLine("2. Lista");
            Console.WriteLine("3. Usuń");

            switch (Console.ReadLine())
            {
                case "1":
                    db.Owners.Add(new Owner
                    {
                        FirstName = Read("Imię"),
                        LastName = Read("Nazwisko"),
                        Email = Read("Email"),
                        Phone = Read("Telefon")
                    });
                    db.SaveChanges();
                    break;

                case "2":
                    foreach (var o in db.Owners.Include(o => o.Animals))
                    {
                        Console.WriteLine($"{o.Id}: {o.FirstName} {o.LastName}");
                        foreach (var a in o.Animals)
                            Console.WriteLine($"   {a.Name}");
                    }
                    Console.ReadKey();
                    break;

                case "3":
                    DeleteEntity(db.Owners, db);
                    break;
            }
        }

        //ZWIERZĘTA
        static void AnimalsMenu(ApplicationDbContext db)
        {
            Console.Clear();
            Console.WriteLine("1. Dodaj");
            Console.WriteLine("2. Lista");
            Console.WriteLine("3. Usuń");

            switch (Console.ReadLine())
            {
                case "1":
                    var owner = Choose(db.Owners.ToList(), "właściciela");
                    if (owner == null) return;

                    db.Animals.Add(new Animal
                    {
                        Name = Read("Imię"),
                        Species = Read("Gatunek"),
                        Breed = Read("Rasa"),
                        Age = int.Parse(Read("Wiek")),
                        WeightKg = double.Parse(Read("Waga")),
                        OwnerId = owner.Id
                    });
                    db.SaveChanges();
                    break;

                case "2":
                    foreach (var a in db.Animals.Include(a => a.Owner))
                        Console.WriteLine($"{a.Id}: {a.Name} | {a.Owner?.FirstName}");
                    Console.ReadKey();
                    break;

                case "3":
                    DeleteEntity(db.Animals, db);
                    break;
            }
        }

        //WIZYTY + LECZENIA
        static void AppointmentsMenu(ApplicationDbContext db)
        {
            Console.Clear();
            Console.WriteLine("1. Dodaj wizytę");
            Console.WriteLine("2. Lista wizyt");
            Console.WriteLine("3. Dodaj leczenie");
            Console.WriteLine("4. Usuń wizytę");

            switch (Console.ReadLine())
            {
                case "1":
                    var animal = Choose(db.Animals.ToList(), "zwierzę");
                    var vet = Choose(db.Veterinarians.ToList(), "weterynarza");
                    if (animal == null || vet == null) return;

                    db.Appointments.Add(new Appointment
                    {
                        AnimalId = animal.Id,
                        VeterinarianId = vet.Id,
                        Date = DateTime.Parse(Read("Data")),
                        Status = Read("Status"),
                        Notes = Read("Notatki")
                    });
                    db.SaveChanges();
                    break;

                case "2":
                    foreach (var a in db.Appointments
                        .Include(a => a.Animal)
                        .Include(a => a.Veterinarian)
                        .Include(a => a.Treatments))
                    {
                        Console.WriteLine($"{a.Id}: {a.Animal?.Name} | {a.Date:d}");
                        foreach (var t in a.Treatments)
                            Console.WriteLine($"   {t.Name} ({t.Cost} zł)");
                    }
                    Console.ReadKey();
                    break;

                case "3":
                    AddTreatmentToAppointment(db);
                    break;

                case "4":
                    DeleteEntity(db.Appointments, db);
                    break;
            }
        }

        //LECZENIE 
        static void AddTreatmentToAppointment(ApplicationDbContext db)
        {
            var appointments = db.Appointments
                .Include(a => a.Treatments)
                .ToList();

            var appointment = Choose(appointments, "wizytę");
            if (appointment == null) return;

            var treatment = new Treatment
            {
                Name = Read("Nazwa leczenia"),
                Description = Read("Opis"),
                Cost = decimal.Parse(Read("Koszt"))
            };

            appointment.Treatments.Add(treatment);
            db.SaveChanges();
        }

        static T Choose<T>(List<T> list, string name) where T : class
        {
            if (!list.Any())
            {
                Console.WriteLine($"Brak {name}.");
                Console.ReadKey();
                return null;
            }

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}. {list[i]}");

            if (!int.TryParse(Read($"Wybierz {name}"), out int index) ||
                index < 1 || index > list.Count)
                return null;

            return list[index - 1];
        }

        static string Read(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine();
        }

        static void DeleteEntity<T>(DbSet<T> set, ApplicationDbContext db) where T : class
        {
            foreach (var e in set)
                Console.WriteLine(e);

            if (!int.TryParse(Read("Id"), out int id)) return;

            var entity = set.Find(id);
            if (entity != null)
            {
                set.Remove(entity);
                db.SaveChanges();
            }

        }
    }
}
