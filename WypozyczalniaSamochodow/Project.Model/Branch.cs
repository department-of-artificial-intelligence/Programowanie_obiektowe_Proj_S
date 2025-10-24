using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaSamochodow.Model
{
    interface IBranch
    {
        void ShowBranches();
        void AddBranch(string name, string city);
        void RemoveBranch(string city);

        void AddCar(Car car);
        void RemoveCar(Car car);
    }

    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int CarsCount { get; set; }

        public List<Car> Cars;

        public Branch(): this(0, string.Empty, string.Empty) { }

        public Branch(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
            Cars = new List<Car>();
        }

        public void AddCar(Car car)
        {
            if (car == null) return;
            Cars.Add(car);
            CarsCount++;
        }

        public void RemoveCar(Car car)
        {
            if(car == null) return;
            Cars.Remove(car);
            CarsCount--;
        }

        public override string ToString()
        {
            string s = string.Format($"Oddział {Id}:\nNazwa oddziału: {Name}, Miasto: {City}, Liczba oferowanych samochodów: {CarsCount}\nOferta:\n");
            foreach (Car car in Cars)
            {
                s += car + "\n";
            }

            return s;
        }
    }
}
