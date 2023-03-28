using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IAdvanceStatusOperation
    {
        public Task<int> SwitchStatus(Guid id);
    }
}
