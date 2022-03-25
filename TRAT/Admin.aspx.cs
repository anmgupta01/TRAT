using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class Admin : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            if (CommonFunctions.ValidateEmail(CommonFunctions.GetUser()) == false)
            {                
                Response.Redirect("~/Home.aspx?id=Success");
            }

            DisableAllSections();
            switch (CommonFunctions.GetUseRole(CommonFunctions.GetUser()))
            {
                case 2: //Admin
                    divOnlyForAdmin.Visible = true;
                    break;
                case 5: //Super Admin
                    divSuperAdmin.Visible = true;
                    break;
                case 6:
                    divForWorkOrderUsers.Visible = true;
                    break;
                default: // Other users
                    divForNonAdmins.Visible = true;
                    break;
            }
                DataTable dsEmployee = new DataTable();
                dsEmployee = dbh.GetSingleEmployeeDetails("0", CommonFunctions.GetUser());
                if (dsEmployee.Rows[0]["Department_Name"].ToString() == "Transaction Services")
                {
                    divTSReport.Visible = true;
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        
        private void DisableAllSections()
        {

            divOnlyForAdmin.Visible = false;
            divSuperAdmin.Visible = false;
            divForNonAdmins.Visible = false;
            divForWorkOrderUsers.Visible = false;
        }
    }
}