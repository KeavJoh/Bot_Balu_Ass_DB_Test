using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bot_Balu_Ass_DB.ValidationController
{
    internal class DeregistrationRegistrationValidation
    {
        public static bool DateIsInThePast(DateTime dateToCheck)
        {
            if (dateToCheck < DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static bool DeregistrationRegistrationForOneDay(DateTime dateToCheckFrom, DateTime dateToCheckTo)
        {
            if(dateToCheckTo == DateTime.MinValue || dateToCheckTo == dateToCheckFrom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DateToIsLowerDateFrom(DateTime dateToCheckTo, DateTime dateToCheckFrom)
        {
            if(dateToCheckTo < dateToCheckFrom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
