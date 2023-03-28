using BusinessLogic;
using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanAPI.Controllers
{
    public class PromotionsController : Controller
    {
        private IBusinessLogic _businessLogic;
        public PromotionsController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }
        [HttpPost]
        public async Task<IActionResult> AddPromotionsTask([FromBody] PromotionsDTO promotionsDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Promotions Details are Incorrect");
            }
            try
            {
                PromotionsDTO promotion= await _businessLogic.GetPromotionsBusinessLogic().AddNewPromotionTask(promotionsDTO);
                return StatusCode(202, "Promotions Added Sucessfully");
            }
            catch(Exception exception)
            {
                return StatusCode(500, "Promotion Cannot Be Added");
            }
        }

    }
}
