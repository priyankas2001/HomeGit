using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryImplementation;
using DataAccessLayer.Repository.RepositoryInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Algorithm
{
    public class CollateralCalculator<T> where T : class
    {
        private readonly IBasicOperations<Collateral> _basicOperations;
        private readonly IBasicOperations<LoanRequirements> loanRequirements;

        public CollateralCalculator(IBasicOperations<Collateral> basicOperations,IBasicOperations<LoanRequirements> loanRequirements)
        {
            _basicOperations = basicOperations;
            this.loanRequirements = loanRequirements;
        }
        
        public decimal collateralValue(Guid id)
        {
            var loanAmount =  this.loanRequirements.GetByIdTask(id).Result.LoanAmount;
            var Data = new Dictionary<string, int>()
            {
                { "Insurance_Policy_Worth",20000}, // 80%
                { "Gold_Worth",50000},  //75%
                { "Stock_Worth",10000}, //50%
                { "Property_Worth",200000}   //80%
            };
            var collateralAmount = (decimal)((Data["Insurance_Policy_Worth"] * .8) + (Data["Gold_Worth"] * .75) + (Data["Stock_Worth"] * .5) + (Data["Property_Worth"] * .8));

            var colletralPercantage = (collateralAmount / loanAmount) * 100;
            return colletralPercantage;

            // ek function bnana hai jo ek colletral worth nikale based on the share of user and and bank evolution of that colletral
            // colletral type + user share + value of colletral 

        }

        public async Task<string> Status(Guid id)
        {

            var colletralPercentage = collateralValue(id);


            switch (colletralPercentage)
            {
                case decimal n when n <= 40:
                    return "RED";

                case decimal n when n <= 70:
                    return "YELLOW";

                case decimal n when n > 70:
                    return "GREEN";

                default:
                    return "Colletral value could not be read";
            }

        }


       



    }
}
