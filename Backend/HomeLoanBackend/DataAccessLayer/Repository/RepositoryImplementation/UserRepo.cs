using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class UserRepo : IBasicOperations<User>, IAdvanceUserOperation<User>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;
        public UserRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<User> AddTask(User user)
        {
            await _appDbContext.Users.AddAsync(user);
            _logger.WriteInFile("User Added in  the Database");
            _logger.WriteInConsole("User Added in  the Database");
            return user;
        }

        public async Task<User> EditTask(User user)
        {
            User userEntity = await _appDbContext.Users.FirstOrDefaultAsync((entity) => entity.Id == user.Id);
            userEntity.EmailId = user.EmailId;
            userEntity.Password = user.Password;
            userEntity.CountryCode = user.CountryCode;
            userEntity.CityCode = user.CityCode;
            userEntity.StateCode = user.StateCode;
            userEntity.CountryCode = user.CountryCode;
            _logger.WriteInFile("User Details Changed in  the Database");
            _logger.WriteInConsole("User details changed in  the Database");
            return userEntity;
        }

        public async Task<IEnumerable<User>> GetAllRecordsTask()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByEmailTask(string email)
        {
            User userEntity = await _appDbContext.Users.FirstOrDefaultAsync((entity) => entity.EmailId == email);
            return userEntity;
        }

        public async Task<User> GetByIdTask(Guid id)
        {
            User userEntity = await _appDbContext.Users.FirstOrDefaultAsync((entity) => entity.Id == id);
            return userEntity;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            User userEntity = await _appDbContext.Users.FirstOrDefaultAsync((entity) => entity.Id == id);
            _appDbContext.Users.Remove(userEntity);
            _logger.WriteInFile("User Removed from  the Database");
            _logger.WriteInConsole("User Removed from  the Database");
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _appDbContext.SaveChangesAsync() >= 0);
        }

        public async Task<string> UpdatePasswordTask(Guid id, string newPassword)
        {
            User userEntity = await _appDbContext.Users.FirstOrDefaultAsync((entity) => entity.Id == id);
             userEntity.Password = newPassword;
            _logger.WriteInFile("User Password Updated in  the Database");
            _logger.WriteInConsole("User Password Updated in  the Database");
            return userEntity.Password;
        }
    }
}
