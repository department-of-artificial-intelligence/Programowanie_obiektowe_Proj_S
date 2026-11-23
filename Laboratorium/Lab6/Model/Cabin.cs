namespace Model;

public class Cabin {
  public int CabinId { get; set; }

  public int Number { get; set; }
  public int Capacity { get; set; }
  public decimal PricePerNight { get; set; }

  public int CampsiteId { get; set; }
  public Campsite Campsite { get; set; } = null!;
}