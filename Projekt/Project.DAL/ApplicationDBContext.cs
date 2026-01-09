using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;


namespace Project.DAL
{
    public class ApplicationDbcontext : DbContext
    {
        public DbSet<Pracownik> Pracownicy { get; set; }

        public DbSet<Dzial> Dzialy { get; set; }
        public DbSet<Projekt> Projekty { get; set; }

        public DbSet<Adres> Adresy { get; set; }
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {

        }
    }
}