using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace BusinessLogic.DTO
{
    public class UserUpdatePasswordDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$")]
        public string NewPassword { get; set; }
    }
}
