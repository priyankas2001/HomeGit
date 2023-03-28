
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer.Repository.RepositoryImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using BusinessLogic.Mapper;
using DataAccessLayer.Model;
using Microsoft.Extensions.Logging;
using DataAccessLayer.Data;
using AutoMapper;
using Utility;
using DataAccessLayer.Repository.RepositoryInterface;

namespace BusinessLogic.Business.BusinessImplementation
{
    public class UserBusinessLogic : IUserBusinessLogic<RegisterUserDTO, LoginUserDTO, UserUpdatePasswordDTO>
    {
        IMapper _mapper;
        private Logging _logger;
        IBasicOperations<User> _userBasicOperartion;
        IAdvanceUserOperation<User> _userAdvanceUserOperation;
        public UserBusinessLogic(IMapper mapper, Logging logger, IBasicOperations<User> userBasicOperartion, IAdvanceUserOperation<User> userAdvanceUserOperation)
        {
            _logger= logger;
            _mapper= mapper;
            _userBasicOperartion= userBasicOperartion;
            _userAdvanceUserOperation= userAdvanceUserOperation;
        }
        
        public async Task<RegisterUserDTO> RegisterTask(RegisterUserDTO entity)
        {
           
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if(userFromDAL == null)
            {
                User user = _mapper.Map<User>(entity);
                User UserReceivedFromDAL = await _userBasicOperartion.AddTask(user);
                await _userBasicOperartion.SaveChanges();
                RegisterUserDTO returnedUser = _mapper.Map<RegisterUserDTO>(UserReceivedFromDAL);
                return returnedUser;
            }
            else
            {
                _logger.WriteInFile("User Registration Failed as User already Exists!");
                _logger.WriteInConsole("User Registration Failed as User already Exists! ");
                throw new ArgumentException("Email Already Exists !");
            }
            
        }

        public async Task<bool> LoginTask(LoginUserDTO entity)
        {
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if (userFromDAL == null)
            {
                //ToDo: Add Logger
                return false;
            }
            if (entity.Password == userFromDAL.Password)
            {
                //ToDo: Add Logger
                return true;
            }
            else
            {
                _logger.WriteInFile("User Login Failed: Password Do not Match");
                _logger.WriteInConsole("User Login Failed: Password Do not Match");
                return false;
            }
        }

        public async Task<bool> UpdatePasswordTask(UserUpdatePasswordDTO entity)
        {
            
            
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if (userFromDAL == null)
            {
                _logger.WriteInFile("User Not Found");
                _logger.WriteInConsole("User Not Found");
                throw new ArgumentException("User Do not Exist");
            }
            if (entity.Password == userFromDAL.Password)
            {
                await _userAdvanceUserOperation.UpdatePasswordTask(userFromDAL.Id, entity.NewPassword);
                await _userBasicOperartion.SaveChanges();
                _logger.WriteInFile(" User Password Updated Successfully");
                _logger.WriteInConsole("User Password Updated Successfully");
                return true;
            }
            else
            {
                _logger.WriteInFile("Old Password Not Matched");
                _logger.WriteInConsole("Old Password Not Matched");
                return false;
            }
        }


    }
}
