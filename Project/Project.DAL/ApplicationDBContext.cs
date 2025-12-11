using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.DAL
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaNetwork> CinemaNetworks { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
