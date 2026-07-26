using Microsoft.EntityFrameworkCore;
using UserDirectory.Application.Abstractions;
using UserDirectory.Domain.Entities;
using UserDirectory.Infrastructure.Persistence;

namespace UserDirectory.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    #region CONSTRUCTOR
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Get all user list
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users
            .OrderBy(x => x.Id)
            .ToListAsync();
    }
    /// <summary>
    /// Get user details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetUserById(int id)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    /// <summary>
    /// Add user details into the table
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<User> AddUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    /// <summary>
    /// Update user details by id
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
    /// <summary>
    /// Delete user details from table by id
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task DeleteUser(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
    #endregion
}
