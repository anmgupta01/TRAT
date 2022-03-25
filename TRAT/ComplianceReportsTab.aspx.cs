using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class ComplianceReportsTab : System.Web.UI.Page
    {
        DBHandler dh = new DBHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunctions.ValidateEmail(CommonFunctions.GetUser()) == false)
                {
                    Response.Redirect("~/Home.aspx?id=Success");
                }
                alert.Visible = false;

                if (!IsPostBack)
                {
                    LoadMasters();
                    AssignDates();
                    BindProjects();
                    BindEmployees();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Load Methods
        public void AssignDates()
        {
            try
            {


                if (DateTime.Today.Month < 6)
                {

                    dtStartDate.Value = (DateTime.Today.Year - 1) + "-06-01";
                    dtEndDate.Value = DateTime.Today.Year + "-05-31";
                }
                else
                {
                    dtStartDate.Value = (DateTime.Today.Year) + "-06-01";
                    dtEndDate.Value = (DateTime.Today.Year + 1) + "-05-31";
                }
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


                lblNoRecords.Visible = false;
                //ddlEmpList.Items.Insert(0, new ListItem("All", "-1"));
                //ddlEmpList.Enabled = false;

                DBHandler handler = new DBHandler();
                DataSet ds = new DataSet();
                ds = handler.GetMasters();

                drpServiceLine.DataSource = ds.Tables[2];
                drpServiceLine.DataTextField = "Name";
                drpServiceLine.DataValueField = "Id";
                drpServiceLine.DataBind();
                drpServiceLine.Items.Insert(0, new ListItem("All", "-1"));

                drpCountry.DataSource = ds.Tables[6];
                drpCountry.DataTextField = "Name";
                drpCountry.DataValueField = "Id";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("All", "-1"));
                drpCountry.Items.Insert(4, new ListItem("Others", "-2"));

                drpDepartment.DataSource = ds.Tables[10];
                drpDepartment.DataTextField = "Name";
                drpDepartment.DataValueField = "Id";
                drpDepartment.DataBind();
                drpDepartment.Items.Insert(0, new ListItem("All", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void BindEmployees()
        {
            try
            {

                ddlEmpList.Enabled = true;

                if (drpServiceLine.SelectedItem.Text != "All")
                {
                    DataSet ds = new DataSet();
                    ds = dh.GetEmployeesData(drpServiceLine.SelectedItem.Text);
                    ds.Tables[1].DefaultView.Sort = "Employee_Name ASC";
                    ddlEmpList.DataSource = ds.Tables[1];
                    ddlEmpList.DataTextField = "Employee_Name";
                    ddlEmpList.DataValueField = "Id";
                    ddlEmpList.DataBind();
                    ddlEmpList.Items.Insert(0, new ListItem("All", "-1"));

                }
                else
                {
                    DataSet ds = new DataSet();
                    ds = dh.GetEmployeesData(drpServiceLine.SelectedItem.Text);
                    ds.Tables[0].DefaultView.Sort = "Employee_Name ASC";
                    ddlEmpList.DataSource = ds.Tables[0];
                    ddlEmpList.DataTextField = "Employee_Name";
                    ddlEmpList.DataValueField = "Id";
                    ddlEmpList.DataBind();
                    ddlEmpList.Items.Insert(0, new ListItem("All", "-1"));

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void BindProjects()
        {
            try
            {


                DataSet ds = new DataSet();
                ds = dh.GetMasters();
                //ds.Tables[5].DefaultView.Sort = "Name ASC";
                //ddlProject.DataSource = ds.Tables[5];
                //ddlProject.DataTextField = "Name";
                //ddlProject.DataValueField = "Id";
                //ddlProject.DataBind();
                //ddlProject.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Click Events
        protected void btnClrSelect_Click(object sender, EventArgs e)
        {
            try
            {


                txtProjectName.Text = string.Empty;
                lblNoRecords.Visible = false;

                if (gvProjects.Rows.Count > 0)
                {
                    //gvProjects = new GridView();
                    gvProjects.DataSource = null;
                    gvProjects.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            try
            {
                //lblAlert.InnerText = "";
                //alert.Visible = false;
                //bindGrid();
                bindGridReportsSearch();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnDownloadReport_Click(object sender, EventArgs e)
        {
            try
            {


                int reportType = 0;
                //DataSet ds = new DataSet();
                //ds = dh.GetEmployeeReport(2, int.Parse(ddlEmpList.SelectedValue), dtStartDate.Value.ToString(), dtEndDate.Value.ToString());
                //ExportToExcel(ds.Tables[0]);
                //string projectIds = string.Empty;
                int projectId = -1;


                foreach (GridViewRow gvrow in gvProjects.Rows)
                {

                    //CheckBox chk = (CheckBox)gvrow.FindControl("chkProject");
                    RadioButton rd = (RadioButton)gvrow.FindControl("rdProjectSelect");

                    if (rd != null && rd.Checked)
                    {
                        projectId = (int)gvProjects.DataKeys[gvrow.RowIndex].Values["Id"];
                        //projectIds += (int)gvProjects.DataKeys[gvrow.RowIndex].Values["Id"] + ",";

                    }
                }
                reportType = 1;

                //if (rdDay.Checked)
                //    reportType = 0;
                //else if (rdWeek.Checked)
                //    reportType = 1;
                //else if (rdMonth.Checked)
                //    reportType = 2;
                //else if (rdQuat.Checked)
                //    reportType = 3;

                DataSet ds = new DataSet();
                string message = "";
                ds = dh.GetFilteredEmployeeComplianceReport(reportType, dtStartDate.Value.ToString(), dtEndDate.Value.ToString(), int.Parse(drpServiceLine.SelectedValue),
                    int.Parse(drpCountry.SelectedValue), int.Parse(ddlEmpList.SelectedValue), projectId, int.Parse(drpDepartment.SelectedValue), int.Parse(drpSubDepartment.SelectedValue), out message);
                if (message != null && message != "")
                {
                    MsgBox(message);
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ExportToExcel(ds.Tables[0]);
                }
                else
                {
                    alert.Visible = true;
                }

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


                if (txtProjectName.Text.Trim() != "")
                {
                    gvProjects.DataSource = dh.Project_Data_SEARCH(txtProjectName.Text.Trim());
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
        void bindGridReportsSearch()
        {
            //int projectIds = (int)gvProjects.DataKeys[row.RowIndex].Values["Id"];
            try
            {


                gvProjects.DataSource = dh.Reports_Data_SEARCH(txtProjectName.Text.Trim(), int.Parse(drpServiceLine.SelectedValue), int.Parse(drpCountry.SelectedValue));
                gvProjects.DataBind();
                if (gvProjects.Rows.Count > 0)
                {
                    gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader;
                    lblNoRecords.Visible = false;
                }
                else
                {
                    lblNoRecords.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        private void MsgBox(string message)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + message + "');";
            msg += "</script>";
            Response.Write(msg);
        }
        protected void drpServiceLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployees();

        }
        protected void gvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    RadioButton rb = (RadioButton)e.Row.FindControl("rdProjectSelect");
                    rb.Checked = true;
                }
            }
            //header select all function
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
            //        "javascript:SelectAll('" +
            //        ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            //}

        }

        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DBHandler dh = new DBHandler();


        }

        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (drpDepartment.SelectedValue != "-1")
                {
                    dt = new DBHandler().GetSubDepartment(0, drpDepartment.SelectedItem.Text);

                    drpSubDepartment.DataSource = dt;
                    drpSubDepartment.DataTextField = "Name";
                    drpSubDepartment.DataValueField = "Id";
                    drpSubDepartment.DataBind();
                    drpSubDepartment.Items.Insert(0, new ListItem("All", "-1"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string filename = "Report.xls";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    DataGrid dgGrid = new DataGrid();
                    dgGrid.DataSource = dt;
                    dgGrid.DataBind();
                    //dgGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
                    //Get the HTML for the control.
                    dgGrid.RenderControl(hw);
                    //Write the HTML back to the browser.
                    //Response.ContentType = application/vnd.ms-excel;

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");

                    this.EnableViewState = false;
                    Response.Write(tw.ToString());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}