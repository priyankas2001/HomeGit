using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class ApplicationRepo : IBasicOperations<Application>, IAdvanceListOperation<Application>, IAdvanceStatusOperation
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;

        public ApplicationRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<Application> AddTask(Application application)
        {
            await _appDbContext.Applications.AddAsync(application);
            _logger.WriteInFile("Loan Application Added in the Database");
            _logger.WriteInConsole("Loan Application Added in the Database");
            return application;
        }

        public async Task<Application> EditTask(Application application)
        {
            Application applicationEntitiy = await _appDbContext.Applications.FirstOrDefaultAsync((entity) => entity.Id == application.Id);
            applicationEntitiy.UserId = application.UserId;
            applicationEntitiy.LoanRequirementId = application.LoanRequirementId;
            applicationEntitiy.PersonalIncomeId = application.PersonalIncomeId;
            applicationEntitiy.PropertyId = application.PropertyId;
            _logger.WriteInFile("Changes made in Loan Application in the Database");
            _logger.WriteInConsole("Changes made in Loan Application in the Database");
            return applicationEntitiy;
        }

        public async Task<IEnumerable<Application>> GetAllRecordsTask()
        {
            return await _appDbContext.Applications.ToListAsync();
        }

        public async Task<Application> GetByIdTask(Guid id)
        {
            Application applicationEntity = await _appDbContext.Applications.FirstOrDefaultAsync((entity) => entity.Id == id);
            return applicationEntity;
        }

        public async Task<IEnumerable<Application>> GetAllRecordsByParameterTask(Guid id, string parameterName)
        {
            IEnumerable<Application> listOfApplicationEntity = null;
            if(parameterName == "UserId")
            {
                listOfApplicationEntity = await _appDbContext.Applications.Where((x) => x.UserId == id).ToListAsync();

            }
            else if (parameterName == "PersonalIncomeId")
            {
                listOfApplicationEntity = await _appDbContext.Applications.Where((x) => x.PersonalIncomeId == id).ToListAsync();
            }
            else if (parameterName == "LoanRequirementsId")
            {
                listOfApplicationEntity = await _appDbContext.Applications.Where((x) => x.LoanRequirementId == id).ToListAsync();
            }
            else if (parameterName == "PropertyId")
            {
                listOfApplicationEntity = await _appDbContext.Applications.Where((x) => x.PropertyId == id).ToListAsync();
            }
            await Task.WhenAll();
            _logger.WriteInFile("Loan Application Details returned from the Database");
            _logger.WriteInConsole(" Loan Application Details returned from the Database");
            return listOfApplicationEntity;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            Application applicationEntity = await _appDbContext.Applications.FirstOrDefaultAsync((entity) => entity.Id == id);
            _appDbContext.Applications.Remove(applicationEntity);
            _logger.WriteInFile("Loan Application removed from the Database");
            _logger.WriteInConsole(" Loan Application removed from the Database");
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _appDbContext.SaveChangesAsync() >= 0);
        }

        public async Task<int> SwitchStatus(Guid id)
        {
            Application application = await _appDbContext.Applications.FirstOrDefaultAsync((x) => x.Id == id);
            application.Status = (application.Status == 0 ? 1 : 0);
            return application.Status;
        }
    }
}
