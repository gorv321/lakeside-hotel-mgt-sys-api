using LakeSideHotelApi.Data;
using LakeSideHotelApi.Models;
using LakeSideHotelApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LakeSideHotelApi.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly LakeSideHotelDbContext _context;

        public RoomRepository(LakeSideHotelDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(long roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }

        public async Task AddRoomAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(long roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
