using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class TempReport : System.Web.UI.Page
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
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
    }
}