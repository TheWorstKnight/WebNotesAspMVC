using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.bl.Infrustructure
{
    public class DateRestrictAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime inputDate = (DateTime)value;
                DateTime currentDate = DateTime.Today;
                if (inputDate.Year < currentDate.Year || inputDate.DayOfYear < currentDate.DayOfYear || inputDate.TimeOfDay < currentDate.TimeOfDay)
                    return false;
                else return true;
            }
            else return false;

        }
    }
}
