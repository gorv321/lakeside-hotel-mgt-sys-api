using System.ComponentModel.DataAnnotations;

namespace LakeSideHotelApi.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name can't exceed 50 characters")]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Address can't exceed 500 characters")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone number should be 10 digit and it's 1 digit must be 9")]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
