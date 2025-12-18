using Project.DTO;
using Project.Model;

namespace Project.Services;

public interface IAuthorService : IBaseService<Author, int, AuthorDto>
{
    Task<IEnumerable<AuthorDto>> GetAuthorsByNameAsync(string firstName, string lastName);
    Task<IEnumerable<AuthorDto>> SearchByLastNameAsync(string lastName);
}

