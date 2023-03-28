using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Utility
{
    public class CustomDateRangeAttribute:RangeAttribute
    {    
        public CustomDateRangeAttribute() : base(typeof(DateTime),
            DateTime.Now.AddYears(5).ToShortDateString(),
            DateTime.Now.ToShortDateString())
        { }
    }
}
