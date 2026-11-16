using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    internal class Car
    {
        public string Model { get; set; }
        public int EnginePower { get; set; }
        public int Price { get; set; }
        public bool Status { get; private set; }
        public Car(string model, int enginePower, int price, bool status) {
            Model = model;
            EnginePower = enginePower;
            Price = price;
            Status = status;
        }
        
    }
}
