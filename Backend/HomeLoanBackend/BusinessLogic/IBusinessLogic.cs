using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IBusinessLogic
    {
        public IUserBusinessLogic<RegisterUserDTO, LoginUserDTO, UserUpdatePasswordDTO> GetUserBusinessLogic();
        public ILoanBusinessLogic<ApplyLoanDTO> GetLoanBusinessLogic();
        public IAdvisorBusinessLogic<LoginAdvisorDTO, AdvisorUpdatePasswordDTO> GetAdvisorBusinessLogic();
        public ICollateralBusinessLogic<ApplyCollateralDTO, EditCollateralDTO> GetCollateralBusinessLogic();
        public IPromotionBusinessLogic<PromotionsDTO> GetPromotionsBusinessLogic();
    }
}
