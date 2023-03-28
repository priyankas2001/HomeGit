using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    public interface ILoanBusinessLogic<T>
    {
        public Task<bool> ApplyLoanTask(T entity);
        public Task<IEnumerable<T>> FetchAllLoanApplicationTask();
        public Task<IEnumerable<T>> FetchAllOpenLoanApplicationTask();
        public Task<IEnumerable<T>> FetchAllCloseLoanApplicationTask();
    }
}
