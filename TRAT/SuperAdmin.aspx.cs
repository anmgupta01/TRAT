using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class SuperAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string userid = CommonFunctions.GetUser();
                bool isAuthorized = CheckUserAuthorization(userid);
                if (isAuthorized)
                {
                    divOnlyForAdmin.Visible = true;
                }
                else
                {
                    divForNonAdmins.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckUserAuthorization(string userid)
        {

            return false;
        }
    }
}