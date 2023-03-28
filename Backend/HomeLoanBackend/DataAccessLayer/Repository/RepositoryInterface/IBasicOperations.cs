using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IBasicOperations<T>
    {
        public Task<T> AddTask(T entity);
        public Task<bool> RemoveTask(Guid id);
        public Task<IEnumerable<T>> GetAllRecordsTask();
        public Task<T> EditTask(T entity);
        public Task<bool> SaveChanges();
        public Task<T> GetByIdTask(Guid id);
    }
}
