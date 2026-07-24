using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task<User> AddUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(User user);
    }
}
