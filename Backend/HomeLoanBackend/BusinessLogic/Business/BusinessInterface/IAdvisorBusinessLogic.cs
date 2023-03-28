using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    public interface IAdvisorBusinessLogic<T, P>
    {
        public Task<T> RegisterTask(T entity);
        public Task<bool> LoginTask(T entity);
        public Task<bool> UpdatePasswordTask(P entity);
    }
}
