using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class LoanRequirements
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal LoanAmount { get; set; }
        [Required]
        public int LoanDuration { get; set; }
        [Required]
        public DateTime LoanStartDate { get; set; }

    }
}
