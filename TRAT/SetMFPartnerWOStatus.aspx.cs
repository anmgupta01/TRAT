using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class SetMFPartnerWOStatus : System.Web.UI.Page
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

                    //LoadWorkOrders();

                }
                alert.Visible = false;

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
                int requestSource = 1;
                if (txtWorkOrderName.Text != null || txtWorkOrderName.Text != "")
                {
                    dt = dbh.Work_Order_SEARCH(txtWorkOrderName.Text.Trim(), requestSource);
                    gvProjects.DataSource = dt;
                }
                gvProjects.DataBind();
                if (gvProjects.Rows.Count > 0)
                { gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader; }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public void LoadWorkOrders()
        //{
        //    DataSet ds = new DataSet();
        //    ds = dbh.WorkOrderList_GET();

        //    ddlWO.DataSource = ds.Tables[0];

        //    ddlWO.DataTextField = "WOLogNumber";
        //    ddlWO.DataValueField = "Id";
        //    ddlWO.DataBind();
        //    ddlWO.Items.Insert(0, new ListItem("--Select--", "-1"));
        //}
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //lblAlert.InnerText = dbh.SetMFParterWOStatus(682, Convert.ToInt32(ddlStatus.SelectedValue),
            //     CommonFunctions.GetUser());
            //alert.Visible = true;
            //bindGrid();
        }
        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                int userid = CommonFunctions.GetUserId();

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                //var WOLogNumber = e.CommandArgument;
                string MFApprover = selectedRow.Cells[2].Text;
                if (e.CommandName == "Approve")
                {
                    lblAlert.InnerText = dbh.SetMFParterWOStatus(rowIndex, 1,
                     CommonFunctions.GetUser());
                    alert.Visible = true;
                    bindGrid();
                }
                else if (e.CommandName == "Reject")
                {
                    lblAlert.InnerText = dbh.SetMFParterWOStatus(rowIndex, 0,
                     CommonFunctions.GetUser());
                    alert.Visible = true;
                    bindGrid();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void ddlWO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DBHandler handler = new DBHandler();

                bindGrid();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnWorkOrder_Click(object sender, EventArgs e)
        {
            try
            {

                lblAlert.InnerText = "";
                alert.Visible = false;
                bindGrid();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}