using Model;
using Services.DTO.Cabin;

namespace Services.Mapping;

public static class CabinMapping { //klasa rozszerzająca

  //Entity -> DTO
  public static CabinDto ToDto(this Cabin cabin) => new() {
    CabinId=cabin.CabinId,
    Number=cabin.Number,
    Capacity=cabin.Capacity,
    PricePerNight=cabin.PricePerNight,
    CampsiteId=cabin.CampsiteId,
    CampsiteName=cabin.Campsite?.Name??string.Empty
  };

  //CreateCabinDto -> Entity
  public static Cabin ToEntity(this CreateCabinDto dto,Campsite campsite) => new() {
    Number=dto.Number,
    Capacity=dto.Capacity,
    PricePerNight=dto.PricePerNight,
    CampsiteId=campsite.CampsiteId,
    Campsite=campsite
  };

  //UpdateDto -> Entity
  public static void UpdateEntity(this UpdateCabinDto dto,Cabin cabin,Campsite campsite) {
    cabin.Number=dto.Number;
    cabin.Capacity=dto.Capacity;
    cabin.PricePerNight=dto.PricePerNight;
    cabin.CampsiteId=campsite.CampsiteId;
    cabin.Campsite=campsite;
  }
}