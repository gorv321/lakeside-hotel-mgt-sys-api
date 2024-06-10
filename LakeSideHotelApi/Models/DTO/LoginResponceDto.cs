using LakeSideHotelApi.Models.Entities;

namespace LakeSideHotelApi.Models.DTO
{
    public class LoginResponseDto
    {
        public string Email {  get; set; }
        public string Token { get; set; }

        public List<string> Role { get; set; }
    }
}
