using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL;

public class AppDbContext:DbContext {

  public DbSet<Campsite> Campsites { get; set; }
  public DbSet<Cabin> Cabins { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    optionsBuilder.UseSqlServer(
      @"Server=(localdb)\MSSQLLocalDB;
      Database=CampsiteSystemDb+'DW';
      Trusted_Connection=True;
      TrustedServerCerftificate=True");
  }

  //public AppDbContext() {
  //  Campsites=DataSeeder.GetCampsites();
  //  Cabins=DataSeeder.GetCabins(Campsites);
  //}

}