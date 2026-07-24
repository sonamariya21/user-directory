using backend.Data;
using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _appDbContext;

        public UserRepo(AppDbContext appDbContext)
        {           
            _appDbContext= appDbContext;
        }

        #region PUBLIC METHODS

        #region GET ALL USERS
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _appDbContext.Users
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        #endregion GET ALL USERS

        #region GET USERS BY ID
        public async Task<User?> GetUserById(int id)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion GET USERS BY ID

        #region ADD USER
        public async Task<User> AddUser(User user)
        {
            _appDbContext.Users.Add(user);

            await _appDbContext.SaveChangesAsync();

            return user;
        }
        #endregion ADD USER

        #region UPDATE USER
        public async Task UpdateUser(User user)
        {
            _appDbContext.Users.Update(user);

            await _appDbContext.SaveChangesAsync();
        }
        #endregion UPDATE USER

        #region DELETE USER
        public async Task DeleteUser(User user)
        {
            _appDbContext.Users.Remove(user);

            await _appDbContext.SaveChangesAsync();
        }
        #endregion DELETE USER

        #endregion PUBLIC METHODS
    }
}
