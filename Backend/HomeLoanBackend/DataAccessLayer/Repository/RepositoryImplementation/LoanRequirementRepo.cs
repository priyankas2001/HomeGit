using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class LoanRequirementRepo:IBasicOperations<LoanRequirements>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;

        public LoanRequirementRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<LoanRequirements> AddTask(LoanRequirements loanRequirements)
        {
            await _appDbContext.LoanRequirements.AddAsync(loanRequirements);
            _logger.WriteInFile("Loan Requirement  Details added to the Database");
            _logger.WriteInConsole(" Loan Requirement  Details added to the Database");
            return loanRequirements;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            var loanRequirements = await _appDbContext.LoanRequirements.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Remove(loanRequirements);
            _logger.WriteInFile("Loan Requirement  Details removed from the Database");
            _logger.WriteInConsole(" Loan Requirement  Details removed from the Database");
            return true;
        }

        public async Task<IEnumerable<LoanRequirements>> GetAllRecordsTask()
        {
            return await _appDbContext.LoanRequirements.ToListAsync();
        }

        public async Task<LoanRequirements> EditTask(LoanRequirements loanRequirements)
        {
            LoanRequirements loanRequirementsEntity = await _appDbContext.LoanRequirements.FirstOrDefaultAsync((x) => (x.Id == loanRequirements.Id));
            loanRequirementsEntity.LoanDuration = loanRequirements.LoanDuration;
            loanRequirementsEntity.LoanStartDate = loanRequirements.LoanStartDate;
            loanRequirementsEntity.LoanAmount = loanRequirements.LoanAmount;

            _logger.WriteInFile("Changes made in Loan Requirement Details in the Database");
            _logger.WriteInConsole(" Changes made in Loan Requirement Details in the Database");
            return loanRequirementsEntity;  
        }

        public async Task<bool> SaveChanges()
        {
            return await _appDbContext.SaveChangesAsync() >= 0;
        }

        public async Task<LoanRequirements> GetByIdTask(Guid id)
        {
            LoanRequirements loanRequirementsEntity = await _appDbContext.LoanRequirements.FirstOrDefaultAsync(x => x.Id == id);
            return loanRequirementsEntity;
        }
    }
}
