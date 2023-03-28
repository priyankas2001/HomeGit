using BusinessLogic;
using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollateralController : ControllerBase
    {
        private readonly IBusinessLogic _businessLogic;

        public CollateralController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollateralTask(ApplyCollateralDTO entity)
        {
            if (!ModelState.IsValid)
            {

            }
            try
            {
               Guid id =  await _businessLogic.GetCollateralBusinessLogic().AddCollateralTask(entity);
                return StatusCode(201, $"Your collateral are submited against this {id}");
            }
            catch(Exception ex)
            {
                //log ex
                return StatusCode(500, "Internal Server error");
            }
        }
        [HttpPatch]
        public async Task<IActionResult> EditCollateralTask(EditCollateralDTO entity)
        {
            if (!ModelState.IsValid)
            {

            }
            try
            {
                Guid id = await _businessLogic.GetCollateralBusinessLogic().EditCollateralTask(entity);
                return StatusCode(201, $"Your collateral are submited against this {id}");
            }
            catch (Exception ex)
            {
                //log ex
                return StatusCode(500, "Internal Server error");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCollateralTask([Required]Guid Id)
        {
            if (!ModelState.IsValid)
            {

            }
            try
            {
                await _businessLogic.GetCollateralBusinessLogic().DeleteCollateralTask(Id);
                return StatusCode(201, $"Your collateral are submited against this {Id}");
            }
            catch (Exception ex)
            {
                //log ex
                return StatusCode(500, "Internal Server error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCollateralsForUserTask([Required]string emailId)
        {
            
            try
            {
                IEnumerable<ApplyCollateralDTO> listOfAllCollateralsForUserTask =  await _businessLogic.GetCollateralBusinessLogic().GetAllCollateralByUserEmailTask(emailId);
                return StatusCode(200, listOfAllCollateralsForUserTask);
            }
            catch(Exception ex)
            {
                //log ex
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
