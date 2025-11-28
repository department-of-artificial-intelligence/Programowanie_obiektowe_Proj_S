using System.Collections.Generic;

namespace WypozyczalniaSamochodow.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public List<Car> Cars { get; set; } = new List<Car>();

        public Branch() { }
        public Branch(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
        }

        public override string ToString()
        {
            return $"Oddział {Id}: {Name} ({City}), Samochody: {Cars.Count}";

        }
    }
}
