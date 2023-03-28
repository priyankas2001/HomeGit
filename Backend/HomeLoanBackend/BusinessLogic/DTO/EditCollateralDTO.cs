using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class EditCollateralDTO
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Range(1000, 990000000)]
        public decimal Value { get; set; }
        [Required]
        [Range(1, 100)]
        public decimal Share { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
