using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class LoginUserDTO
    {
        [Required]
      
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$")]
        public string Password { get; set; }
    }
}
