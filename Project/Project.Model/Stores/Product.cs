using System.Diagnostics.CodeAnalysis;

namespace Project.Model.Stores
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

        
        public required ProductCategory Category { get; set; }

       
        public Product() { }

        [SetsRequiredMembers]
        public Product(string name, decimal price, ProductCategory category)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nazwa produktu nie może być pusta.");
            }

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