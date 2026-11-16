using Model;

namespace DAL;

public static class DataSeeder {

  public static List<Campsite> GetCampsites() => [
    new() { CampsiteId=1, Name="Kemping Mazury" },
    new() { CampsiteId=2, Name="Kemping Tatry" },
    new() { CampsiteId=3, Name="Kemping Bałtyk" }
  ];

  public static List<Cabin> GetCabins(List<Campsite> campsites) {
    var cabins = new List<Cabin> {
      new() { CabinId=1, Number=1, Capacity=4, PricePerNight=250, CampsiteId=1, Campsite=campsites[0] },
      new() { CabinId=2, Number=2, Capacity=6, PricePerNight=350, CampsiteId=1, Campsite=campsites[0] },
      new() { CabinId=3, Number=1, Capacity=4, PricePerNight=300, CampsiteId=2, Campsite=campsites[1] },
      new() { CabinId=4, Number=2, Capacity=8, PricePerNight=450, CampsiteId=2, Campsite=campsites[1] },
      new() { CabinId=5, Number=1, Capacity=5, PricePerNight=280, CampsiteId=3, Campsite=campsites[2] }
    };

    foreach(var cabin in cabins)cabin.Campsite.Cabins.Add(cabin);

    return cabins;
  }
}