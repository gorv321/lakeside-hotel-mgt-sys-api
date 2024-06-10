using Microsoft.AspNetCore.Mvc;

using LakeSideHotelApi.Repository.LakeSideHotelApi.Repositories;
using LakeSideHotelApi.Models.Entities;
using LakeSideHotelApi.Models.DTO;
using LakeSideHotelApi.Models.DTOs;
using LakeSideHotelApi.Repositories;
using System.Text;
using System;


namespace LakeSideHotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookedRoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookedRoomRepository _bookedRoomRepository;

        public BookedRoomController(IRoomRepository roomRepository, IBookedRoomRepository bookedRoomRepository)
        {
            _bookedRoomRepository = bookedRoomRepository;
            _roomRepository = roomRepository;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookedRoom>>> GetBookedRooms()
        {
            var bookedRooms = await _bookedRoomRepository.GetAllBookedRoomsAsync();
            return Ok(bookedRooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookedRoom>> GetBookedRoom(long id)
        {
            var bookedRoom = await _bookedRoomRepository.GetBookedRoomByIdAsync(id);

            if (bookedRoom == null)
            {
                return NotFound();
            }

            return Ok(bookedRoom);
        }
        private string GenerateConfirmationCode(string guestName, DateTime checkInDate)
        {
            // Take first three letters of guest's name
            string namePrefix = guestName.Substring(0, Math.Min(3, guestName.Length));

            // Format date (assuming format YYYYMMDD)
            string dateSuffix = checkInDate.ToString("yyyyMMdd");

            // Concatenate name prefix and date suffix to generate confirmation code
            return $"{namePrefix}{dateSuffix}";
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddBookedRoom(long roomId, [FromBody] BookedRoomDto bookedRoomDto)
        {
            // Validate input if needed
            // Example validation: check if check-in date is before check-out date
            if (bookedRoomDto.CheckInDate >= bookedRoomDto.CheckOutDate)
            {
                return BadRequest("Check-in date must be before check-out date");
            }

            var room= _roomRepository.GetRoomByIdAsync(roomId);
            int TotalNumOfGuest1 = bookedRoomDto.NumOfAdults + bookedRoomDto.NumOfChildren;
           string confirmationcode= GenerateConfirmationCode(bookedRoomDto.GuestFullName, bookedRoomDto.CheckInDate);

            // Create a new BookedRoom instance
            var bookedRoom =new BookedRoom
            {
                RoomId = roomId,
                CheckInDate = bookedRoomDto.CheckInDate,
                CheckOutDate = bookedRoomDto.CheckOutDate,
                TotalNumOfGuest=TotalNumOfGuest1,
                GuestFullName = bookedRoomDto.GuestFullName,
                GuestEmail = bookedRoomDto.GuestEmail,
                BookingConfirmationCode=confirmationcode,
                NumOfAdults = bookedRoomDto.NumOfAdults,
                NumOfChildren = bookedRoomDto.NumOfChildren
            };

            // Call the repository method to add the booked room
    

            await _bookedRoomRepository.AddBookedRoomAsync(bookedRoom);
            return Ok("Booked room added successfully Your code is "+confirmationcode);        
        }


    [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookedRoom(long id, BookedRoom bookedRoom)
        {
            if (id != bookedRoom.BookingId)
            {
                return BadRequest();
            }

            await _bookedRoomRepository.UpdateBookedRoomAsync(bookedRoom);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookedRoom(long id)
        {
            await _bookedRoomRepository.DeleteBookedRoomAsync(id);
            return NoContent();
        }
    }

}
