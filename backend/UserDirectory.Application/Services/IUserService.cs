using UserDirectory.Application.DTOs;

namespace UserDirectory.Application.Users;

public interface IUserService
{
    /// <summary>
    /// Get all user list
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<UserDto>> GetAllUsers();
    /// <summary>
    /// Get user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserDto?> GetUserById(int id);
    /// <summary>
    /// Add user details into the table
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<UserDto> AddUser(UserDto dto);
    /// <summary>
    /// Update user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<bool> UpdateUser(int id, UserDto dto);
    /// <summary>
    /// Delete user from the table by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteUser(int id);
}
