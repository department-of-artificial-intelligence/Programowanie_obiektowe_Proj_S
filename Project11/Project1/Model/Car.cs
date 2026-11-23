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
        private CarStatus _status;
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
        public CarStatus Status
        {
            get => _status;
            private set
            {
                _status = value;
            }
        }
        public Car(int id, string model, decimal enginePower, decimal pricePerHour, CarStatus status) 
        {
            Id = id;
            Model = model;
            EnginePower = enginePower;
            PricePerHour = pricePerHour;
            Status = status;
        }
        public void Rent() 
        {
            if (Status!=CarStatus.Available) throw new Exception("Car is rented.");
            Status = CarStatus.Rented;
        }
        public void Return()
        {
            if (Status != CarStatus.Rented) throw new InvalidOperationException("Car is not rented.");
            Status = CarStatus.Available;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"EnginePower: {EnginePower}");
            Console.WriteLine($"PricePerHour: {PricePerHour}");
            Console.WriteLine($"Status: {Status}");
        }
    }
}
