using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model
{
    public class Client : Person
    {
        public DateTime ContactDate { get; set; }
        public Excursion TripBooked { get; set; }

        public Client() { }
        public Client(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Age = person.Age;
        }
        public Client(Person person, DateTime cd)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Age = person.Age;
            ContactDate = cd;
        }

        public void ShowBookedTrip()
        {
            if (TripBooked != null) {
                Console.WriteLine($"{this.FirstName} {this.LastName} ma wycieczkę do {TripBooked} ");
            }
            else { Console.WriteLine($"{this.FirstName} {this.LastName} nie ma zabookowanej zadnej wycieczki"); }
        }

        public override string ToString()
        {
            return $"klient nazywa sie {FirstName} {LastName} ma {Age} lat i poznalismy go {ContactDate}";
        }
    }
}
