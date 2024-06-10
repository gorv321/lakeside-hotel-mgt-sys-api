using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LakeSideHotelApi.Models.Entities
{
    public class BookedRoom
    {
        [Key]
        public long BookingId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public string GuestFullName { get; set; }

        public string GuestEmail { get; set; }

        public int NumOfAdults { get; set; }

        public int NumOfChildren { get; set; }

        public int TotalNumOfGuest { get; set; }

        public string BookingConfirmationCode { get; set; }

        [ForeignKey("Room")]
        public long RoomId { get; set; } // Foreign key

        public virtual Room Room { get; set; } // Navigation property
    }
}
