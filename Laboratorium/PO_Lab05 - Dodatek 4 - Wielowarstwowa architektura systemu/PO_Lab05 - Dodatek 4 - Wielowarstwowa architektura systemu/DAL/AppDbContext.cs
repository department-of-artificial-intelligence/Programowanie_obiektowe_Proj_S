using Model;

namespace DAL;

public class AppDbContext {

  public List<Campsite> Campsites { get; set; }
  public List<Cabin> Cabins { get; set; }

  public AppDbContext() {
    Campsites=DataSeeder.GetCampsites();
    Cabins=DataSeeder.GetCabins(Campsites);
  }
}