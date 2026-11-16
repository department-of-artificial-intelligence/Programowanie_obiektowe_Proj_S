using Services.DTO.Campsite;

namespace Services.Interfaces;

public interface ICampsiteService {
  Task<List<CampsiteDto>> GetAllAsync();
  Task<CampsiteDto?> GetByIdAsync(int id);
  Task<int> CreateAsync(CreateCampsiteDto dto);
  Task<bool> UpdateAsync(int id,UpdateCampsiteDto dto);
  Task<bool> DeleteAsync(int id);
}