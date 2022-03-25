using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TRAT
{
    public partial class Search : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.ValidateEmail(CommonFunctions.GetUser()) == false)
            {
                Response.Redirect("~/Home.aspx?id=Success");
            }
        }

        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            lblAlert.InnerText = "";
            alert.Visible = false;
            bindGrid();
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
                    Response.Redirect("ProjectForm.aspx?project=" + projectId);

                }
                else if (e.CommandName == "Replicate")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());                    
                    Response.Redirect("ProjectForm.aspx?project=" + rowIndex + "&Replication="+ rowIndex);

                }
                else if (e.CommandName == "Delete")
                {
                    lblAlert.InnerText = dbh.Project_Data_Deactivate(e.CommandArgument.ToString(), "0", userid);
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

        void bindGrid()
        {
            try
            {

                if (txtProjectName.Text.Trim() != "")
                {
                    gvProjects.DataSource = dbh.Project_Data_SEARCH_Project(txtProjectName.Text.Trim());
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
    }
}

