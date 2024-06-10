namespace LakeSideHotelApi.Models.DTO
{
    public class UpdateRoomDto
    {
        public string RoomType { get; set; }
        public double RoomPrice { get; set; }
        public string? Photo { get; set; }
        public bool IsBooked { get; set; }
    }
}
