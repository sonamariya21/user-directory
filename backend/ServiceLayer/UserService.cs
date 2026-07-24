using backend.DTO;
using backend.Models;
using backend.Repository.Interfaces;
using backend.ServiceLayer.Interfaces;

namespace backend.ServiceLayer
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        #region PUBLIC METHODS
        /// <summary>
        /// Get all user details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                City = user.City,
                State = user.State,
                Pincode = user.Pincode
            });
        }

        /// <summary>
        /// Get user details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserById(int id)
        {
            var user = await _userRepo.GetUserById(id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                City = user.City,
                State = user.State,
                Pincode = user.Pincode
            };
        }

        /// <summary>
        /// add user details into the table
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns></returns>
        public async Task<UserDto> AddUser(UserDto userdto)
        {
            var user = new User
            {
                Name = userdto.Name,
                Age = userdto.Age,
                City = userdto.City,
                State = userdto.State,
                Pincode = userdto.Pincode
            };

            var createdUser = await _userRepo.AddUser(user);

            return new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Age = createdUser.Age,
                City = createdUser.City,
                State = createdUser.State,
                Pincode = createdUser.Pincode
            };
        }

        /// <summary>
        /// update user deatils
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userdto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(int id, UserDto userdto)
        {
            var user = await _userRepo.GetUserById(id);

            if (user == null)
                return false;

            user.Name = userdto.Name;
            user.Age = userdto.Age;
            user.City = userdto.City;
            user.State = userdto.State;
            user.Pincode = userdto.Pincode;

            await _userRepo.UpdateUser(user);

            return true;
        }

        /// <summary>
        /// delete user from the table by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepo.GetUserById(id);

            if (user == null)
                return false;

            await _userRepo.DeleteUser(user);

            return true;
        }
        #endregion PUBLIC METHODS
    }
}
