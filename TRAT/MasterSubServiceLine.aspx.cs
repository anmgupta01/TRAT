using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterSubServiceLine : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    var intRole = CommonFunctions.GetUseRole(CommonFunctions.GetUser());
                    if (intRole != 5 && intRole != 2)
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

                drpServiceLine.DataSource = ds.Tables[2];
                drpServiceLine.DataTextField = "Name";
                drpServiceLine.DataValueField = "Id";
                drpServiceLine.DataBind();
                drpServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));


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

                string newVal = e.NewValues["Name"].ToString();
                lblAlert.InnerText = dbh.MasterSubServiceLine(Convert.ToInt32(e.Keys["SubServiceLineId"]), Convert.ToInt32(drpServiceLine.SelectedValue), newVal, 0, CommonFunctions.GetUser());
                alert.Visible = true;
                gvProjects.EditIndex = -1;
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

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                lblAlert.InnerText = dbh.MasterSubServiceLine(Convert.ToInt32(e.Keys["SubServiceLineId"]), Convert.ToInt32(drpServiceLine.SelectedValue), e.Values["Name"].ToString(), 1, CommonFunctions.GetUser());
                gvProjects.EditIndex = -1;
                bindGrid();

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

                int rowIndex;
                switch (e.CommandName)
                {
                    case "Edit":
                        rowIndex = int.Parse(e.CommandArgument.ToString());
                        var projectId = gvProjects.DataKeys[rowIndex][0].ToString();
                        break;

                    case "Delete":
                        rowIndex = int.Parse(e.CommandArgument.ToString());
                        bindGrid();
                        alert.Visible = true;
                        break;
                }

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

                if (txtSubServiceLineName.Text.Trim() != "")
                {
                    lblAlert.InnerText = dbh.MasterSubServiceLine(0, Convert.ToInt32(drpServiceLine.SelectedValue), txtSubServiceLineName.Text, 0, CommonFunctions.GetUser());
                    alert.Visible = true;

                    bindGrid();
                    txtSubServiceLineName.Text = string.Empty;
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

        protected void drpServiceLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }
        public void bindGrid()
        {
            try
            {

                DBHandler handler = new DBHandler();
                DataTable dt = new DataTable();
                if (drpServiceLine.SelectedValue == "-1") return;

                dt = handler.GetSubServiceLine(Convert.ToInt32(drpServiceLine.SelectedValue));
                gvProjects.DataSource = dt;
                gvProjects.DataBind();
                if (gvProjects.Rows.Count == 0) return;
                gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}