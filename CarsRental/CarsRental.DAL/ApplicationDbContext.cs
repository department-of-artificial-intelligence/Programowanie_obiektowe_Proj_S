using Microsoft.EntityFrameworkCore;
using CarsRental.Model;
using System.Collections.Generic;

namespace CarsRental.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
    }
}
