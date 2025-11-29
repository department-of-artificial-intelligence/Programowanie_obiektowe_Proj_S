using Project.Model;
using System.Linq;

// ReportGenerator.cs
public class ReportGenerator
{
    // LAMBDA EXPRESSION FOR GROUPING
    public void GroupClientsByGoal(List<Client> allClients)
    {
        Console.WriteLine("\n\n--- 📋 REPORT 1: Clients Grouped by Training Goal (LINQ GroupBy with Lambda) ---");

        // Using lambda expression 'c => c.TrainingGoal' for grouping
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

    // Interface utilization (Polymorphism)
    public void GenerateReportSummary(List<IReportable> reportList)
    {
        Console.WriteLine("\n\n--- 📈 REPORT 2: Reportable Items Summary (Polymorphism with Interface) ---");
        foreach (var element in reportList)
        {
            // Calls methods/properties defined in the IReportable interface
            Console.WriteLine($"\n{element.ReportDescription}");
            element.DisplayDetails();
        }
    }
}
