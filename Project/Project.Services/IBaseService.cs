using Project.Model;

namespace Project.Services;

public interface IBaseService<T, K, TDto> where T : BaseEntity<K>
{
    Task<TDto?> GetByIdAsync(K id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> CreateAsync(TDto dto);
    Task<TDto?> UpdateAsync(K id, TDto dto);
    Task<bool> DeleteAsync(K id);
    Task<bool> ExistsAsync(K id);
}