using DAL;
using Services.DTO.Cabin;
using Services.Interfaces;
using Services.Mapping;
using Services.Extensions;

namespace Services.Services;

public class CabinService:ICabinService {
  private readonly AppDbContext _dbContext;

  public CabinService(AppDbContext dbContext) => _dbContext=dbContext;

  public Task<List<CabinDto>> GetAllAsync() =>
    Task.FromResult(_dbContext.Cabins.Select(x => x.ToDto()).ToList());

  public Task<CabinDto?> GetByIdAsync(int id) =>
    Task.FromResult(_dbContext.Cabins.GetById(id)?.ToDto());

  public Task<int> CreateAsync(CreateCabinDto dto) {
    var campsite = _dbContext.Campsites.GetById(dto.CampsiteId);
    if(campsite==null) return Task.FromResult(0);

    var cabin = dto.ToEntity(campsite);
    cabin.CabinId=_dbContext.Cabins.Count()==0 ? 1 : _dbContext.Cabins.Max(x => x.CabinId)+1;

    _dbContext.Cabins.Add(cabin);
    campsite.Cabins.Add(cabin);

    return Task.FromResult(cabin.CabinId);
  }

  public Task<bool> UpdateAsync(int id,UpdateCabinDto dto) {
    var cabin = _dbContext.Cabins.GetById(id);
    if(cabin==null) return Task.FromResult(false);

    var campsite = _dbContext.Campsites.GetById(dto.CampsiteId);
    if(campsite==null) return Task.FromResult(false);

    cabin.Campsite.Cabins.Remove(cabin);
    dto.UpdateEntity(cabin,campsite);
    campsite.Cabins.Add(cabin);

    return Task.FromResult(true);
  }

  public Task<bool> DeleteAsync(int id) {
    var cabin = _dbContext.Cabins.GetById(id);
    if(cabin==null) return Task.FromResult(false);

    cabin.Campsite.Cabins.Remove(cabin);
    _dbContext.Cabins.Remove(cabin);

    return Task.FromResult(true);
  }
}