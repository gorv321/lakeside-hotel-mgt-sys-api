using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LakeSideHotelApi.Models.Entities
{
    public class Room
    {
        [Key]
        public long RoomId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Image as a byte array
        public byte[] Image { get; set; }

        // Optional: if you want to store the image name or path
        [StringLength(255)]
        public string ImageName { get; set; }

        // Navigation property to represent the one-to-many relationship
        public virtual ICollection<BookedRoom> BookedRooms { get; set; }
    }
}
