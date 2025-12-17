namespace Project.Model
{
    public class Service
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Nazwa { get; set; } = "";
        public decimal Cena { get; set; }

        public override string ToString() => $"Usługa {Nazwa} - {Cena:C}";
    }
}
