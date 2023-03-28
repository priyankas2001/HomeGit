using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTO
{
    public class AdvisorUpdatePasswordDTO
    {
        [Required(ErrorMessage ="Advisor email id is required")]
        [EmailAddress]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage ="Advisor email id fromat is not correct")]
        public string EmailId { get; set; }
        [Required(ErrorMessage ="Advisor password is required")]
        [PasswordPropertyText(true)]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$")]
        public string NewPassword { get; set; }
    }
}
