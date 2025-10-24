using Microsoft.EntityFrameworkCore;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieMark> MovieMarks { get; set; }

        public DbSet<Review> Reviews { get; set; }
  
        public string DbPath { get; }

        public ApplicationDbContext ()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "MoviesDb.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

    }
}
