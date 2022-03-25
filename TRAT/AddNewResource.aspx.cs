using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class AddNewResource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (CommonFunctions.GetUseRole(CommonFunctions.GetUser()) != 5)
                        Response.Redirect("~/Home.aspx");
                    LoadMasters();
                    if (!string.IsNullOrEmpty(Request.QueryString["EmpID"]))
                    {
                        hidEmpID.Value = (Request.QueryString["EmpID"]);
                        DBHandler dh = new DBHandler();
                        DataTable dt = new DataTable();

                        dt = dh.GetSingleEmployeeDetails(hidEmpID.Value.ToString(), "");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            LoadEmployeeData(dt);
                        }
                    }
                    else
                    {
                        btnAddResource.Text = "Add New Resource";
                    }
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


                DBHandler handler = new DBHandler();
                DataSet ds = new DataSet();
                ds = handler.GetMasters();

                drpDepartment.DataSource = ds.Tables[10];
                drpDepartment.DataTextField = "Name";
                drpDepartment.DataValueField = "Name";
                drpDepartment.DataBind();
                drpDepartment.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAddResource_Click(object sender, EventArgs e)
        {
            try
            {
                DBHandler handler = new DBHandler();
                string UserId = CommonFunctions.GetUser();
                int id = hidEmpID.Value != "" ? int.Parse(hidEmpID.Value) : 0;
                string result = handler.EmployeesManage(id, out id, txtEmpID.Text, txtEmpName.Text,
                    txtEmpEmail.Text, drpDesignation.Text, drpDepartment.SelectedItem.Text, drpSubDepartment.SelectedItem.Text, txtAusEmailID.Text,
                    txtCanEmailId.Text, txtUkEmailId.Text, UserId);
                MsgBox(result);
                //SendMail();
                if (id > 0)
                {
                    ClearFields();
                    Response.Redirect("AddNewResource.aspx?id=Success");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SendMail()
        {
            try
            {

                var fromAddress = new MailAddress("shahriddhi89@gmail.com", "Riddhi Shah");
                var toAddress = new MailAddress("riddhishah@deloitte.com", "Riddhi");
                const string fromPassword = "";
                const string subject = "Subject";
                const string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MsgBox(string message)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + message + "');";
            msg += "</script>";
            Response.Write(msg);
        }
        public void ClearFields()
        {
            txtEmpID.Text = string.Empty;
            txtEmpName.Text = string.Empty;
            txtEmpEmail.Text = string.Empty;

            drpDesignation.SelectedValue = "Select";
            drpDepartment.SelectedValue = "-1";

            txtAusEmailID.Text = string.Empty;
            txtCanEmailId.Text = string.Empty;
            txtUkEmailId.Text = string.Empty;

        }

        public void LoadEmployeeData(DataTable dt)
        {
            try
            {

                EventArgs e1 = new EventArgs();

                txtEmpID.Text = dt.Rows[0]["EmpID"].ToString();
                txtEmpName.Text = dt.Rows[0]["Employee_Name"].ToString();

                txtEmpEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAusEmailID.Text = dt.Rows[0]["Australia_Mail_ID"].ToString();

                txtCanEmailId.Text = dt.Rows[0]["Canada_Mail_ID"].ToString();
                txtUkEmailId.Text = dt.Rows[0]["UK_Mail_ID"].ToString();

                drpDepartment.SelectedValue = dt.Rows[0]["Department_Name"].ToString();

                drpDepartment_SelectedIndexChanged(drpDepartment.Items, e1);
                drpSubDepartment.SelectedValue = dt.Rows[0]["SubDepartment_Name"].ToString();

                drpDesignation.SelectedValue = dt.Rows[0]["Designation"].ToString();
                btnAddResource.Text = "Update Details";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                if (drpDepartment.SelectedValue != "-1")
                {
                    dt = new DBHandler().GetSubDepartment(0, drpDepartment.SelectedValue);

                    drpSubDepartment.DataSource = dt;
                    drpSubDepartment.DataTextField = "Name";
                    drpSubDepartment.DataValueField = "Name";
                    drpSubDepartment.DataBind();

                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}