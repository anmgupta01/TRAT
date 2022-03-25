using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TRAT
{
    public partial class WorkOrderUsers : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnWorkOrder_Click(object sender, EventArgs e)
        {
            lblAlert.InnerText = "";
            alert.Visible = false;
            bindGrid();
        }
        protected void gvWorkOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                int userid = CommonFunctions.GetUserId();

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int rowIndex = int.Parse(e.CommandArgument.ToString());                
                string MFApprover = selectedRow.Cells[2].Text;

                
                int gvrowindex = selectedRow.RowIndex;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow1 = gvWorkOrder.Rows[gvrowindex];                
                CheckBox isChecked = (CheckBox)selectedRow1.FindControl("chkMail");

               

                if (e.CommandName == "Approve")
                {
                    UpdateStatusFromDIJV("1", rowIndex, Convert.ToString(userid), MFApprover);
                    if (isChecked.Checked)
                    {
                        MsgBox("Work order has been approved, Without sending email to Enagagement Partner");
                    }
                    else
                    {
                        
                        EmailManager em = new EmailManager();
                        SaveWorkOrderHoursData(Convert.ToString(userid));
                        DataSet dsWO = dbh.WorkOrder_PDFData_GET_Report(Convert.ToString(rowIndex));
                        LocalReport lr = new LocalReport();
                        ReportViewerWO.ProcessingMode = ProcessingMode.Local;
                        lr.ReportPath = HttpContext.Current.Server.MapPath("~/Report/WorkOrderDetails.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DataSetWorkOrder", dsWO.Tables[0]);
                        ReportViewerWO.LocalReport.DataSources.Clear();
                        ReportViewerWO.LocalReport.DataSources.Add(datasource);

                        string templateName = string.Empty;
                        string Subject = string.Empty;
                        if (dsWO.Tables[0].Rows[0]["WOVersion"].ToString() == "Revise")
                        {
                            templateName = "WorkOrderApproval_IJV_Revised";
                            Subject = "Revised Work Order(" + Convert.ToString(rowIndex) + ") Request For Approval";
                        }
                        else
                        {
                            templateName = "WorkOrderApproval_IJV_Initial";
                            Subject = "Initial Work Order(" + Convert.ToString(rowIndex) + ") Request For Approval";
                        }

                        string mBody = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                                    dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                                    + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                                    Convert.ToString(rowIndex), dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, "");
                        em.SendMailWithAttachment(dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString(), "indijvprojects@deloitte.com", Subject, mBody,
                            dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["Creator"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["MFManagerEmailId"].ToString(), "", null, ReportViewerWO);

                        string mBodyForIJVManager = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                                    dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                                    + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                                   Convert.ToString(rowIndex), dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), "WorkOrderNotification", "");
                        em.SendMailWithAttachment(dsWO.Tables[0].Rows[0]["MFManagerEmailId"].ToString(), "indijvprojects@deloitte.com", "Work Order Request For Approval", mBodyForIJVManager, "", "", null, ReportViewerWO);
                        MsgBox("Work order has been approved. A mail has been sent to the Enagagement Partner to approve or reject the work order.");
                    }
                    bindGrid();                    
                }
                else if (e.CommandName == "Reject")
                {
                    UpdateStatusFromDIJV("0", rowIndex, Convert.ToString(userid), MFApprover);
                    MsgBox("Work order has been rejected.");
                    bindGrid();
                }
                else if (e.CommandName == "Reset")
                {
                    ResetStatus(rowIndex, Convert.ToString(userid));
                    MsgBox("Work order has been reset.");
                    bindGrid();
                }

            }
            catch (Exception ex)
            {

                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        //public void DisablePostApproveorReject()
        //{
        //    DisableFieldsPostSubmit(false);
        //    txtMemberFirmPartnerAppr.Enabled = false;
        //    txtMemberFirmPartnerAppr.ReadOnly = true;

        //    txtADHours.Enabled = false;
        //    txtADHours.ReadOnly = true;
        //    txtAMHours.Enabled = false;
        //    txtAMHours.ReadOnly = true;
        //    txtDMHours.Enabled = false;
        //    txtDMHours.ReadOnly = true;
        //    txtExecutiveHours.Enabled = false;
        //    txtExecutiveHours.ReadOnly = true;
        //    txtExec1.Enabled = false;
        //    txtExec1.ReadOnly = true;
        //    txtManagerHours.Enabled = false;
        //    txtManagerHours.ReadOnly = true;
        //    txtDirectorHours.Enabled = false;
        //    txtDirectorHours.ReadOnly = true;

        //}
        private string PopulateBody(string mailto, string mailCC, string WorkOrderID, string MFPartnerName, string DIJVEngagementManagerName, string projectName, string projectCode, string templateName, string href)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/" + templateName + ".html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{mailto}", mailto);
            body = body.Replace("{mailcc}", mailCC);
            body = body.Replace("{WorkOrderID}", WorkOrderID);
            body = body.Replace("{MFPartnerName}", MFPartnerName);
            body = body.Replace("{DIJVEngagementManagerName}", DIJVEngagementManagerName);
            body = body.Replace("{ProjectName}", projectName);
            body = body.Replace("{ProjectCode}", projectCode);
            body = body.Replace("{Href}", href);
            return body;
        }

        private void MsgBox(string message)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('" + message + "');", true);
        }

        public void SaveWorkOrderHoursData(string userid)
        {
            try
            {

                int UserId = dbh.GetUserId();
                if (userid != "" && userid != null)
                {
                    string workOrderID = dbh.WorkOrderHours_Update(int.Parse(userid), 0, 0, 0, 0, 0, 0, 0);
                    //txtADHours.Text != "" ? decimal.Parse(txtADHours.Text) : 0,
                    //txtAMHours.Text != "" ? decimal.Parse(txtAMHours.Text) : 0,
                    //txtDMHours.Text != "" ? decimal.Parse(txtDMHours.Text) : 0,
                    //txtExecutiveHours.Text != "" ? decimal.Parse(txtExecutiveHours.Text) : 0,
                    //txtExec1.Text != "" ? decimal.Parse(txtExec1.Text) : 0,
                    //txtManagerHours.Text != "" ? decimal.Parse(txtManagerHours.Text) : 0,
                    //txtDirectorHours.Text != "" ? decimal.Parse(txtDirectorHours.Text) : 0);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateStatusFromDIJV(string status, int woLogNumber, string DIJVApprover, string memberFirmPartner)
        {
            try
            {

                dbh.UpdateStatusFromDIJVApprover(status, woLogNumber, DIJVApprover, memberFirmPartner);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ResetStatus(int woLogNumber, string DIJVApprover)
        {
            try
            {

                dbh.ResetStatus(woLogNumber, DIJVApprover);

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

                int requestSource = 2;
                if (txtWorkOrderName.Text.Trim() != "")
                {
                    gvWorkOrder.DataSource = dbh.Work_Order_SEARCH(txtWorkOrderName.Text.Trim(), requestSource);
                    gvWorkOrder.DataBind();
                    if (gvWorkOrder.Rows.Count > 0)
                    { gvWorkOrder.HeaderRow.TableSection = TableRowSection.TableHeader; }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}