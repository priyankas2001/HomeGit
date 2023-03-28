using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IAdvanceListOperation<T>
    {
        public Task<IEnumerable<T>> GetAllRecordsByParameterTask(Guid id, string parameterName = "");
    }
}
