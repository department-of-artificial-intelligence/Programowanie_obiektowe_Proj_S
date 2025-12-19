using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;
using Project.Model.Utils;

namespace Project.Services
{
    public class HotelService
    {
        private readonly HotelGenerator _hotelGenerator = new HotelGenerator(0xBEEF /* cats love beef */);
        
        private readonly ApplicationDbContext _applicationDbContext;

        public HotelService(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        
        public async Task<bool> IsAnyHotelExistsAsync()
        {
            return await this._applicationDbContext.Hotels.AnyAsync();
        }

        public async Task SeedSampleHotelAsync()
        {
            await this._applicationDbContext.Hotels.AddAsync(_hotelGenerator.GenerateHotel());
            await this._applicationDbContext.SaveChangesAsync();
        }
        
        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await this._applicationDbContext.Hotels
                .ToListAsync();
        }

        public async Task<Hotel> CreateHotelAsync(string name, string address, Manager manager)
        {
            var hotel = new Hotel
            {
                Name = name,
                Address = address,
                Manager = manager,
                Rooms = []
            };
            
            await this._applicationDbContext.Hotels.AddAsync(hotel);
            await this._applicationDbContext.SaveChangesAsync();

            return hotel;
        }
        
        public async Task<List<Hotel>> GetHotelsByManagerAsync(Manager manager)
        {
            return await this._applicationDbContext.Hotels
                .Where(h => h.Manager.Id == manager.Id)
                .ToListAsync();
        }
    }
}