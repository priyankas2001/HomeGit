using BusinessLogic.DTO;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        public LoanController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }
        [HttpPost]
        public async Task<IActionResult> ApplyForLoanTask([FromBody] ApplyLoanDTO loanDTO)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, "Loan Details are Incorrect");
                
            }
            try
            {
                bool result = await _businessLogic.GetLoanBusinessLogic().ApplyLoanTask(loanDTO);
                if (result)
                {
                    return StatusCode(201, "Loan application successful");
                }
                else
                {
                    return StatusCode(500, "Loan application failed, try again later");
                }
            }
           
            catch (Exception exception)
            {
                return StatusCode(406, "Loan Application already Exists !");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllOpenLoanApplicationTask()
        {
           
            try
            {
                return StatusCode(200, await _businessLogic.GetLoanBusinessLogic().FetchAllOpenLoanApplicationTask());
            }
            
            catch(Exception exception)
            {
                return StatusCode(500,"Error ");
            }
        }
    }

}
