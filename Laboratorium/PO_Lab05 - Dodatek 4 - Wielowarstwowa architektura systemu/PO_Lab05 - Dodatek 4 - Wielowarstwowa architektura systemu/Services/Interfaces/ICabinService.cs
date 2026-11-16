using Services.DTO.Cabin;

namespace Services.Interfaces;

public interface ICabinService {
  Task<List<CabinDto>> GetAllAsync();
  Task<CabinDto?> GetByIdAsync(int id);
  Task<int> CreateAsync(CreateCabinDto dto);
  Task<bool> UpdateAsync(int id,UpdateCabinDto dto);
  Task<bool> DeleteAsync(int id);
}