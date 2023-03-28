using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryImplementation;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BusinessLogic.Business.BusinessImplementation
{
    public class AdvisorBusinessLogic : IAdvisorBusinessLogic<LoginAdvisorDTO, AdvisorUpdatePasswordDTO>
    {
        private IMapper _mapper;
        private Logging _logger;
        IBasicOperations<Advisor> _advisorBasicOperartion;
        IAdvanceUserOperation<Advisor> _advisorAdvanceUserOperation;
        public AdvisorBusinessLogic(IMapper mapper, Logging logger, IBasicOperations<Advisor> advisorBasicOperartion, IAdvanceUserOperation<Advisor> advisorAdvanceUserOperation)
        {
            _logger = logger;
            _mapper = mapper;
            _advisorBasicOperartion= advisorBasicOperartion;
            _advisorAdvanceUserOperation= advisorAdvanceUserOperation;
        }
        public async Task<bool> LoginTask(LoginAdvisorDTO entity)
        {
           
            Advisor advisorFromDAL = await _advisorAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if (advisorFromDAL == null)
            {
                _logger.WriteInFile("advisor email Id doesn't found");
                _logger.WriteInConsole("advisor email Id doesn't found");
                throw new ArgumentException("Advisor Not Found");
            }
            if (entity.Password == advisorFromDAL.Password)
            {
                return true;
            }
            else
            {
                return false;
               
            }
        }

        public async Task<bool> UpdatePasswordTask(AdvisorUpdatePasswordDTO entity)
        {
            
            Advisor advisorFromDAL = await _advisorAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if (advisorFromDAL == null)
            {
                _logger.WriteInFile("Email Id doesn't found");
                _logger.WriteInConsole("Email Id doesn't found");
                throw new ArgumentException("Email Id doesn't exist");
            }
            if (entity.Password == advisorFromDAL.Password)
            {
                await _advisorAdvanceUserOperation.UpdatePasswordTask(advisorFromDAL.Id, entity.NewPassword);
                await _advisorBasicOperartion.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<LoginAdvisorDTO> RegisterTask(LoginAdvisorDTO entity)
        {
           
            Advisor advisorFromDAL = await _advisorAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if (advisorFromDAL == null)
            {
                Advisor advisor = _mapper.Map<Advisor>(entity);
                Advisor advisorReceivedFromDAL = await _advisorBasicOperartion.AddTask(advisor);
                await _advisorBasicOperartion.SaveChanges();
                LoginAdvisorDTO returnedAdvisor = _mapper.Map<LoginAdvisorDTO>(advisorReceivedFromDAL);
                return returnedAdvisor;
            }
            else
            {
                _logger.WriteInFile("Advisor Registration Failed as User already Exists!");
                _logger.WriteInConsole("Advisor Registration Failed as User already Exists! ");
                throw new ArgumentException("Email Already Exists !");
            }

        }
    }
}
