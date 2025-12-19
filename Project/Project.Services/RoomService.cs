using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services
{
    public class RoomService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        private readonly ResidentService _residentService;

        public RoomService(ApplicationDbContext applicationDbContext, ResidentService residentService)
        {
            this._applicationDbContext = applicationDbContext;
            this._residentService = residentService;
        }
        
        public async Task<List<HotelRoom>> GetAllRoomsAsync(ulong hotelId)
        {
            return await this._applicationDbContext.Hotels
                .Include(x => x.Rooms)
                .Where(x => x.Id == hotelId)
                .SelectMany(x => x.Rooms)
                .Include(x => x.Residents)
                .ToListAsync();
        }
        
        public async Task<HotelRoom> CreateRoomAsync(ulong hotelId, int roomNumber, int floor, decimal pricePerDay)
        {
            var hotel = await this._applicationDbContext.Hotels
                .Include(x => x.Rooms)
                .FirstAsync(x => x.Id == hotelId);
            
            var room = new HotelRoom()
            {
                Number = roomNumber,
                Floor = floor,
                PricePerDay = pricePerDay,
                Residents = [],
                HistoricResidents = []
            };
            
            hotel.Rooms.Add(room);
            
            this._applicationDbContext.Add(room);
            this._applicationDbContext.Update(hotel);
            
            await this._applicationDbContext.SaveChangesAsync();

            return room;
        }
        
        public async Task AddResidentToRoomAsync(ulong roomId, ulong residentId)
        {
            var room = await this._applicationDbContext.HotelRooms.FindAsync(roomId);
            var resident = await this._applicationDbContext.Residents.FindAsync(residentId);
            
            if (room == null || resident == null)
            {
                throw new ArgumentException("Room or Resident not found");
            }

            room.Residents.Add(resident);
            
            this._applicationDbContext.Update(room);
            await this._applicationDbContext.SaveChangesAsync();
        }
    }
}