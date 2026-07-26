using UserDirectory.Application.Abstractions;
using UserDirectory.Application.DTOs;
using UserDirectory.Domain.Entities;

namespace UserDirectory.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    #region CONSTRUCTOR
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Get all user list'
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();

        return users.Select(ToDto);
    }
    /// <summary>
    /// Get user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<UserDto?> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);

        return user is null ? null : ToDto(user);
    }
    /// <summary>
    /// Add user details into the table
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    public async Task<UserDto> AddUser(UserDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Age = userDto.Age,
            City = userDto.City,
            State = userDto.State,
            Pincode = userDto.Pincode
        };

        var createdUser = await _userRepository.AddUser(user);

        return ToDto(createdUser);
    }
    /// <summary>
    /// Update user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userDto"></param>
    /// <returns></returns>
    public async Task<bool> UpdateUser(int id, UserDto userDto)
    {
        var user = await _userRepository.GetUserById(id);

        if (user is null)
            return false;

        user.Name = userDto.Name;
        user.Age = userDto.Age;
        user.City = userDto.City;
        user.State = userDto.State;
        user.Pincode = userDto.Pincode;

        await _userRepository.UpdateUser(user);

        return true;
    }
    /// <summary>
    /// Delete user from table by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id);

        if (user is null)
            return false;

        await _userRepository.DeleteUser(user);

        return true;
    }

    #endregion

    #region PRIVATE METHODS
    private static UserDto ToDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Age = user.Age,
        City = user.City,
        State = user.State,
        Pincode = user.Pincode
    };
    #endregion
}
