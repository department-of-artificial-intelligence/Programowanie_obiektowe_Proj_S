using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}