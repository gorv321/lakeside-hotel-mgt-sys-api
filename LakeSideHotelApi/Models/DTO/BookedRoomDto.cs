using System;

namespace LakeSideHotelApi.Models.DTOs
{
    public class BookedRoomDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public int NumOfAdults { get; set; }
        public int NumOfChildren { get; set; }
    }
}
