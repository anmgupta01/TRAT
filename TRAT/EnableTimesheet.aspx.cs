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
    public partial class EnableTimesheet : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    var intRole = CommonFunctions.GetUseRole(CommonFunctions.GetUser());
                    //if (intRole != 5 && intRole != 2)
                    if (intRole != 5)
                        Response.Redirect("~/Home.aspx", true);
                    else
                        GetEmployeeEmailIds();
                    //drpProjects.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void GetEmployeeEmailIds()
        {
            try
            {


                var ds = new DBHandler().GetMasters();
                ds.Tables[7].DefaultView.Sort = "Employee_Name ASC";
                ds.Tables[7].Columns.Add(new DataColumn("Title", Type.GetType("System.String"), "Employee_Name + ' - ' + Email"));

                drpEMail.DataSource = ds.Tables[7];
                drpEMail.DataTextField = "Title";
                drpEMail.DataValueField = "Id";
                drpEMail.DataBind();
                drpEMail.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime startDate = new DateTime();
                DateTime EnableStartDate = new DateTime();
                DateTime EnableEndDate = new DateTime();
                string Response = "";
                string id = "";
                string resourceEmail = "";
                string txtResourceMail = drpEMail.SelectedValue; //txtResourceName.Text;
                if (txtResourceMail.Trim() == "")
                {
                    txtResourceMail = "x";
                }
                //startDate = DateTime.Parse(hfStartDate.Value);
                //try
                //{
                //    resourceEmail = new MailAddress(txtResourceMail).Address;
                //}
                //catch (FormatException)
                //{
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('Invalid resource email address!');;", true);
                //    return;
                //}
                try
                {
                    EnableStartDate = DateTime.Parse(txtFromDate.Value);
                    EnableEndDate = DateTime.Parse(txtTodate.Value);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('Invalid dates!');", true);
                    return;
                }

                if (EnableEndDate < EnableStartDate)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('End date cant be less than start date');", true);
                    return;
                }
                else
                {
                    int employeeId, projectId;
                    if (int.TryParse(drpEMail.SelectedValue, out employeeId) && int.TryParse(drpProjects.SelectedValue, out projectId))
                    {
                        Response = dbh.EnableTimesheet_New(UserId: employeeId, ProjectId: projectId, FromDate: txtFromDate.Value, ToDate: txtTodate.Value, CreatedBy: CommonFunctions.GetUser());
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('" + Response + "');", true);
                    }
                    return;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpEMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                int employeeId;
                if (int.TryParse(drpEMail.SelectedValue, out employeeId))
                {
                    var dtProjectList = new DBHandler().GetActiveProjectsForEmployee(employeeId: employeeId);
                    drpProjects.DataSource = dtProjectList;
                    drpProjects.DataTextField = "ProjectCode";
                    drpProjects.DataValueField = "Id";
                    drpProjects.DataBind();
                    drpProjects.Items.Insert(0, new ListItem("--Select--", "-1"));

                    //drpProjects.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}