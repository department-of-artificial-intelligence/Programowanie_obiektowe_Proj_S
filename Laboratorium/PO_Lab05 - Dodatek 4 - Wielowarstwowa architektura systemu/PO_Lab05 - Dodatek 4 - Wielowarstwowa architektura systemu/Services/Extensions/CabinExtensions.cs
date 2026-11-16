using Model;

namespace Services.Extensions;

public static class CabinExtensions { //klasa rozszerzająca
  public static Cabin? GetById(this IEnumerable<Cabin> cabins,int id) =>
    cabins.FirstOrDefault(x => x.CabinId==id);
}