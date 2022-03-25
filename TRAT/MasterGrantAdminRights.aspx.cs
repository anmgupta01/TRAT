using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterGrantAdminRights : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    if (CommonFunctions.GetUseRole(CommonFunctions.GetUser()) != 5)
                        Response.Redirect("~/Home.aspx", true);

                    LoadEmployees();
                    LoadRights();
                    btnDele.Visible = false;
                }
                alert.Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LoadRights()
        {
            try
            {


                ddlRights.DataSource = dbh.GetRights();
                ddlRights.DataTextField = "Role";
                ddlRights.DataValueField = "Id";
                ddlRights.DataBind();

                //ddlRights.Items.Insert(0, new ListItem("None", "0"));
                ddlRights.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadEmployees()
        {
            try
            {


                DataSet ds = new DataSet();
                ds = dbh.GetEmployeesData("");
                ds.Tables[0].DefaultView.Sort = "Employee_Name ASC";
                ds.Tables[0].Columns.Add(new DataColumn("Title", Type.GetType("System.String"), "Employee_Name + ' - ' + Email"));

                ddlEmpList.DataSource = ds.Tables[0];

                ddlEmpList.DataTextField = "Title";
                ddlEmpList.DataValueField = "Id";
                ddlEmpList.DataBind();
                ddlEmpList.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlRights_SelectedIndexChanged(object sender, EventArgs e)
        {
            var intRoleId = Convert.ToInt32(ddlRights.SelectedValue);
            if (intRoleId != 6)
            {
                btnDele.Visible = false;
            }
            else
            {
                btnDele.Visible = true;
            }
        }
        private void MsgBox(string message)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + message + "');";
            msg += "</script>";
            Response.Write(msg);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var intRoleId = Convert.ToInt32(ddlRights.SelectedValue);
                string[] tokens = ddlEmpList.SelectedItem.Text.Split('-');
                string strName = tokens[0].TrimEnd();
                string message1 = "";
                if (intRoleId == 6)
                {
                    dbh.SaveWorkOrderRights(Convert.ToInt32(ddlEmpList.SelectedValue), 1, strName, out message1);
                    MsgBox(message1);
                }
                else
                {
                    lblAlert.InnerText = dbh.SetAccessRight(Convert.ToInt32(ddlEmpList.SelectedValue), intRoleId,
                    (intRoleId == 0 ? true : false), CommonFunctions.GetUser());
                    alert.Visible = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnDele_Click(object sender, EventArgs e)
        {
            try
            {
                var intRoleId = Convert.ToInt32(ddlRights.SelectedValue);
                string[] tokens = ddlEmpList.SelectedItem.Text.Split('-');
                string strName = tokens[0].TrimEnd();
                string message1 = "";
                if (intRoleId == 6)
                {
                    dbh.SaveWorkOrderRights(Convert.ToInt32(ddlEmpList.SelectedValue), 0, strName, out message1);
                    MsgBox(message1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}