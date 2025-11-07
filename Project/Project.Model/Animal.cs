
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Animal
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public int Age { get; set; }
        public double? WeightKg { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public List<Appointment> Appointments { get; set; } = new();



        public void AddAppointment(Appointment appointment)
        {
            if (!Appointments.Contains(appointment))
            {
                Appointments.Add(appointment);
                appointment.Animal = this;
                appointment.AnimalId = this.Id;
            }
        }

        public void RemoveAppointment(Appointment appointment)
        {
            if (Appointments.Contains(appointment))
            {
                Appointments.Remove(appointment);
                appointment.Animal = null;
            }
        }

        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return Appointments.Where(a => a.Data.Date == date.Date).ToList();
        }

        public void UpdateAge(int newAge)
        {
            if (newAge < 0) throw new ArgumentException("Wiek nie może być ujemny.", nameof(newAge));
            Age = newAge;
        }

        public int GetAge() => Age;

        public void UpdateWeight(double newWeight)
        {
            if (newWeight <= 0) throw new ArgumentException("Waga musi być większa niż 0.", nameof(newWeight));
            WeightKg = newWeight;
        }

        public bool IsOverweight(double threshold)
        {
            if (WeightKg == null) throw new InvalidOperationException("Brak danych o wadze zwierzęcia.");
            return WeightKg > threshold;
        }
        public int GetId() => Id;

        public override string ToString()
        {
            var weightStr = WeightKg.HasValue ? $"{WeightKg.Value} kg" : "brak wagi";

            return $"{Name} ({Species ?? "gatunek nieznany"}{(Breed != null ? ", " + Breed : "")}), " + $"Wiek: {Age} lat, Waga: {weightStr}";

        }
    }


}
