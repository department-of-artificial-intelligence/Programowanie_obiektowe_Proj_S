using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Project.DAL

{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<Event> Events { get; set; }
    }
}
