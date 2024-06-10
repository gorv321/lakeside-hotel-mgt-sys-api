using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LakeSideHotelApi.Models.Entities;
using LakeSideHotelApi.Repositories;

namespace LakeSideHotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(long id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> AddRoom([FromForm] RoomDto roomDto)
        {
            if (roomDto.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await roomDto.ImageFile.CopyToAsync(memoryStream);
                    var room = new Room
                    {
                        Name = roomDto.Name,
                        Description = roomDto.Description,
                        Capacity = roomDto.Capacity,
                        Price = roomDto.Price,
                        Image = memoryStream.ToArray(),
                        ImageName = roomDto.ImageFile.FileName
                    };

                    await _roomRepository.AddRoomAsync(room);
                    return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);
                }
            }
            else
            {
                return BadRequest("Image file is required.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(long id, [FromForm] RoomDto roomDto)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            if (roomDto.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await roomDto.ImageFile.CopyToAsync(memoryStream);
                    existingRoom.Image = memoryStream.ToArray();
                    existingRoom.ImageName = roomDto.ImageFile.FileName;
                }
            }

            existingRoom.Name = roomDto.Name;
            existingRoom.Description = roomDto.Description;
            existingRoom.Capacity = roomDto.Capacity;
            existingRoom.Price = roomDto.Price;

            await _roomRepository.UpdateRoomAsync(existingRoom);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(long id)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            await _roomRepository.DeleteRoomAsync(id);

            return NoContent();
        }
        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoomNames()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            var roomNames = rooms.Select(room => room.Name).ToList();
            return Ok(roomNames);
        }
    }

    public class RoomDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
