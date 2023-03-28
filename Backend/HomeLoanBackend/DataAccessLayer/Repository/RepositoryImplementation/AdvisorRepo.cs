using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class AdvisorRepo : IBasicOperations<Advisor>, IAdvanceUserOperation<Advisor>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;
        public AdvisorRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<Advisor> AddTask(Advisor advisor)
        {
            await _appDbContext.Advisors.AddAsync(advisor);
            _logger.WriteInFile("Advisor Added In the Database");
            _logger.WriteInConsole("Advisor Added In the Database");
            return advisor;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            var advisorEntity = await _appDbContext.Advisors.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Remove(advisorEntity);
            _logger.WriteInFile("Advisor Removed In the Database");
            _logger.WriteInConsole("Advisor Removed In the Database");

            return true;
        }

        public async Task<IEnumerable<Advisor>> GetAllRecordsTask()
        {
            _logger.WriteInFile("Advisor Details returned from the Database");
            _logger.WriteInConsole("Advisor Details returned from the Database");
            return await _appDbContext.Advisors.ToListAsync();
        }

        public async Task<Advisor> EditTask(Advisor advisor)
        {
            Advisor advisorEntity = await _appDbContext.Advisors.FirstOrDefaultAsync((x) => (x.Id == advisor.Id));
            advisorEntity.Password = advisor.Password;
            advisorEntity.EmailId = advisor.EmailId;
            _logger.WriteInFile("Email and Password For the Advisor are changed in the Database");
            _logger.WriteInConsole("Email and Password For the Advisor are changed in the Database");
            return advisor;
        }

        public async Task<bool> SaveChanges()
        {
            return await _appDbContext.SaveChangesAsync() >= 0;
        }

        public async Task<Advisor> GetByIdTask(Guid id)
        {
            Advisor advisorEntity = await _appDbContext.Advisors.FirstOrDefaultAsync(x => x.Id == id);
            return advisorEntity;
        }

        public async Task<Advisor> GetByEmailTask(string email)
        {
            Advisor advisorEntity = await _appDbContext.Advisors.FirstOrDefaultAsync(x => x.EmailId == email);
            return advisorEntity;
        }

        public async Task<string> UpdatePasswordTask(Guid id, string newPassword)
        {
            Advisor advisorEntity = await _appDbContext.Advisors.FirstOrDefaultAsync(x => x.Id == id);
            advisorEntity.Password = newPassword;
            _logger.WriteInFile("Password For the Advisor is changed in the Database");
            _logger.WriteInConsole("Password For the Advisor is changed in the Database");
            return newPassword;
        }
    }
}
