using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class ApplyCollateralDTO
    {
        [Required]
        [MinLength(1)]
        public string Type { get; set; }
        [Required]
        [Range(1000, 990000000)]
        public decimal Value { get; set; }
        [Required]
        [Range(1, 100)]
        public decimal Share { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        public decimal InsuranceWorth { get; set; }
        public decimal StockWorth { get; set; }
        public decimal GoldWorth { get; set; }
    }
}
