using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    internal class Car
    {
        public int Id { get; private set; }
        public string Model { get; set; }
        public int EnginePower { get; set; }
        public decimal PricePerHour { get; set; }
        public bool IsAvailable { get; private set; }
        public Car(int id, string model, int enginePower, decimal pricePerHour, bool isAvailable) {
            Id = id;
            Model = model;
            EnginePower = enginePower;
            PricePerHour = pricePerHour;
            IsAvailable = true;
        }
        public void Rent() {
            if (!IsAvailable)
            {
                throw new Exception("Car is rented.");
            }
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"EnginePower: {EnginePower}");
            Console.WriteLine($"PricePerHour: {PricePerHour}");
            Console.WriteLine($"IsAvailable: {IsAvailable}");
        }
        
    }
}
