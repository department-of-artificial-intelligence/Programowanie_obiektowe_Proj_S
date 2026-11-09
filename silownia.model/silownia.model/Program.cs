namespace silownia.model
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Klient k1 = new Klient(1, "Jan", "Kowalski", "123456789", "Karnet-jedno dniowy");
            Console.WriteLine(k1.PobierzDane());

            Console.ReadKey();
        }
    }
}
