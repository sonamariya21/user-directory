using UserDirectory.Domain.Entities;

namespace UserDirectory.Application.Abstractions;

public interface IUserRepository
{
    /// <summary>
    /// Get all user list
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<User>> GetAllUsers();
    /// <summary>
    /// Get user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User?> GetUserById(int id);
    /// <summary>
    /// Add user details into table
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<User> AddUser(User user);
    /// <summary>
    /// Update user details by id
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task UpdateUser(User user);
    /// <summary>
    /// Delete user from the table by id
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task DeleteUser(User user);
}
