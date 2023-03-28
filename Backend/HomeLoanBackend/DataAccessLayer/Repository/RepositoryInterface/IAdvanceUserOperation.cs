using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IAdvanceUserOperation<T>
    {
        public Task<T> GetByEmailTask(string email);
        public Task<string> UpdatePasswordTask(Guid id, string newPassword);
    }
}
