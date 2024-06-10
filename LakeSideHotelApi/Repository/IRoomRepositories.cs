using System.Collections.Generic;
using System.Threading.Tasks;
using LakeSideHotelApi.Models.Entities;

namespace LakeSideHotelApi.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(long roomId);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(long roomId);
    }
}
