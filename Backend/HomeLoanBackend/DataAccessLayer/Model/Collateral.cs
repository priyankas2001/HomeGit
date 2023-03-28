using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Model
{
    public class Collateral
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
        public decimal InsuranceWorth { get; set; }
        public decimal StockWorth { get; set; }
        public decimal GoldWorth { get; set; }

        [Required]
        public decimal Value { get; set; }
        [Required]
        public decimal Share { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        [Required]
        [ForeignKey(nameof(ApplicationId))]
        public Guid ApplicationId { get; set; }

    }
}
