using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRAT
{
    public class ErrorLogs

    {
        public static string GetUser()
        {
            string formatedString = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            formatedString = formatedString.Remove(0, formatedString.IndexOf(@"\") + 1);
            formatedString = formatedString + "@deloitte.com";
            return formatedString;
            //return "prnayak@deloitte.com";
        }

        public static int GetUserId()
        {
            return Convert.ToInt32(new DBHandler().GetUserId());
        }

        public static bool ValidateEmail(string emailId)
        {
            return emailId != "" ? Convert.ToBoolean(new DBHandler().ValidateEmail(emailId)) : true;
        }

        internal static int GetUseRole(string userId)
        {
            string userRole;
            return Convert.ToInt32(new DBHandler().Validate_AdminByEmail(0, userId, out userRole));
        }
        public static bool CheckUserAccessOfWorkOrderEdit(string emailId, int projectId)
        {
            return emailId != "" ? Convert.ToBoolean(new DBHandler().CheckUserAccessOfWorkOrderEdit(emailId, projectId)) : true;
        }
    }
}
