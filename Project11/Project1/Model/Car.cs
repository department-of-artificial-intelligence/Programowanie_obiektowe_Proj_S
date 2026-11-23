using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    internal class Car
    {
        private int _id;
        private string _model;
        private decimal _enginePower;
        private decimal _pricePerHour;
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("IdOutOfRange");
                _id = value;
            }
        }
        public string Model
        {
            get => _model;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("ModelIsEmpty");
                _model = value;
            }
        }
        public decimal EnginePower
        {
            get => _enginePower;
            set
            {
                if (value < 0m) throw new ArgumentOutOfRangeException("EnginePowerOutOfRange");
                _enginePower = value;
            }
        }
        public decimal PricePerHour
        {
            get => _pricePerHour;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("PricePerHourOutOfRange");
                _pricePerHour = value;
            }
        }
        public bool IsAvailable { get; private set; }
        public Car(int id, string model, int enginePower, decimal pricePerHour, bool isAvailable) 
        {
            Id = id;
            Model = model;
            EnginePower = enginePower;
            PricePerHour = pricePerHour;
            IsAvailable = true;
        }
        public void Rent() 
        {
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
