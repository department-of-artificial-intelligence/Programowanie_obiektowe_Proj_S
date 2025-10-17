using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class Bicycle
    {
        public int Id {  get; set; }
        public string Model { get; set; }
        public bool Isavailable {  get; set; }
        public string Type { get; set; }
        public int Price {  get; set; }
        public Station CurrentStation { get; internal set; }

        public void Rent()
        {
            if (Isavailable != true)
                throw new Exception("Bycicle is not available at the moment");
            Isavailable = false;
        }

        public void Return(Station station)
        {
            Isavailable = true;
            CurrentStation = station;
        }

        public void ShowInfo() {
            Console.WriteLine($"ID: {Id}, Model: {Model}, Avaialable: {Isavailable}, Type of bycicle: {Type} Price per hour: {Price}");
        }
    }
}
