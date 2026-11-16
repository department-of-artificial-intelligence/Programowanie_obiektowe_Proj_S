using Model;
using Services.DTO.Campsite;

namespace Services.Mapping;

public static class CampsiteMapping { //klasa rozszerzająca

  //Entity -> DTO
  public static CampsiteDto ToDto(this Campsite campsite) => new() {
    CampsiteId=campsite.CampsiteId,
    Name=campsite.Name
  };

  //CreateDto -> Entity
  public static Campsite ToEntity(this CreateCampsiteDto dto) => new() {
    Name=dto.Name
  };

  //UpdateDto -> Entity
  public static void UpdateEntity(this UpdateCampsiteDto dto,Campsite campsite) =>
    campsite.Name=dto.Name;
}