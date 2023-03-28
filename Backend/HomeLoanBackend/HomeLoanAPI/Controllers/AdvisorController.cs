using BusinessLogic;
using BusinessLogic.DTO;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        //private readonly AppDbContext _appDbContext;
        private readonly IBusinessLogic _businessLogic;

        public AdvisorController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }


        [HttpPost]
        
        public async Task<IActionResult> AdvisorRegisterTask([FromBody] LoginAdvisorDTO advisorDTO)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Advisor Details are Incorrect");
            }
            try
            {
                LoginAdvisorDTO advisorCreated = await _businessLogic.GetAdvisorBusinessLogic().RegisterTask(advisorDTO);
                return StatusCode(201, advisorCreated);
            }
           
            catch (Exception exception)
            {
                return StatusCode(409, "Advisor Registration Failed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AdvisorLoginTask([FromBody] LoginAdvisorDTO advisor)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Advisor Details are Incorrect");
            }
            try
            {
                bool result = await _businessLogic.GetAdvisorBusinessLogic().LoginTask(advisor);
                if (result == true)
                {
                    return StatusCode(202, "Login Sucessful");
                }
                else
                {
                    return StatusCode(406, "Advisor Details Incorrect");
                }
            }

            catch (Exception exception)
            {
                return StatusCode(406, "Advisor Details Incorrect");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> AdvisorChangePasswordTask([FromBody] AdvisorUpdatePasswordDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Advisor Details are Incorrect");
            }
            try
            {
                bool result = await _businessLogic.GetAdvisorBusinessLogic().UpdatePasswordTask(entity);
                if(result==true)
                {
                    return StatusCode(202, "Password Changed Sucessfully");
                }
                else
                {
                    return StatusCode(406, "Password Not Changed");
                }
                
            }
            
            catch (Exception exception)
            {
                return StatusCode(406, "Password not Changed");
            }
        }

        
    }
}
