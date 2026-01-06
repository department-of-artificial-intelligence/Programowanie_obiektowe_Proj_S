using System;

namespace Project.Model
{
    public abstract class Product
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Manufacturer { get; set; } 

        
        public required ProductCategory Category { get; set; }

        
        protected Product() { }

       
        public virtual string GetDescription()
        {
            return $"{Name} ({Manufacturer}) - {Price:C}";
        }

        public override string ToString()
        {
            return GetDescription();
        }
    }
}