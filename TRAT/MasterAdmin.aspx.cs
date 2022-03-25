using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterAdmin : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindGrid();
                }
                alert.Visible = false;
                gvProjects.EditIndex = -1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        void bindGrid()
        {
            try
            {
                DBHandler handler = new DBHandler();
                DataSet ds = new DataSet();
                ds = handler.GetUserRoles();
                gvProjects.DataSource = ds.Tables[1];
                gvProjects.DataBind();
                if (gvProjects.Rows.Count > 0)
                { gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader; }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(drpEmployeeNames.SelectedValue) != -1)
                {
                    lblAlert.InnerText = dbh.MasterAdmin(drpEmployeeNames.SelectedValue, "0", CommonFunctions.GetUser());
                    bindGrid();
                    drpEmployeeNames.Text = string.Empty;
                }
                else
                {
                    lblAlert.InnerText = "Please select an employee";
                    alert.Visible = true;
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

                }

                if (e.CommandName == "Delete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
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
            try
            {
                lblAlert.InnerText = dbh.MasterAdmin(e.Keys["Id"].ToString(), "1", CommonFunctions.GetUser());
                gvProjects.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvProjects.EditIndex = e.NewEditIndex;
                bindGrid();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvProjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvProjects.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvProjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string newval = e.NewValues["Name"].ToString();
                lblAlert.InnerText = dbh.MasterAdmin(e.Keys["Id"].ToString(), "0", CommonFunctions.GetUser());
                alert.Visible = true;
                gvProjects.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}