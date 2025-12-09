using Project.Model;
using Microsoft.EntityFrameworkCore;
namespace Project.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }
    }
}
