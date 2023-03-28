using AutoMapper;
using BusinessLogic;
using BusinessLogic.Business.BusinessImplementation;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        public UserController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }      
        [HttpPost]
        public async Task<IActionResult> UserRegisterTask([FromBody] RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "User Details Are Incorrect");
            }
            try
            {
                RegisterUserDTO userCreated = await _businessLogic.GetUserBusinessLogic().RegisterTask(userDTO);
                return StatusCode(201,userCreated);
            }
            catch (Exception exception)
            {
                return StatusCode(500, " User Already Exists! ");
                //exception log
            }

        }
        [HttpPatch]
        public async Task<IActionResult> UserChangePasswordTask([FromBody] UserUpdatePasswordDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Incorrect Details");
            }
            try
            {
                bool result= await _businessLogic.GetUserBusinessLogic().UpdatePasswordTask(entity);
                if(result==true)
                {
                    return StatusCode(202, "User Password Changed Successfully");
                }
                else
                {
                    return StatusCode(406, "User Password Not Changed");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(406, "User Not Found");
            }
            
          
        }
        [HttpGet]
        public async Task<IActionResult> UserLoginTask([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Incorrect Details");
            }
            try
            {
                bool result = await _businessLogic.GetUserBusinessLogic().LoginTask(userDTO);
                if (result == true)
                {
                    return StatusCode(202, "Login Sucessful");
                }
                else
                {
                    return StatusCode(403, "Login Failed");
                }
            }
           
            catch (Exception exception)
            {
                return StatusCode(406,"Login Failed !");
                //ToDo: Add Logger

            }


        }
    }
}
