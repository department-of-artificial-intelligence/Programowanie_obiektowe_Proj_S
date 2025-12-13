using Project.Model;
using System.Linq;


public class ReportGenerator
{

    public void GroupClientsByGoal(List<Client> allClients)
    {
        Console.WriteLine("grupowanie po celu) ---");

        var goalGroups = allClients
            .GroupBy(c => c.TrainingGoal)
            .OrderByDescending(g => g.Count());

        foreach (var group in goalGroups)
        {
            Console.WriteLine($"\n[GOAL: {group.Key}] - Client Count: {group.Count()}");
            foreach (var client in group)
            {
                Console.WriteLine($"- {client.FirstName} {client.LastName} (Weight: {client.Weight}kg)");
            }
        }
    }
        public void DisplayAllData(
        List<Client> Clients,
        List<Trainer> Trainers,
        List<Exercise> Exercises,
        List<Workout> Workouts)
    {
        Console.WriteLine("\n\n--- ZESTAWIENIE WSZYSTKICH DANYCH SYSTEMU ---");
        Console.WriteLine("-------------------------------------------------");

        // 1. Klienci
        Console.WriteLine($"[KLIENCI] (Liczba: {Clients.Count})");
        foreach (var c in Clients)
        {
            Console.WriteLine($"  -> ID: {c.Id} | {c.FirstName} {c.LastName} | Cel: {c.TrainingGoal} | Waga: {c.Weight}kg");
        }

        // 2. Trenerzy
        Console.WriteLine($"\n[TRENERZY] (Liczba: {Trainers.Count})");
        foreach (var t in Trainers)
        {
            Console.WriteLine($"  -> ID: {t.Id} | {t.FirstName} {t.LastName} | Specjalizacja: {t.Specialization} | Stawka: {t.HourlyRate:C}");
        }

        // 3. Ćwiczenia
        Console.WriteLine($"\n[ĆWICZENIA] (Liczba: {Exercises.Count})");
        foreach (var e in Exercises)
        {
            Console.WriteLine($"  -> ID: {e.Id} | {e.Name} | Partia: {e.MuscleGroup}");
        }

        Console.WriteLine($"\n[TRENINGI] (Liczba: {Workouts.Count})");
        foreach (var w in Workouts)
        {
            Console.WriteLine($"  -> ID: {w.Id} | Data: {w.Date.ToShortDateString()} | Klient: {w.Client.LastName}");
            // Wyświetlanie szczegółów serii dla każdego treningu
            foreach (var set in w.Sets)
            {
                
                Console.WriteLine($"     -> {set.Exercise.Name}: {set.SetCount} serii po {set.Repetitions} powtórzeń ({set.WeightUsed}kg)");
            }
        }
        Console.WriteLine("\n-------------------------------------------------");
    }
}
    
   

