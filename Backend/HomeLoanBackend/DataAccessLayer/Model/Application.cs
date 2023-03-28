using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Model
{
    public class Application
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey (nameof(UserId))]
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        [Required]
        public Guid PropertyId { get; set; }

        [ForeignKey(nameof(PersonalIncomeId))]
        [Required]
        public Guid PersonalIncomeId { get; set; }

        [ForeignKey(nameof(LoanRequirementId))]
        [Required]
        public Guid LoanRequirementId { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
