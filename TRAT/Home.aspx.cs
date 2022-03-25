using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("Username : " + HttpContext.Current.User.Identity.Name);
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                divForOtherThanDIJV.Visible = true;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Hi Vijay");
            }
        }
        private void MsgBox(string message)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('" + message + "');", true);
        }


    }
}