namespace Model;

public class Campsite {
  public int CampsiteId { get; set; }

  public string Name { get; set; } = string.Empty;

  public ICollection<Cabin> Cabins { get; set; } = [];
}