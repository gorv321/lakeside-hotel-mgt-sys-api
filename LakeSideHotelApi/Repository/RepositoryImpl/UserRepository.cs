using LakeSideHotelApi.Data;
using LakeSideHotelApi.Models.DTO;
using LakeSideHotelApi.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LakeSideHotelApi.Repository.RepositoryImpl
{
    public class UserRepository : IUserRepository
    {

        private LakeSideHotelDbContext _dbContext;

        public UserRepository(LakeSideHotelDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return users;
        }
        
        public User GetUserById(Guid id)
        {
            var user = _dbContext.Users.Find(id);
            return user;
        }

        public User AddUser(RegisterUserDto registerUserDto)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
                Address = registerUserDto.Address,
                Password = registerUserDto.Password,
                Phone = registerUserDto.Phone,
                Role="User"
               // Role= registerUserDto.Role,
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }

        public User UpdateUserInfo(Guid id, RegisterUserDto registerUserDto)
        {
            var updatedUser = _dbContext.Users.Find(id);

            if(updatedUser == null)
            {
                return updatedUser;
            }

            updatedUser.Name = registerUserDto.Name;
            updatedUser.Email = registerUserDto.Email;
            updatedUser.Address = registerUserDto.Address;
            updatedUser.Password = registerUserDto.Password;
            updatedUser.Phone = registerUserDto.Phone;
            // updatedUser.Role = registerUserDto.Role;
            updatedUser.Role = "User";

            _dbContext.SaveChanges();

            return updatedUser;
        }

        public void DeleteUser(Guid id)
        {
            var userToDelete = _dbContext.Users.Find(id);

            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user;
        }

    }
}
