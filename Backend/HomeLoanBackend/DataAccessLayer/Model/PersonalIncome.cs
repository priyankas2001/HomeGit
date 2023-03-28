using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.Model
{
    public class PersonalIncome
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal MonthlyFamilyIncome { get; set; }
        [Required]
        public decimal OtherIncome { get; set; }
    }
}
