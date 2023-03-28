using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryImplementation;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BusinessLogic.Business.BusinessImplementation
{
    public class LoanBusinessLogic : ILoanBusinessLogic<ApplyLoanDTO>
    {
        IMapper _mapper;
        private Logging _logger;
        IBasicOperations<User> _userBasicOperartion;
        IBasicOperations<Advisor> _advisorBasicOperartion;
        IBasicOperations<Property> _propertyBasicOperartion;
        IBasicOperations<LoanRequirements> _loanRequirementsBasicOperartion;
        IBasicOperations<PersonalIncome> _personalIncomeBasicOperartion;
        IBasicOperations<Application> _applicationBasicOperartion;
        IAdvanceUserOperation<Advisor> _advisorAdvanceUserOperation;
        IAdvanceUserOperation<User> _userAdvanceUserOperation;
        IAdvanceListOperation<Application> _applicationAdvanceListOperation;
        public LoanBusinessLogic(IMapper mapper, Logging logger,
            IBasicOperations<User> userBasicOperartion,
        IBasicOperations<Advisor> advisorBasicOperartion,
        IBasicOperations<Property> propertyBasicOperartion,
        IBasicOperations<LoanRequirements> loanRequirementsBasicOperartion,
        IBasicOperations<PersonalIncome> personalIncomeBasicOperartion,
        IBasicOperations<Application> applicationBasicOperartion,
        IAdvanceUserOperation<Advisor> advisorAdvanceUserOperation,
        IAdvanceUserOperation<User> userAdvanceUserOperation,
        IAdvanceListOperation<Application> applicationAdvanceListOperation)
        {
            _logger = logger;
            _mapper = mapper;
            _userBasicOperartion= userBasicOperartion;
            _userAdvanceUserOperation= userAdvanceUserOperation;
            _advisorBasicOperartion= advisorBasicOperartion;
            _advisorAdvanceUserOperation= advisorAdvanceUserOperation;
            _applicationAdvanceListOperation= applicationAdvanceListOperation;
            _applicationBasicOperartion= applicationBasicOperartion;
            _loanRequirementsBasicOperartion= loanRequirementsBasicOperartion;
            _personalIncomeBasicOperartion  = personalIncomeBasicOperartion;
            _propertyBasicOperartion = propertyBasicOperartion;
        }

        public async Task<bool> ApplyLoanTask(ApplyLoanDTO loanDTO)
        {
           
            
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(loanDTO.EmailId);
            if(userFromDAL==null)
            {
                _logger.WriteInFile("User Trying to apply for loan couldn't be found!");
                _logger.WriteInConsole("User Trying to apply for loan couldn't be found!");
                throw new ArgumentException("Email Id doesn't exist");
            }
            Property property = _mapper.Map<Property>(loanDTO);
            Property propertyFromDAL =  await _propertyBasicOperartion.AddTask(property);
            await _propertyBasicOperartion.SaveChanges();

            LoanRequirements loanRequirements = _mapper.Map<LoanRequirements>(loanDTO);
            LoanRequirements loanRequirementsFromDAL = await _loanRequirementsBasicOperartion.AddTask(loanRequirements);
            await _loanRequirementsBasicOperartion.SaveChanges();


            PersonalIncome personalIncome = _mapper.Map<PersonalIncome>(loanDTO);
            PersonalIncome personalIncomeFromDAL = await _personalIncomeBasicOperartion.AddTask(personalIncome);
            await _personalIncomeBasicOperartion.SaveChanges();


            Application application = new Application();
            application.PersonalIncomeId = personalIncomeFromDAL.Id;
            application.LoanRequirementId = loanRequirementsFromDAL.Id;
            application.PropertyId = propertyFromDAL.Id;
            application.UserId = userFromDAL.Id;
            Application applicationFromDAL = await _applicationBasicOperartion.AddTask(application);           
            await _applicationBasicOperartion.SaveChanges();


            if (applicationFromDAL != null)
            {
                _logger.WriteInFile("Applied for loan Successfully !");
                _logger.WriteInConsole("Applied for loan Successfully !");
                return true;
            }
            else
            {
                _logger.WriteInFile("Apply for loan Failed !");
                _logger.WriteInConsole("Apply for loan Failed !");
                return false;
            }

        }

        public async Task<IEnumerable<ApplyLoanDTO>> FetchAllCloseLoanApplicationTask()
        {
            IEnumerable<Application> listOfAllApplications = await _applicationBasicOperartion.GetAllRecordsTask();
            IList<Property> listOfAllProperties = new List<Property>();
            IList<LoanRequirements> listOfAllLoanRequirements = new List<LoanRequirements>();
            IList<PersonalIncome> listOfAllPersonalIncomes = new List<PersonalIncome>();
            IList<User> listOfAllUsers = new List<User>();
            foreach (Application application in listOfAllApplications)
            {
                if (application.Status == 1)
                {
                    listOfAllProperties.Add(await _propertyBasicOperartion.GetByIdTask(application.PropertyId));
                    listOfAllLoanRequirements.Add(await _loanRequirementsBasicOperartion.GetByIdTask(application.LoanRequirementId));
                    listOfAllPersonalIncomes.Add(await _personalIncomeBasicOperartion.GetByIdTask(application.PersonalIncomeId));
                    listOfAllUsers.Add(await _userBasicOperartion.GetByIdTask(application.UserId));
                }
            }
            IList<ApplyLoanDTO> listOfApplyLoanDTO = new List<ApplyLoanDTO>();
            for (int i = 0; i < listOfAllUsers.Count; i++)
            {
                ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
                {
                    EmailId = listOfAllUsers[i].EmailId,
                    Address = listOfAllProperties[i].Address,
                    Cost = listOfAllProperties[i].Cost,
                    Size = listOfAllProperties[i].Size,
                    RegistrationCost = listOfAllProperties[i].RegistrationCost,
                    MonthlyFamilyIncome = listOfAllPersonalIncomes[i].MonthlyFamilyIncome,
                    OtherIncome = listOfAllPersonalIncomes[i].OtherIncome,
                    LoanDuration = listOfAllLoanRequirements[i].LoanDuration,
                    LoanAmount = listOfAllLoanRequirements[i].LoanAmount,
                    LoanStartDate = listOfAllLoanRequirements[i].LoanStartDate
                };
                listOfApplyLoanDTO.Add(applyLoanDTO);
            }
            return listOfApplyLoanDTO;
        }

        public async Task<IEnumerable<ApplyLoanDTO>> FetchAllLoanApplicationTask()
        {
            IEnumerable<Application> listOfAllApplications = await _applicationBasicOperartion.GetAllRecordsTask();
            IList<Property> listOfAllProperties = new List<Property>();
            IList<LoanRequirements> listOfAllLoanRequirements = new List<LoanRequirements>();
            IList<PersonalIncome> listOfAllPersonalIncomes = new List<PersonalIncome>();
            IList<User> listOfAllUsers = new List<User>();
            foreach (Application application in listOfAllApplications)
            { 
                    listOfAllProperties.Add(await _propertyBasicOperartion.GetByIdTask(application.PropertyId));
                    listOfAllLoanRequirements.Add(await _loanRequirementsBasicOperartion.GetByIdTask(application.LoanRequirementId));
                    listOfAllPersonalIncomes.Add(await _personalIncomeBasicOperartion.GetByIdTask(application.PersonalIncomeId));
                    listOfAllUsers.Add(await _userBasicOperartion.GetByIdTask(application.UserId));
            }
            IList<ApplyLoanDTO> listOfApplyLoanDTO = new List<ApplyLoanDTO>();
            for (int i = 0; i < listOfAllUsers.Count; i++)
            {
                ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
                {
                    EmailId = listOfAllUsers[i].EmailId,
                    Address = listOfAllProperties[i].Address,
                    Cost = listOfAllProperties[i].Cost,
                    Size = listOfAllProperties[i].Size,
                    RegistrationCost = listOfAllProperties[i].RegistrationCost,
                    MonthlyFamilyIncome = listOfAllPersonalIncomes[i].MonthlyFamilyIncome,
                    OtherIncome = listOfAllPersonalIncomes[i].OtherIncome,
                    LoanDuration = listOfAllLoanRequirements[i].LoanDuration,
                    LoanAmount = listOfAllLoanRequirements[i].LoanAmount,
                    LoanStartDate = listOfAllLoanRequirements[i].LoanStartDate
                };
                listOfApplyLoanDTO.Add(applyLoanDTO);
            }
            return listOfApplyLoanDTO;
        }

        public async Task<IEnumerable<ApplyLoanDTO>> FetchAllOpenLoanApplicationTask()
        {
            IEnumerable<Application> listOfAllApplications = await _applicationBasicOperartion.GetAllRecordsTask();
            IList<Property> listOfAllProperties = new List<Property>();
            IList<LoanRequirements> listOfAllLoanRequirements = new List<LoanRequirements>();
            IList<PersonalIncome> listOfAllPersonalIncomes = new List<PersonalIncome>();
            IList<User> listOfAllUsers = new List<User>();
            foreach (Application application in listOfAllApplications)
            {
                if(application.Status == 0)
                {
                    listOfAllProperties.Add(await _propertyBasicOperartion.GetByIdTask(application.PropertyId));
                    listOfAllLoanRequirements.Add(await _loanRequirementsBasicOperartion.GetByIdTask(application.LoanRequirementId));
                    listOfAllPersonalIncomes.Add(await _personalIncomeBasicOperartion.GetByIdTask(application.PersonalIncomeId));
                    listOfAllUsers.Add(await _userBasicOperartion.GetByIdTask(application.UserId));
                }                
            }
    
            IList<ApplyLoanDTO> listOfApplyLoanDTO = new List<ApplyLoanDTO>();
            for (int i = 0; i < listOfAllUsers.Count; i++)
            {
                ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
                {
                    EmailId = listOfAllUsers[i].EmailId,
                    Address = listOfAllProperties[i].Address,
                    Cost = listOfAllProperties[i].Cost,
                    Size = listOfAllProperties[i].Size,
                    RegistrationCost = listOfAllProperties[i].RegistrationCost,
                    MonthlyFamilyIncome = listOfAllPersonalIncomes[i].MonthlyFamilyIncome,
                    OtherIncome = listOfAllPersonalIncomes[i].OtherIncome,
                    LoanDuration = listOfAllLoanRequirements[i].LoanDuration,
                    LoanAmount = listOfAllLoanRequirements[i].LoanAmount,
                    LoanStartDate = listOfAllLoanRequirements[i].LoanStartDate
                };
                listOfApplyLoanDTO.Add(applyLoanDTO);
            }
            return listOfApplyLoanDTO;
        }
    }
}
