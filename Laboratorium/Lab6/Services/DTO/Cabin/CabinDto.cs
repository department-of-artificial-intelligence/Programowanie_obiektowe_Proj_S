namespace Services.DTO.Cabin;

public class CabinDto {
  public int CabinId { get; set; }

  public int Number { get; set; }
  public int Capacity { get; set; }
  public decimal PricePerNight { get; set; }

  public int CampsiteId { get; set; }
  public string CampsiteName { get; set; } = string.Empty;
}