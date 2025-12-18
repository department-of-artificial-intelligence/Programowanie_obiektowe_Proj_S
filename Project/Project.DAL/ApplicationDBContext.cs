using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Project.DAL

{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
