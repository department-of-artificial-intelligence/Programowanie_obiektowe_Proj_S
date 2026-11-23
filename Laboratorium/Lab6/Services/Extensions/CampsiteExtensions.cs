using Model;

namespace Services.Extensions;

public static class CampsiteExtensions { //klasa rozszerzająca
  public static Campsite? GetById(this IEnumerable<Campsite> campsites,int id) =>
    campsites.FirstOrDefault(x => x.CampsiteId==id);
}