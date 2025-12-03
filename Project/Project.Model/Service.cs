namespace Project.Model
{
    public class Service : IHotelElement
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = "";
        public decimal Cena { get; set; } = 0m;

        public string Info() => ToString();
        public override string ToString() => $"Usługa {Id}: {Nazwa} - {Cena:C}";
    }
}
