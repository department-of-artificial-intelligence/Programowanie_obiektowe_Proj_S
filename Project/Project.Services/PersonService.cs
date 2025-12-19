using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PersonService(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        
        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await this._applicationDbContext.People
                .ToListAsync();
        }
        
        public async Task<Person> CreatePersonAsync(string firstName, string lastName, DateTime dateOfBirth)
        {
            var person = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
            
            await this._applicationDbContext.People.AddAsync(person);
            await this._applicationDbContext.SaveChangesAsync();

            return person;
        }
    }
}