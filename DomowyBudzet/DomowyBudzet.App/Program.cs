using DomowyBudzet.Model.DataModels;

namespace DomowyBudzet.App;

internal class Program {
    static void Main(string[] args) {
        User us = new User();
        us.Name = "Jan";
        Console.WriteLine(us.Name);
    }
}
