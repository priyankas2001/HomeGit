using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class CollateralRepo : IBasicOperations<Collateral>,IAdvanceListOperation<Collateral>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;
        public CollateralRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<Collateral> AddTask(Collateral entity)
        {
            await _appDbContext.Collaterals.AddAsync(entity);
            return entity;
        }

        public async Task<Collateral> EditTask(Collateral entity)
        {
            Collateral collateral = await _appDbContext.Collaterals.FirstOrDefaultAsync((x) => (x.Id == entity.Id));
            collateral.Share = entity.Share;
            collateral.ApplicationId = entity.ApplicationId;
            collateral.Value = entity.Value;
            return collateral;
        }

        public async Task<IEnumerable<Collateral>> GetAllRecordsByParameterTask(Guid id, string parameterName = "")
        {
            IEnumerable<Collateral> listOfCollateralByParameter = null;
            if (parameterName == "UserId")
            {
                listOfCollateralByParameter = await _appDbContext.Collaterals.Where((x) => x.UserId == id).ToListAsync();

            }
            else if (parameterName == "ApplicationId")
            {
                listOfCollateralByParameter = await _appDbContext.Collaterals.Where((x) => x.ApplicationId == id).ToListAsync();
            }
            await Task.WhenAll();
            _logger.WriteInFile("collateral Details returned from the Database");
            _logger.WriteInConsole(" collateral Details returned from the Database");
            return listOfCollateralByParameter;
        }

        public async Task<IEnumerable<Collateral>> GetAllRecordsTask()
        {
            return await _appDbContext.Collaterals.ToListAsync();
        }

        public async Task<Collateral> GetByIdTask(Guid id)
        {
            return await _appDbContext.Collaterals.FirstOrDefaultAsync((x) => x.Id == id);
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            Collateral collateral = await _appDbContext.Collaterals.FirstOrDefaultAsync((x) => x.Id == id);
            _appDbContext.Collaterals.Remove(collateral);
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return await _appDbContext.SaveChangesAsync() >= 0;
        }
    }
}
