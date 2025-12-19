using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services
{
    public class ResidentService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        private readonly PersonService _personService;

        public ResidentService(ApplicationDbContext applicationDbContext, PersonService personService)
        {
            this._applicationDbContext = applicationDbContext;
            this._personService = personService;
        }

        public async Task<List<Resident>> GetAllResidents(ulong hotelId)
        {
            return await this._applicationDbContext.Hotels
                .Include(x => x.Rooms)
                .Where(x => x.Id == hotelId)
                .SelectMany(x => x.Rooms)
                .Include(x => x.Residents)
                .SelectMany(x => x.Residents)
                .Include(x => x.Person)
                .ToListAsync();
        }
        
        public async Task<Resident> CreateResidentAsync(string firstName, string lastName, DateTime dateOfBirth, DateTime residentFrom)
        {
            var person = await this._personService.CreatePersonAsync(firstName, lastName, dateOfBirth);
            var resident = new Resident(person, residentFrom);
            
            await this._applicationDbContext.Residents.AddAsync(resident);
            await this._applicationDbContext.SaveChangesAsync();

            return resident;
        }
        
        public async Task EvictResidentAsync(Resident resident)
        {
            this._applicationDbContext.RoomHistoricResidents.Add(new RoomHistoricResident()
            {
                Person = resident.Person,
                ResidentFrom = resident.ResidentFrom,
                ResidentTo = DateTime.Now
            });
            
            this._applicationDbContext.Residents.Remove(resident);
            
            await this._applicationDbContext.SaveChangesAsync();
        }
    }
}