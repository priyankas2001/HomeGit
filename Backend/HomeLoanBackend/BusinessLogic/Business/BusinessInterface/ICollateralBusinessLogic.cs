using BusinessLogic.DTO;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    public interface ICollateralBusinessLogic<T, P>
    {
        public Task<Guid> AddCollateralTask(T entity);
        public Task<bool> DeleteCollateralTask(Guid id);
        public Task<IEnumerable<T>> GetAllCollateralByUserEmailTask(string emailId);
        public Task<Guid> EditCollateralTask(P entity);
    }
}
