using LakeSideHotelApi.Models.DTO;
using LakeSideHotelApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LakeSideHotelApi.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid id);

        User AddUser(RegisterUserDto registerUserDto);

        User UpdateUserInfo(Guid id, RegisterUserDto registerUserDto);

        void DeleteUser(Guid id);
        User FindByEmailAndPassword(string email, string password);
    }
}
