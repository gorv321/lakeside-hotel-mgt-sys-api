using LakeSideHotelApi.Models.DTO;
using LakeSideHotelApi.Models.Entities;
using LakeSideHotelApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LakeSideHotelApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(this._userRepository.GetAllUsers());
        }

        [HttpGet("{id:guid}")]
        public ActionResult<User> GetUserById(Guid id)
        {
            User user = _userRepository.GetUserById(id);

            if(user == null)
            {
                return NotFound("User of given id not found");
            }

            return Ok(user);
        }

       

        [HttpPut("{id:guid}")]
        public ActionResult<User> UpdateUserInfo(Guid id, [FromBody] RegisterUserDto registerUserDto)
        {
            User user = this._userRepository.GetUserById(id);
           
            if (user == null)
            {
                return NotFound("User of given id not found");
            }

            return Ok(_userRepository.UpdateUserInfo(id, registerUserDto));
        }

        [Authorize(Roles =Roles.Admin)]
        [HttpDelete("{id:guid}")]
        public ActionResult DeleteUser(Guid id)
        {
            User user = this._userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound("User of given id not found");
            }

            this._userRepository.DeleteUser(id);

            return Ok("User Deleted Successfully");
        }

    }
}
