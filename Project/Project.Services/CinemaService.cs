using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Models;

namespace Project.Services
{
    public static class CinemaService
    {
        public static List<Cinema> GetAll(ApplicationDBContext context)
        {
            return [.. context.Cinemas];
        }

        public static Cinema? GetById(ApplicationDBContext context, string id)
        {
            return context.Cinemas.FirstOrDefault(c => c.Id == id);
        }

        public static Cinema Add(ApplicationDBContext context, string name, string address, string contactPhone, string contactEmail, string managerName)
        {
            var cinema = new Cinema(name, address, contactPhone, contactEmail, managerName);

            context.Cinemas.Add(cinema);
            context.SaveChanges();

            return cinema;
        }

        public static void Update(ApplicationDBContext context, Cinema cinema)
        {
            context.Cinemas.Update(cinema);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string cinemaId)
        {
            var cinema = context.Cinemas.FirstOrDefault(c => c.Id == cinemaId);

            if (cinema != null)
            {
                var auditoriums = context.Auditoriums.Where(a => a.CinemaId == cinemaId).ToList();

                foreach (var auditorium in auditoriums)
                {
                    AuditoriumService.Delete(context, auditorium.Id);
                }

                context.Cinemas.Remove(cinema);
                context.SaveChanges();
            }
        }

        public static List<Cinema> SortByNumberOfAvailableFilms(ApplicationDBContext context)
        {
            return [.. context.Cinemas.OrderByDescending(c => c.AvailableFilmIds.Count)];
        }

        public static List<Cinema> FilterByName(ApplicationDBContext context, string name)
        {
            return [.. context.Cinemas.Where(c => c.Name.Contains(name))];
        }

        public static List<Cinema> FilterByFilmAvailability(ApplicationDBContext context, string filmId)
        {
            return [.. context.Cinemas.Where(c => c.AvailableFilmIds.Contains(filmId))];
        }
    }
}