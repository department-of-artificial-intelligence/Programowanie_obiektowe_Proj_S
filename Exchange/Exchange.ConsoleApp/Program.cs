namespace Exchange.ConsoleApp;
using Exchange.Model.DataModels;

internal class Program
{
    static void Main()
    {
        Employee test = new Employee() { FirstName = "Antoni", LastName = "Duda", Id = 27, BranchId=2 };
        Console.WriteLine(test);
    }
}
