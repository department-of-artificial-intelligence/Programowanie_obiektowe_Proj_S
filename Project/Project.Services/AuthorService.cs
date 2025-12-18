using AutoMapper;
using Project.DTO;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Model;

namespace Project.Services;

public class AuthorService : BaseService<Author, int, AuthorDto>, IAuthorService
{
    public AuthorService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsByNameAsync(string firstName, string lastName)
    {
        var authors = await _dbSet
            .Where(a => a.FirstName == firstName && a.LastName == lastName)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public async Task<IEnumerable<AuthorDto>> SearchByLastNameAsync(string lastName)
    {
        var authors = await _dbSet
            .Where(a => a.LastName.Contains(lastName))
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }
}

