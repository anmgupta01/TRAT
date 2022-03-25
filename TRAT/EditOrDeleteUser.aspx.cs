using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class EditOrDeleteUser : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    var intRole = CommonFunctions.GetUseRole(CommonFunctions.GetUser());
                    if (intRole != 2 && intRole != 5)
                        Response.Redirect("~/Home.aspx", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            bindGrid();
        }
        void bindGrid()
        {
            try
            {
                if (txtResourceName.Text.Trim() != "")
                {
                    gvProjects.DataSource = dbh.EmployeeSearch(txtResourceName.Text.Trim());
                    gvProjects.DataBind();
                    if (gvProjects.Rows.Count > 0)
                    { gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader; }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                string userid = CommonFunctions.GetUser();
                if (e.CommandName == "Edit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string projectId = this.gvProjects.DataKeys[rowIndex][0].ToString();
                    Response.Redirect("AddNewResource.aspx?EmpID=" + projectId);

                }
                if (e.CommandName == "Delete")
                {
                    lblAlert.InnerText = dbh.Employee_Data_Deactivate(e.CommandArgument.ToString(), "0", userid);
                    bindGrid();
                    alert.Visible = true;

                }
                if (e.CommandName == "Activate")
                {
                    lblAlert.InnerText = dbh.Employee_Data_Deactivate(e.CommandArgument.ToString(), "1", userid);
                    bindGrid();
                    alert.Visible = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}