using DAL;
using Services.DTO.Campsite;
using Services.Interfaces;
using Services.Mapping;
using Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Services.Services;

public class CampsiteService:ICampsiteService {
  private readonly AppDbContext _dbContext;

  public CampsiteService(AppDbContext dbContext) => _dbContext=dbContext;

  //public Task<List<CampsiteDto>> GetAllAsync() =>
  //  Task.FromResult(_dbContext.Campsites.Select(x => x.ToDto()).ToList());

  public async Task<List<CampsiteDto>> GetAllAsync() {
    var campsites = await _dbContext.Campsites.ToListAsync();
    return campsites.Select(x => x.ToDto()).ToList();
  }

  public Task<CampsiteDto?> GetByIdAsync(int id) =>
    Task.FromResult(_dbContext.Campsites.GetById(id)?.ToDto());

  public Task<int> CreateAsync(CreateCampsiteDto dto) {
    var campsite = dto.ToEntity();
    campsite.CampsiteId=_dbContext.Campsites.Count()==0 ? 1 : _dbContext.Campsites.Max(x => x.CampsiteId)+1;

    _dbContext.Campsites.Add(campsite);
    return Task.FromResult(campsite.CampsiteId);
  }

  public Task<bool> UpdateAsync(int id,UpdateCampsiteDto dto) {
    //Wyszukiwanie kempingu za pomocą własnego rozszerzenia LINQ.
    var campsite = _dbContext.Campsites.GetById(id);
    if(campsite==null) return Task.FromResult(false);

    dto.UpdateEntity(campsite);
    return Task.FromResult(true);
  }

  public Task<bool> DeleteAsync(int id) {
    var campsite = _dbContext.Campsites.GetById(id);
    if(campsite==null) return Task.FromResult(false);

    //Nie usuwamy kempingu, jeśli ma przypisane domki.
    if(_dbContext.Cabins.Any(x => x.CampsiteId==id))
      return Task.FromResult(false);

    _dbContext.Campsites.Remove(campsite);
    return Task.FromResult(true);
  }
}