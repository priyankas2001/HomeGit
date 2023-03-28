using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class PromotionRepo:IBasicOperations<Promotions>, IAdvanceStatusOperation,IAdvanceParameterOperation<Promotions,int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;
        public PromotionRepo(AppDbContext appDbContext, Logging logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<Promotions> AddTask(Promotions entity)
        {
            await _appDbContext.Promotions.AddAsync(entity);
            return entity;
        }

        public async Task<Promotions> EditTask(Promotions entity)
        {
            Promotions promotions = await _appDbContext.Promotions.FirstOrDefaultAsync((x) => (x.Id == entity.Id));
            promotions.StartDate = entity.StartDate;
            promotions.EndDate = entity.EndDate;
            promotions.Message = entity.Message;
            promotions.Type = entity.Type;
            return promotions;
        }

        public async Task<IEnumerable<Promotions>> GetAllRecordsTask()
        {
            return await _appDbContext.Promotions.ToListAsync();
        }

        public async Task<Promotions> GetByIdTask(Guid id)
        {
            return await _appDbContext.Promotions.FirstOrDefaultAsync((x) => x.Id == id);
        }

        public async Task<Promotions> GetByParameterBasedOperation(int parameterValue, string parameterName = "")
        {
            Promotions promotion=null;
            if(parameterName=="Status")
            {
                 promotion = await _appDbContext.Promotions.FirstOrDefaultAsync(x => x.Status == parameterValue);
                
            }
            return promotion;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            Promotions promotions = await _appDbContext.Promotions.FirstOrDefaultAsync((x) => x.Id == id);
            _appDbContext.Promotions.Remove(promotions);
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return await _appDbContext.SaveChangesAsync() >= 0;
        }

        public async Task<int> SwitchStatus(Guid id)
        {
            Promotions promotions = await _appDbContext.Promotions.FirstOrDefaultAsync((x) => x.Id == id);
            promotions.Status = (promotions.Status == 0 ? 1 : 0);
            return promotions.Status;
        }
    }
}
