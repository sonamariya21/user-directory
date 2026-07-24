using backend.DTO;

namespace backend.ServiceLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserDto?> GetUserById(int id);

        Task<UserDto> AddUser(UserDto dto);

        Task<bool> UpdateUser(int id, UserDto dto);

        Task<bool> DeleteUser(int id);
    }
}
