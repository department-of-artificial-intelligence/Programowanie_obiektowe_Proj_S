namespace Services.DTO.Cabin;

public class UpdateCabinDto {
  public int Number { get; set; }
  public int Capacity { get; set; }
  public decimal PricePerNight { get; set; }

  public int CampsiteId { get; set; }
}