using LakeSideHotelApi.Models.Entities;

namespace LakeSideHotelApi.Repository
{
  

    namespace LakeSideHotelApi.Repositories
    {
        public interface IBookedRoomRepository
        {
            Task<IEnumerable<BookedRoom>> GetAllBookedRoomsAsync();
            Task<BookedRoom> GetBookedRoomByIdAsync(long id);
            Task AddBookedRoomAsync(BookedRoom bookedRoom);
            Task UpdateBookedRoomAsync(BookedRoom bookedRoom);
            Task DeleteBookedRoomAsync(long id);
        }
    }

}
