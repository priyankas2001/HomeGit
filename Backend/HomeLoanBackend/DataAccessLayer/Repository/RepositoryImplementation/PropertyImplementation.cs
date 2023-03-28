using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class PropertyImplementation : IBasicOperations<Property>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Logging _logger;
        public PropertyImplementation(AppDbContext context, Logging logger)
        {
            _appDbContext = context;
            _logger = logger;
        }
        public async Task<bool> SaveChanges()
        {
            return (await _appDbContext.SaveChangesAsync() >= 0);
        }

        public async Task<Property> AddTask(Property property)
        {
            await _appDbContext.Properties.AddAsync(property);
            _logger.WriteInFile("Property Details Added in the Database");
            _logger.WriteInConsole("Property Details Added in the Database");
            return property;
        }

        public async Task<bool> RemoveTask(Guid id)
        {
            Property entity = await _appDbContext.Properties.FirstOrDefaultAsync(item => item.Id == id);
            _appDbContext.Properties.Remove(entity);

            _logger.WriteInFile("Property Details removed from the Database");
            _logger.WriteInConsole("Property Details removed from the Database");
            return true;
        }

        public async Task<IEnumerable<Property>> GetAllRecordsTask()
        {
            return await _appDbContext.Properties.ToListAsync();
        }

        public async Task<Property> EditTask(Property property)
        {
            Property entity = await _appDbContext.Properties.FirstOrDefaultAsync(item => item.Id == property.Id);
            entity.Address = property.Address;
            entity.Size = property.Size;
            entity.Cost = property.Cost;
            entity.RegistrationCost = property.RegistrationCost;
            _logger.WriteInFile("Property Details changed in the Database");
            _logger.WriteInConsole("Property Details changed in the Database");
            return property;
        }

        public async Task<Property> GetByIdTask(Guid id)
        {
            Property entity = await _appDbContext.Properties.FirstOrDefaultAsync(entity => entity.Id == id);
            return entity;
        }
    }
}
