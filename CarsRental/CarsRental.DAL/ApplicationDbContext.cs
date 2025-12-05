using Microsoft.EntityFrameworkCore;
using CarsRental.Model;
using System.Collections.Generic;

namespace CarsRental.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
    }
}
