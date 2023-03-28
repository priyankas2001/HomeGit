
using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;
using System.Linq;

namespace BusinessLogic.Business.BusinessImplementation
{
    public class PromotionBusinessLogic : IPromotionBusinessLogic<PromotionsDTO>
    {
        IMapper _mapper;
        private Logging _logger;
        IBasicOperations<Promotions> _promotionsBasicOperation;
        IAdvanceStatusOperation _promotionStatusOperation;
        IAdvanceParameterOperation<Promotions, int> _promotionParameterOperation;
        public PromotionBusinessLogic(IMapper mapper, Logging logger, IBasicOperations<Promotions> promotionsBasicOperartion,  IAdvanceStatusOperation statusOperation,IAdvanceParameterOperation<Promotions,int> promotionsParameter)
        {
            _logger = logger;
            _mapper = mapper;
            _promotionsBasicOperation = promotionsBasicOperartion;
            _promotionStatusOperation = statusOperation;
            _promotionParameterOperation = promotionsParameter;
        }
        public async Task<PromotionsDTO> AddNewPromotionTask(PromotionsDTO newPromotion)
        {
            Promotions promotion=await _promotionParameterOperation.GetByParameterBasedOperation(0, "Status");
            if(promotion!=null)
            {
                await _promotionStatusOperation.SwitchStatus(promotion.Id);

            }
            
            Promotions promotions = _mapper.Map<Promotions>(newPromotion);
           
                Promotions promotionReceivedFromDAL = await _promotionsBasicOperation.AddTask(promotions);
                await _promotionsBasicOperation.SaveChanges();
                PromotionsDTO returnedPromotion = _mapper.Map<PromotionsDTO>(promotionReceivedFromDAL);
                return returnedPromotion;
            
           
        }

       
    }
}
