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
    public partial class EPCC3_MF_Approval : System.Web.UI.Page
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


                string userid = CommonFunctions.GetUser();

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                //var WOLogNumber = e.CommandArgument;
                string MFApprover = selectedRow.Cells[2].Text;
                if (e.CommandName == "Approve")
                {
                    UpdateStatusFromDIJV("1", rowIndex, userid);


                    EmailManager em = new EmailManager();

                    DataSet dsWO = dbh.WorkOrder_EPCC3_PDFData_GET_Report(rowIndex);
                    LocalReport lr = new LocalReport();
                    ReportViewerWO.ProcessingMode = ProcessingMode.Local;
                    lr.ReportPath = HttpContext.Current.Server.MapPath("~/Report/EPCC3Report.rdlc");

                    ReportDataSource datasource = new ReportDataSource("DT_TRAT_EPCC3", dsWO.Tables[0]);
                    ReportViewerWO.LocalReport.DataSources.Clear();
                    ReportViewerWO.LocalReport.DataSources.Add(datasource);




                    if (dsWO.Tables[0].Rows[0]["DIJVManagerApproval"].ToString() == "Approved" && dsWO.Tables[0].Rows[0]["EngagementManagerApproval"].ToString() == "Pending")
                    {
                        string templateName = "EPCC3";
                        string Subject = "EPCC 3(" + rowIndex + ") Request For Approval";
                        string mBody = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                               dsWO.Tables[0].Rows[0]["DIJVEngagementManagerEmailId"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                               + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                               Convert.ToString(rowIndex), dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, dsWO.Tables[0].Rows[0]["href"].ToString());
                        em.SendMailWith_MultipleAttachment(dsWO.Tables[0].Rows[0]["DIJVPartnerEmail"].ToString(), "indijvProjects@deloitte.com", Subject, mBody,
                            dsWO.Tables[0].Rows[0]["DIJVManagerEmail"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["CreatorEmail"].ToString(), "", dsWO, ReportViewerWO);
                        MsgBox("EPCC 3 has been approved. A mail has been sent to the Enagagement Partner to approve or reject the EPCC3.");

                    }
                    else if (dsWO.Tables[0].Rows[0]["DIJVManagerApproval"].ToString() == "Approved" && dsWO.Tables[0].Rows[0]["EngagementManagerApproval"].ToString() == "Approved")
                    {
                        string templateName = "EPCC3_FinalApprooved";
                        string Subject = "EPCC 3(" + rowIndex + ") Request is Approved";
                        string mBody = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                               dsWO.Tables[0].Rows[0]["DIJVEngagementManagerEmailId"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                               + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                               Convert.ToString(rowIndex), dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, dsWO.Tables[0].Rows[0]["href"].ToString());
                        em.SendMailWith_MultipleAttachment(dsWO.Tables[0].Rows[0]["CreatorEmail"].ToString(), "indijvProjects@deloitte.com", Subject, mBody,
                            dsWO.Tables[0].Rows[0]["DIJVManagerEmail"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(), "", dsWO, ReportViewerWO);
                        MsgBox("EPCC3 has been approved. A mail has been sent to the Creator");

                    }
                    bindGrid();                    
                }
                else if (e.CommandName == "Reject")
                {
                    UpdateStatusFromDIJV("0", rowIndex, Convert.ToString(userid), MFApprover);
                    MsgBox("EPCC3 has been rejected.");
                    bindGrid();
                }
                else if (e.CommandName == "Reset")
                {
                    ResetStatus(rowIndex);
                    MsgBox("EPCC3 has been reset.");
                    bindGrid();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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


        public void UpdateStatusFromDIJV(string status, int woLogNumber, string DIJVApprover, string memberFirmPartner)
        {
            try
            {

                dbh.EPCC3UpdateStatusFromDIJVApprover(status, woLogNumber, DIJVApprover, 1);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ResetStatus(int woLogNumber)
        {
            try
            {
                int userid = CommonFunctions.GetUserId();
                dbh.ResetStatus_EPCC3(woLogNumber, userid);

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
                    gvWorkOrder.DataSource = dbh.Work_Order_SEARCH_Admin_EPCC3(txtWorkOrderName.Text.Trim(), requestSource);
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

        public void UpdateStatusFromDIJV(string status, int woLogNumber, string DIJVApprover)
        {
            try
            {
                DBHandler dh = new DBHandler();
                dh.EPCC3UpdateStatusFromDIJVApprover(status, woLogNumber, DIJVApprover, 1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}