using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class Allocate : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunctions.ValidateEmail(CommonFunctions.GetUser()) == false)
                {
                    Response.Redirect("~/Home.aspx?id=Success");
                }
                if (!IsPostBack)
                {
                    //txtTodate.Attributes["max"] = getLastDateThisYear();
                    if (Request.QueryString["project"] != null)
                    {
                        txtProjectName.Text = Request.QueryString["project"];
                        btnSearchProject_Click(new object(), new EventArgs());
                    }
                    txtFromDate.Attributes["min"] = getStartDayOfTheWeek();
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            try
            {
                //int a = 0;
                //int b = 1;
                //int c = b / a;

                if (txtProjectName.Text.Trim() != "")
                {
                    bindProjects(txtProjectName.Text.Trim());
                    gvResources.DataSource = null;
                    gvResources.DataBind();
                    lblProjectNameAllocate.Text = "";
                    lblProjName.Text = "";
                    hfProjectId.Value = "";
                    divAllocate.Visible = false;

                    divResources.Visible = false;
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
                
            }
        }
        //--------------------------------
        public void LoadEmployees()
        {
            try
            {
                DBHandler dh = new DBHandler();
                DataSet ds = new DataSet();
                ds = dh.GetEmployeesData("");
                ds.Tables[0].DefaultView.Sort = "Employee_Name ASC";
                ds.Tables[0].Columns.Add(new DataColumn("Title", System.Type.GetType("System.String"), "Employee_Name + ' - ' + Email"));

                ddlEmpList.DataSource = ds.Tables[0];

                ddlEmpList.DataTextField = "Title";
                ddlEmpList.DataValueField = "Email";
                ddlEmpList.DataBind();
                ddlEmpList.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        //------------------------------
        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                divAllocate.Visible = true;
                LoadEmployees();
                divResources.Visible = true;
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string projectId = this.gvProjects.DataKeys[rowIndex][0].ToString();
                hfStartDate.Value = this.gvProjects.DataKeys[rowIndex][1].ToString();
                try
                {
                    DateTime startDate = DateTime.Parse(this.gvProjects.DataKeys[rowIndex][1].ToString());
                    txtFromDate.Attributes["min"] = startDate.ToString("yyyy-MM-dd");
                    lblStartDateDispaly.Text = startDate.ToString("yyyy-MM-dd");
                }
                catch { }
                hfEndDate.Value = this.gvProjects.DataKeys[rowIndex][2].ToString();
                lblProjectNameAllocate.Text = gvProjects.Rows[rowIndex].Cells[0].Text;
                lblProjName.Text = gvProjects.Rows[rowIndex].Cells[0].Text;
                hfProjectId.Value = projectId;
                bindResources(projectId);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToResources", "setTimeout(scrollToResources, 1);", true);
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }

        protected void btnAllocate_Click(object sender, EventArgs e)
        {
            try
            {
                
                DateTime startDate = new DateTime();
                DateTime allocateStartDate = new DateTime();
                DateTime allocateEndDate = new DateTime();
                string Response = "";
                string id = "";
                string resourceEmail = "";
                startDate = DateTime.Parse(hfStartDate.Value);
                try
                {
                    resourceEmail = ddlEmpList.Text;
                }
                catch (FormatException)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('Invalid resource email address!');setTimeout(scrollToAllocate, 1);", true);
                    return;
                }
                try
                {
                    allocateStartDate = DateTime.Parse(txtFromDate.Value);
                    allocateEndDate = DateTime.Parse(txtTodate.Value);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('Invalid allocation dates!');setTimeout(scrollToAllocate, 1);", true);
                    return;
                }
                if (allocateStartDate < startDate)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('Allocation date cant be less than project start date');setTimeout(scrollToAllocate, 1);", true);
                    return;
                }
                if (allocateEndDate < allocateStartDate)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('Allocation end date cant be less than allocation start date');setTimeout(scrollToAllocate, 1);", true);
                    return;
                }
                else
                {                    
                    Response = dbh.ResourceAllocation_INSERT(out id, hfProjectId.Value, resourceEmail, txtFromDate.Value, txtTodate.Value, CommonFunctions.GetUser());
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('" + Response + "');setTimeout(scrollToAllocate, 1);", true);
                    bindResources(hfProjectId.Value);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToResources", "setTimeout(scrollToResources, 1);", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        void bindProjects(string ProjectName)
        {
            try
            {
                
                
                gvProjects.DataSource = dbh.Project_Data_Allocation(ProjectName);
                gvProjects.DataBind();
                if (gvProjects.Rows.Count > 0)
                { gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader; }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        void bindResources(string ProjectID)
        {
            try
            {
                gvResources.DataSource = dbh.ResourceAllocation_GET(ProjectID);
                gvResources.DataBind();
                if (gvResources.Rows.Count > 0)
                { gvResources.HeaderRow.TableSection = TableRowSection.TableHeader; }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        string getLastDateThisYear()
        {
            int month = DateTime.Now.Month;
            string date = "";
            switch (month < 6)
            {
                case true:
                    date = DateTime.Now.Year.ToString() + "-05-31";
                    break;
                case false:
                    date = DateTime.Now.AddYears(1).Year.ToString() + "-05-31";
                    break;
            }
            return date;

        }
        string getStartDayOfTheWeek()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            if (date.DayOfWeek != DayOfWeek.Monday)
            {
                int delta = DayOfWeek.Monday - date.DayOfWeek;
                if (delta > 0)
                    delta -= 7;
                date = date.AddDays(delta);
            }
            return date.ToString("yyyy-MM-dd");

        }
    }
}
