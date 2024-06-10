using LakeSideHotelApi.Data;
using LakeSideHotelApi.Models.Entities;
using LakeSideHotelApi.Repository.LakeSideHotelApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LakeSideHotelApi.Repository.RepositoryImpl
{

        public class BookedRoomRepository : IBookedRoomRepository
        {
            private readonly LakeSideHotelDbContext _dbContext;

            public BookedRoomRepository(LakeSideHotelDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<BookedRoom>> GetAllBookedRoomsAsync()
            {
                return await _dbContext.BookedRooms.ToListAsync();
            }

            public async Task<BookedRoom> GetBookedRoomByIdAsync(long id)
            {
                return await _dbContext.BookedRooms.FindAsync(id);
            }

            public async Task AddBookedRoomAsync(BookedRoom bookedRoom)
            {

                await _dbContext.BookedRooms.AddAsync(bookedRoom);
                await _dbContext.SaveChangesAsync();
            }

            public async Task UpdateBookedRoomAsync(BookedRoom bookedRoom)
            {
                _dbContext.BookedRooms.Update(bookedRoom);
                await _dbContext.SaveChangesAsync();
            }

            public async Task DeleteBookedRoomAsync(long id)
            {
                var bookedRoom = await _dbContext.BookedRooms.FindAsync(id);
                if (bookedRoom != null)
                {
                    _dbContext.BookedRooms.Remove(bookedRoom);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }


