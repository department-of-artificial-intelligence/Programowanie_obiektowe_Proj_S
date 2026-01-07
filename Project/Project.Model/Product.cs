using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public required ProductCategory Category { get; set; }

        public Product() { }

        [SetsRequiredMembers]
        public Product(int id, string name, string manufacturer, decimal price, ProductCategory category)
        {
            Id = id;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            Category = category;
        }

        public string GetDescription()
        {
            return $"{Name} ({Manufacturer}) - {Price:C} [{Category}]";
        }
    }
}