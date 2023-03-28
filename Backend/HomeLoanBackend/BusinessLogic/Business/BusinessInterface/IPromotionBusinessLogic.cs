using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    public interface IPromotionBusinessLogic<T>
    {
       
        public Task<T> AddNewPromotionTask(T newPromotion);
    }
}
