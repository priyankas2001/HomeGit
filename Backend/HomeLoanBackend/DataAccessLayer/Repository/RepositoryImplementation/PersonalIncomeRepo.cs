using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    
    public class PersonalIncomeRepo : IBasicOperations<PersonalIncome>
    {
        private AppDbContext _appDbContext;
        private readonly Logging _logger;
        public PersonalIncomeRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<PersonalIncome> AddTask(PersonalIncome personalIncome)
        {
            await _appDbContext.PersonalIncomes.AddAsync(personalIncome);
            _logger.WriteInFile("Personal Income Details of the user added in the Database");
            _logger.WriteInConsole("Personal Income Details of the user added in the Database");
            return personalIncome;
        }

        public async Task<PersonalIncome> EditTask(PersonalIncome personalIncome)
        {
            PersonalIncome IncomeEntity = await _appDbContext.PersonalIncomes.FirstOrDefaultAsync(item => item.Id == personalIncome.Id);
            IncomeEntity.MonthlyFamilyIncome = personalIncome.MonthlyFamilyIncome;
            IncomeEntity.OtherIncome = personalIncome.OtherIncome;
            _logger.WriteInFile("Personal Income Details of the user are changed in the Database");
            _logger.WriteInConsole("Personal Income Details of the user are changed in the Database");
            return IncomeEntity;
        }

        public async Task<IEnumerable<PersonalIncome>> GetAllRecordsTask()
        {
            return await _appDbContext.PersonalIncomes.ToListAsync();
        }

        public async Task<PersonalIncome> GetByIdTask(Guid Id)
        {
            PersonalIncome IncomeEntity = await _appDbContext.PersonalIncomes.FirstOrDefaultAsync(item => item.Id == Id);
            return IncomeEntity;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            PersonalIncome IncomeEntity = await _appDbContext.PersonalIncomes.FirstOrDefaultAsync(item => item.Id == id);
            _appDbContext.PersonalIncomes.Remove(IncomeEntity);
            _logger.WriteInFile("Personal Income Details of the user are removed from the Database");
            _logger.WriteInConsole("Personal Income Details of the user are removed from the Database");
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return await _appDbContext.SaveChangesAsync()>=0;
            
        }
    }
}
