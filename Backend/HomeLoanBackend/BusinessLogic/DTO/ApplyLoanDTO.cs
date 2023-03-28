using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Utility;

namespace BusinessLogic.DTO
{
    public class ApplyLoanDTO
    {
        //User
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        //Property
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(25.0,1000.0)]
        public decimal Size { get; set; }
        [Required]
        [Range(1000, 990000000)]
        public decimal Cost { get; set; }
        [Required]
        [Range(1000, 1000000)]
        public decimal RegistrationCost { get; set; }

        //PersonalIncome
        [Required]
        [Range(1000, 10000000)]
        public decimal MonthlyFamilyIncome { get; set; }
        [Required]
        [Range(1000, 10000000)]
        public decimal OtherIncome { get; set; }

        //LoanRequirement
        [Required]
        [Range(1000, 990000000)]
        public decimal LoanAmount { get; set; }
        [Required]
        [Range(1, 20)]
        public int LoanDuration { get; set; }
        [Required]        
        [CustomDateRange]
        public DateTime LoanStartDate { get; set; }
    }
}
