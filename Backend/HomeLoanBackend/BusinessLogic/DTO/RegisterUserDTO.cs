using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
   public  class RegisterUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$")]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^([0-9]{10})$")]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(3)]
        [MinLength(2)]
        public string CityCode { get; set; }
        [Required]
        [MaxLength(3)]
        [MinLength(2)]
        public string StateCode { get; set; }

        [Required]
        [MaxLength(3)]
        [MinLength(2)]
        public string CountryCode { get; set; }
    }
}
