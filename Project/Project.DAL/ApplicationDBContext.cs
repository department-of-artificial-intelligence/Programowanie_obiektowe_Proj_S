using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Collections.Generic;

namespace Project.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
