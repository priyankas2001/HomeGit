using DataAccessLayer.Data;
using DataAccessLayer.Repository.RepositoryImplementation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using BusinessLogic.Business.BusinessImplementation;
using BusinessLogic.Mapper;
using BusinessLogic.DTO;
using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using Utility;
using DataAccessLayer.Repository.RepositoryInterface;

namespace BusinessLogic
{
    public class BusinessLogic:IBusinessLogic
    {
        private AppDbContext _appDbContext;

        IBasicOperations<User> _userBasicOperartion;
        IBasicOperations<Advisor> _advisorBasicOperartion;
        IBasicOperations<Property> _propertyBasicOperartion;
        IBasicOperations<LoanRequirements> _loanRequirementsBasicOperartion;
        IBasicOperations<PersonalIncome> _personalIncomeBasicOperartion;
        IBasicOperations<Application> _applicationBasicOperartion;
        IBasicOperations<Collateral> _collateralBasicOperation;
        IBasicOperations<Promotions> _promotionBasicOperation;  
        IAdvanceUserOperation<Advisor> _advisorAdvanceUserOperation;
        IAdvanceUserOperation<User> _userAdvanceUserOperation;
        IAdvanceListOperation<Application> _applicationAdvanceListOperation;
        IAdvanceListOperation<Collateral> _collateralAdvanceListOperation;
        IAdvanceStatusOperation _applicationAdvanceStatusOperation;
        IAdvanceStatusOperation _promotionAdvanceStatusOperation;
        IAdvanceParameterOperation<Promotions, int> _promotionParameterOperation;
        private Logging _logger;
        private IMapper _mapper;

        public IUserBusinessLogic<RegisterUserDTO, LoginUserDTO, UserUpdatePasswordDTO> _userBusinessLogic;
        public ILoanBusinessLogic<ApplyLoanDTO> _loanBusinessLogic;
        public IAdvisorBusinessLogic<LoginAdvisorDTO, AdvisorUpdatePasswordDTO> _advisorBusinessLogic;
        public ICollateralBusinessLogic<ApplyCollateralDTO, EditCollateralDTO> _collateralBusinessLogic;

        public IPromotionBusinessLogic<PromotionsDTO> _promotionBusinessLogic;
        public BusinessLogic(IMapper mapper )
        {
            _appDbContext= new AppDbContext();
            _logger = new Logging();
            _mapper = mapper;

            _userBasicOperartion = new UserRepo(_appDbContext, _logger);
            _applicationBasicOperartion = new ApplicationRepo(_appDbContext, _logger);
            _personalIncomeBasicOperartion = new PersonalIncomeRepo(_appDbContext, _logger);
            _propertyBasicOperartion = new PropertyImplementation(_appDbContext, _logger);
            _loanRequirementsBasicOperartion = new LoanRequirementRepo(_appDbContext, _logger);
            _advisorBasicOperartion = new AdvisorRepo(_appDbContext, _logger);
            _collateralBasicOperation = new CollateralRepo(_appDbContext, _logger);
            _collateralAdvanceListOperation = new CollateralRepo(_appDbContext, _logger);
            _userAdvanceUserOperation = new UserRepo(_appDbContext, _logger);
            _advisorAdvanceUserOperation = new AdvisorRepo(_appDbContext, _logger);
            _applicationAdvanceListOperation = new ApplicationRepo(_appDbContext, _logger);
            _promotionBasicOperation = new PromotionRepo(_appDbContext, _logger);
            _promotionAdvanceStatusOperation = new PromotionRepo(_appDbContext, _logger);
            _promotionParameterOperation = new PromotionRepo(_appDbContext, _logger);

            _userBusinessLogic = new UserBusinessLogic(_mapper, _logger, _userBasicOperartion, _userAdvanceUserOperation);
            _loanBusinessLogic = new LoanBusinessLogic(_mapper, _logger, _userBasicOperartion, _advisorBasicOperartion, 
            _propertyBasicOperartion, _loanRequirementsBasicOperartion, _personalIncomeBasicOperartion, 
            _applicationBasicOperartion, _advisorAdvanceUserOperation, _userAdvanceUserOperation, 
            _applicationAdvanceListOperation);
            _advisorBusinessLogic = new AdvisorBusinessLogic(_mapper, _logger, _advisorBasicOperartion, _advisorAdvanceUserOperation);
            _collateralBusinessLogic = new CollateralBusinessLogic(_mapper, _logger, _collateralBasicOperation, _userAdvanceUserOperation, _collateralAdvanceListOperation);
            _promotionBusinessLogic = new PromotionBusinessLogic(_mapper, _logger, _promotionBasicOperation, _promotionAdvanceStatusOperation,_promotionParameterOperation);
        }

        public IUserBusinessLogic<RegisterUserDTO,LoginUserDTO, UserUpdatePasswordDTO> GetUserBusinessLogic()
        {
            return _userBusinessLogic;
            
        }
        public ILoanBusinessLogic<ApplyLoanDTO> GetLoanBusinessLogic()
        {
            return _loanBusinessLogic;
        }
        public IAdvisorBusinessLogic<LoginAdvisorDTO, AdvisorUpdatePasswordDTO> GetAdvisorBusinessLogic()
        {
            return _advisorBusinessLogic;
        }
        public ICollateralBusinessLogic<ApplyCollateralDTO, EditCollateralDTO> GetCollateralBusinessLogic()
        {
            return _collateralBusinessLogic;
        }
        public IPromotionBusinessLogic<PromotionsDTO> GetPromotionsBusinessLogic()
        {
            return _promotionBusinessLogic;
        }

    }
}
