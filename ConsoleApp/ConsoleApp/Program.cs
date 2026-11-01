using System;
using System.Collections.Generic;
using CinemaNetworkManagement.Model;

namespace CinemaNetworkManagement.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            Employee employee = new Employee(1, "Jan", "Kowalski");
            Customer customer = new Customer(2, "Anna", "Nowak", "anna@example.com", 0);
            Address address = new Address("Czestochowa", "Kinowa", "12A", "42-200");
            Cinema cinema = new Cinema(1, "Kino Centrum", address);
            Movie movie = new Movie(1, "Przykladowy film", "Przygodowy", 120, 12);
            MovieLicense license = new MovieLicense(
                1, movie, 1000m, new DateTime(2026, 11, 1), new DateTime(2026, 11, 30));

            Console.WriteLine("CinemaNetworkManagement");
            Console.WriteLine("Osoby - wspolny typ Person:");

            List<Person> people = new List<Person>();
            people.Add(employee);
            people.Add(customer);

            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine($"Kino: {cinema.Name}, miasto: {cinema.Address.City}");
            Console.WriteLine($"Film: {movie.Title}, czas: {movie.DurationMinutes} minut");
            Console.WriteLine($"Licencja: {license.ValidFrom:yyyy-MM-dd} - {license.ValidTo:yyyy-MM-dd}");

            ScreeningRoom room = new ScreeningRoom(1, "Sala 1", cinema);
            Seat seat = new Seat(1, 1, 1, room);
            Screening screening = new Screening(
                1, movie, room, new DateTime(2026, 11, 15, 18, 0, 0), 40m);

            List<Ticket> tickets = new List<Ticket>();
            tickets.Add(new RegularTicket(1, customer, screening, seat));
            tickets.Add(new ReducedTicket(2, customer, screening, seat, 50m));

            Console.WriteLine("Kalkulacja cen - wspolny typ Ticket:");

            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine($"Bilet {ticket.Id}: {ticket.CalculatePrice():0.00}");
            }
        }
    }
}

