using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IAdvanceParameterOperation<T,P>
    {
        public  Task<T> GetByParameterBasedOperation(P parameterValue,string parameterName="");
        
       
    }
}
