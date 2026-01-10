using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public class Product
    {
        
        public int ProductId { get; private set; }

        public required string Name { get; set; }
        public string Description { get; set; } = "Brak opisu";

        private decimal _price;
        public required decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    
                    throw new ArgumentOutOfRangeException(nameof(Price), "Cena produktu nie może być ujemna!");
                _price = value;
            }
        }

        
        public required string Category { get; set; }

       
        private Product() { }

        [SetsRequiredMembers]
        public Product(string name, decimal price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
        }

        public override string ToString()
        {
            string idInfo = ProductId == 0 ? "NOWY" : ProductId.ToString();
            
            return $"[PRODUKT #{idInfo}] {Name} ({Category}) | Cena: {Price:C}";
        }
    }
}