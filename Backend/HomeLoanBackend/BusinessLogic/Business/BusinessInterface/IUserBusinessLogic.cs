using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    public interface IUserBusinessLogic<A,B,C>
    {
        public Task<A> RegisterTask(A entity);
        public Task<bool> LoginTask(B entity);
        public Task<bool> UpdatePasswordTask(C entity);
    }
}
