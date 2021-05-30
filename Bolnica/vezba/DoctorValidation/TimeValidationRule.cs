using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace vezba.DoctorValidation
{
    class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;
            int r;
            var split = s.Split(':');
            if(split.Length != 2 || !int.TryParse(split[0], out r) || !int.TryParse(split[1], out r))
                return new ValidationResult(false, "Format: h:min");
            return new ValidationResult(true, null);
        }
    }
}
