using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Model;

namespace Project.Services;

public abstract class BaseService<T, K, TDto> : IBaseService<T, K, TDto> where T : BaseEntity<K>
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly DbSet<T> _dbSet;

    protected BaseService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<TDto?> GetByIdAsync(K id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity == null ? default : _mapper.Map<TDto>(entity);
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public virtual async Task<TDto> CreateAsync(TDto dto)
    {
        var entity = _mapper.Map<T>(dto);
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto?> UpdateAsync(K id, TDto dto)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return default;

        _mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<bool> DeleteAsync(K id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> ExistsAsync(K id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity != null;
    }
}

