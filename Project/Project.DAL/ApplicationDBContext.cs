using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Project.DAL

{
    public class ApplicationDBContext: DbContext
    {
        private readonly string _connectionString = "Data source=Database.db";
        
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
        public ApplicationDBContext(){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
