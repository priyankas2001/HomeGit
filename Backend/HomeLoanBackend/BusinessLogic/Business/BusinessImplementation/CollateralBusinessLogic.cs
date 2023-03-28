using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BusinessLogic.Business.BusinessImplementation
{
    public class CollateralBusinessLogic:ICollateralBusinessLogic<ApplyCollateralDTO,EditCollateralDTO>
    {
        IMapper _mapper;
        private Logging _logger;
        IBasicOperations<Collateral> _collateralBasicOperation;
        IAdvanceUserOperation<User> _userAdvanceUserOperation;
        IAdvanceListOperation<Collateral> _collateralAdvanceListOperation;
        public CollateralBusinessLogic(IMapper mapper, Logging logger,
        IBasicOperations<Collateral> collateralBasicOperation,
        IAdvanceUserOperation<User> userAdvanceUserOperation,
        IAdvanceListOperation<Collateral> collateralAdvanceListOperation)
        {
            _logger = logger;
            _mapper = mapper;
            _userAdvanceUserOperation = userAdvanceUserOperation;
            _collateralBasicOperation = collateralBasicOperation;
            _collateralAdvanceListOperation = collateralAdvanceListOperation;
        }
        public async Task<Guid> AddCollateralTask(ApplyCollateralDTO entity)
        {
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            if(userFromDAL == null)
            {
                throw new ArgumentException();
            }
            Collateral collateral = _mapper.Map<Collateral>(entity);
            collateral.UserId = userFromDAL.Id;
            collateral.ApplicationId = new Guid();
            Collateral collateralFromDAL = await _collateralBasicOperation.AddTask(collateral); 
            return collateralFromDAL.Id;
        }
        public async Task<bool> DeleteCollateralTask(Guid id)
        {
            return await _collateralBasicOperation.RemoveTask(id);
        }
        public async Task<IEnumerable<ApplyCollateralDTO>> GetAllCollateralByUserEmailTask(string emailId)
        {
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(emailId);
            IEnumerable <Collateral> listOfCollateralsByEmailFromDAL = await _collateralAdvanceListOperation.GetAllRecordsByParameterTask(userFromDAL.Id, "UserId");
            IEnumerable<ApplyCollateralDTO> listOfCurrentUserApplyCollateralDTO = _mapper.Map<IEnumerable<ApplyCollateralDTO>>(listOfCollateralsByEmailFromDAL);
            foreach (ApplyCollateralDTO applyCollateralDTO in listOfCurrentUserApplyCollateralDTO)
            {
                applyCollateralDTO.EmailId = emailId;
            }
            return listOfCurrentUserApplyCollateralDTO;
        }
        public async Task<Guid> EditCollateralTask(EditCollateralDTO entity)
        {
            User userFromDAL = await _userAdvanceUserOperation.GetByEmailTask(entity.EmailId);
            Collateral collateral = _mapper.Map<Collateral>(entity);
            collateral.UserId = userFromDAL.Id;
            await _collateralBasicOperation.EditTask(collateral);
            return entity.Id;
        }
    }
}
