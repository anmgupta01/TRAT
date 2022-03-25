using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterCountryLocation : System.Web.UI.Page
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
                    else
                        LoadMasters();
                }
                alert.Visible = false;
                gvProjects.EditIndex = -1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void LoadMasters()
        {
            try
            {

                DBHandler handler = new DBHandler();
                DataSet ds = new DataSet();
                ds = handler.GetMasters();

                drpCountry.DataSource = ds.Tables[4];
                drpCountry.DataTextField = "Name";
                drpCountry.DataValueField = "Id";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void bindGrid()
        {
            try
            {
                DBHandler handler = new DBHandler();
                DataTable dt = new DataTable();
                if (drpCountry.SelectedValue != "-1")
                {
                    dt = handler.GetMemberfirmOffices(Convert.ToInt32(drpCountry.SelectedValue));

                    gvProjects.DataSource = dt;
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
                lblAlert.InnerText = dbh.MasterLocation(e.Keys["Id"].ToString(), drpCountry.SelectedValue, e.Values["Office"].ToString(), "1", CommonFunctions.GetUser());
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
                string newval = e.NewValues["Office"].ToString();
                lblAlert.InnerText = dbh.MasterLocation(e.Keys["Id"].ToString(), drpCountry.SelectedValue, newval, "0", CommonFunctions.GetUser());
                alert.Visible = true;
                gvProjects.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberFirmLocationName.Text.Trim() != "")
                {
                    lblAlert.InnerText = dbh.MasterLocation("0", drpCountry.SelectedValue, txtMemberFirmLocationName.Text, "0", CommonFunctions.GetUser());
                    bindGrid();
                    alert.Visible = true;

                    txtMemberFirmLocationName.Text = string.Empty;
                }
                else
                {
                    lblAlert.InnerText = "Enter the name of the sub-sector!";
                    alert.Visible = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}