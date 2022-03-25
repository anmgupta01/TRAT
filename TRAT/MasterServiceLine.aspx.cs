using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterServiceLine : System.Web.UI.Page
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
                ds = handler.GetMasters();
                gvProjects.DataSource = ds.Tables[2];
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

                if (txtSectorName.Text.Trim() != "")
                {
                    lblAlert.InnerText = dbh.MasterServiceLine("0", txtSectorName.Text, "0", CommonFunctions.GetUser());
                    alert.Visible = true;
                    bindGrid();
                    gvProjects.EditIndex = -1;
                    txtSectorName.Text = string.Empty;
                }
                else
                {
                    lblAlert.InnerText = "Enter the name of the sector!";
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

        }

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                lblAlert.InnerText = dbh.MasterServiceLine(e.Keys["Id"].ToString(), e.Values["Name"].ToString(), "1", CommonFunctions.GetUser());
                alert.Visible = true;
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
            lblAlert.InnerText = dbh.MasterServiceLine(e.Keys["Id"].ToString(), newval, "0", CommonFunctions.GetUser());
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