using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static TRAT.DBHandler;

namespace TRAT
{
    public partial class EPCC3 : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        public bool TextWasChanged = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunctions.ValidateEmail(CommonFunctions.GetUser()) == false)
                {
                    Response.Redirect("~/Home.aspx?id=Success");
                }

                txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
                if (!IsPostBack)
                {

                    LoadMasters();
                    bindGrid();
                    
                    SwitchCasesBasedOnStatus(0);
                    if (Request.QueryString["projD"] != null && Request.QueryString["woNumber"] != null && Request.QueryString["approverD"] != null)
                    {
                        LoadWorkOrderFromEmailLink(HttpContext.Current.Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        divWorkOrder.Visible = false;
                    }
                    DocumentBindGrid();
                }
                else
                {
                    bindGrid();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region InitialLoad
        public void LoadMasters()
        {
            try
            {
                DBHandler dh = new DBHandler();
                DataSet dsMasters = new DataSet();
                dsMasters = dh.GetMastersEmployeeSrMngrDirector();
                drpDIJVPartner.DataSource = dsMasters.Tables[0];
                drpDIJVPartner.DataTextField = "Employee_Name";
                drpDIJVPartner.DataValueField = "Id";
                drpDIJVPartner.DataBind();
                drpDIJVMngr.DataSource = dsMasters.Tables[1];
                drpDIJVMngr.DataTextField = "Employee_Name";
                drpDIJVMngr.DataValueField = "Id";
                drpDIJVMngr.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadProjectData(DataSet ds)
        {
            try
            {
                string userid = CommonFunctions.GetUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionType.Text = ds.Tables[0].Rows[0]["NatureOfWorkName"].ToString();
                    txtProjectNameEPCC.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtMFEngagedWith.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                    txtInitialWO.Text = ds.Tables[0].Rows[0]["ProjectCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetDesignations(int projectID)
        {
            try
            {
                DBHandler dh = new DBHandler();
                DataTable dt = new DataTable();
                dt = dh.GetWorkOrderDesignations(projectID).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadWorkOrderData(DataSet ds)
        {
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hidWOID.Value = ds.Tables[0].Rows[0]["ProjectID"].ToString();
                    hidEPCC3ID.Value = ds.Tables[0].Rows[0]["EPCC3_ID"].ToString();
                    txtTransactionType.Text = ds.Tables[0].Rows[0]["TransactionType"].ToString();
                    txtProjectNameEPCC.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    txtMFEngagedWith.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                    txtInitialWO.Text = ds.Tables[0].Rows[0]["ProjectCode"].ToString();

                    drpFinalHours.SelectedValue = ds.Tables[0].Rows[0]["FinalHours"].ToString();
                    drpFinalWO.SelectedValue = ds.Tables[0].Rows[0]["FinalWO"].ToString();
                    drpCanadianReview.SelectedValue = ds.Tables[0].Rows[0]["CanadianReview"].ToString();
                    drpGermanInsider.SelectedValue = ds.Tables[0].Rows[0]["GermanInsider"].ToString();
                    drpTRATwo.SelectedValue = ds.Tables[0].Rows[0]["WorkOrderUpdated"].ToString();

                    txtNote.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                    txtConflicts.Text = ds.Tables[0].Rows[0]["Conflicts"].ToString();

                    drpDIJVConflict.SelectedValue = ds.Tables[0].Rows[0]["ConflictCheck"].ToString();
                    drpAllDocument.SelectedValue = ds.Tables[0].Rows[0]["AllDocument"].ToString();
                    drpNDA.SelectedValue = ds.Tables[0].Rows[0]["NDA"].ToString();
                    
                    drpDIJVMngr.SelectedValue = ds.Tables[0].Rows[0]["DIJVManager"].ToString();
                    drpDIJVPartner.SelectedValue = ds.Tables[0].Rows[0]["EngagementManager"].ToString();

                    dtAppOrReject.Text = ds.Tables[0].Rows[0]["DIJVManagerApprDate"].ToString();
                    dtPartnerApprRej.Text = ds.Tables[0].Rows[0]["EngagementManagerApprDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SwitchCasesBasedOnStatus(int fromStatus)
        {
            try
            {
                switch (fromStatus)
                {
                    case 0: // not ssaved , empty total
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;


                        break;
                    case 1: // saved, not submitted
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;

                        valPartnerEmail.Enabled = false;
                        break;
                    case 2:// submitted
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;

                        break;
                    case 3: // approved level 1
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;

                        break;
                    case 4: // approved level 2
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;

                        break;
                    case -3: // rejected level 1
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;

                        break;
                    case -4: // rejected level 2
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        public void DisableFieldsPostSubmit(bool isApprover)
        {
            try
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;

                if (isApprover)
                {
                    btnSubmit.Visible = false;
                    btnSave.Visible = false;
                    valPartnerEmail.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ClearWOFieldsOnly()
        {
            try
            {

                dtAppOrReject.Text = string.Empty;
                dtPartnerApprRej.Text = string.Empty;
                DataSet dsMasters = new DataSet();
                DBHandler dh = new DBHandler();
                dsMasters = dh.GetMastersEmployeeSrMngrDirector();
                drpDIJVPartner.DataSource = dsMasters.Tables[0];
                drpDIJVPartner.DataTextField = "Employee_Name";
                drpDIJVPartner.DataValueField = "Id";
                drpDIJVPartner.DataBind();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ClearAnyPreviousDataofWorkOrder()
        {
            hidWOID.Value = "";
            hidEPCC3ID.Value = "";
            ClearWOFieldsOnly();

        }
        public void EnableOnlyReviseWOFields()
        {
            drpDIJVPartner.Enabled = true;
        }
        #endregion
        #region Bindings
        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            try
            {

                DBHandler dh = new DBHandler();
                string userid = CommonFunctions.GetUser();

                bindGrid();
                if (TextWasChanged)
                {
                    divWorkOrder.Visible = false;

                    ClearAnyPreviousDataofWorkOrder();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {

            TextWasChanged = true;
        }
        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                DBHandler dh = new DBHandler();
                string UserId = CommonFunctions.GetUser();
                hidProjectID.Value = (e.CommandArgument.ToString()).ToString();
                DataSet ds = new DataSet();
                DataSet dsWO = new DataSet();
                DataSet dsWODetails = new DataSet();
                hidWOID.Value = Convert.ToString(e.CommandArgument);
                ds = dh.GetProjectData(int.Parse(e.CommandArgument.ToString()));

                dsWO = (dh.GetWorkOrder_EPCC3_Data(int.Parse(e.CommandArgument.ToString())));
                string memberFirmID = dh.GetEmployeeCountryMailID(Convert.ToInt32(ds.Tables[0].Rows[0]["IJVManager"]), Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]));
                hidMemberFirmID.Value = memberFirmID;
                GetDesignations(int.Parse(e.CommandArgument.ToString()));
                hidWONumber.Value = ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString();
                
                if (e.CommandName == "edt")
                {
                    if (dsWO.Tables[0].Rows.Count > 0)
                    {
                        dsWODetails = dh.WorkOrder_EPCC3_PDFData_GET_Report(Convert.ToInt32(dsWO.Tables[0].Rows[0]["ProjectID"]));
                    }
                    divWorkOrder.Visible = true;

                    DataTable dsEmployee = new DataTable();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());


                    LoadWorkOrderData(dsWO);
                    LoadProjectData(ds);
                    DocumentBindGrid();
                    dsEmployee = dh.GetSingleEmployeeDetails(drpDIJVPartner.SelectedValue, "");
                    string statusDIJVPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["DIJVManagerApproval"]) ? (dsWO.Tables[0].Rows[0]["DIJVManagerApproval"]).ToString() : null;
                    if (statusDIJVPartner == "True") //DIJV Partner Approved
                    {
                        txtStatusDIJV.Text = "Approved";
                    }
                    else if (statusDIJVPartner == "False")
                    {
                        txtStatusDIJV.Text = "Rejected";
                    }
                    else
                    {
                        txtStatusDIJV.Text = string.Empty;
                    }

                    string statusMFEngagementPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["EngagementManagerApproval"]) ? (dsWO.Tables[0].Rows[0]["EngagementManagerApproval"]).ToString() : null;
                    if (statusMFEngagementPartner == "True")
                    {

                        //SwitchCasesBasedOnStatus(-4);
                        txtStatusPartner.Text = "Approved";
                    }
                    else if (statusMFEngagementPartner == "False") // Approver: MFEngagementPartner Status: Rejected
                    {
                        txtStatusPartner.Text = "Rejected";
                    }
                    else
                    {
                        txtStatusPartner.Text = string.Empty;
                    }

                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Enabled = true;

                    btnDIJVAppr.Visible = false;
                    btnDIJVReject.Visible = false;

                    if ((Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsSubmitted"])) == false)
                    {
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                        drpDIJVMngr.Enabled = true;
                        drpDIJVPartner.Enabled = true;
                        FileUploadDiv.Visible = true;
                    }
                    else if ((Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsSubmitted"])) == true)
                    {
                        drpDIJVMngr.Enabled = false;
                        drpDIJVPartner.Enabled = false;
                        btnSave.Visible = false;
                        btnSave.Enabled = false;
                        btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;
                        FileUploadDiv.Visible = false;
                        if ((txtStatusDIJV.Text) == "" || (txtStatusDIJV.Text) == string.Empty)
                        {
                            if ((dsWODetails.Tables[0].Rows[0]["DIJVManagerEmail"].ToString()) == UserId)
                            {
                                btnDIJVAppr.Visible = true;
                                btnDIJVReject.Visible = true;
                            }
                        }

                        if (((txtStatusPartner.Text) == "" || (txtStatusPartner.Text) == string.Empty) && txtStatusDIJV.Text == "Approved")
                        {
                            if ((dsWODetails.Tables[0].Rows[0]["DIJVPartnerEmail"].ToString()) == UserId)
                            {
                                btnDIJVAppr.Visible = true;
                                btnDIJVReject.Visible = true;
                            }
                        }
                    }

                  
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToWorkOrder", "setTimeout(scrollToWorkOrder, 1);", true);
                }

                if (e.CommandName == "GenerateWO")
                {
                    //GetDesignations(int.Parse(e.CommandArgument.ToString()));
                    divWorkOrder.Visible = true;
                    LoadProjectData(ds);


                    ClearWOFieldsOnly();
                    btnDIJVAppr.Visible = false;
                    btnDIJVReject.Visible = false;

                    if (drpDIJVPartner.Items.FindByValue(ds.Tables[0].Rows[0]["IJVPartner"].ToString()) != null)
                    {
                        drpDIJVPartner.SelectedValue = ds.Tables[0].Rows[0]["IJVPartner"].ToString();
                        SwitchCasesBasedOnStatus(1);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToWorkOrder", "setTimeout(scrollToWorkOrder, 1);", true);
                        divWorkOrder.Visible = true;
                        lblWorkOrderError.Visible = false;
                    }
                    else
                    {
                        lblWorkOrderError.InnerHtml = "Invalid DIJV-Engagement Partner entered while creating the project!";
                        divWorkOrder.Visible = false;
                        lblWorkOrderError.Visible = true;
                    }

                }
                if (e.CommandName == "Revise")
                {
                    divWorkOrder.Visible = true;
                    txtStatusPartner.Text = "Approved";
                    txtStatusDIJV.Text = "Approved";
                    LoadWorkOrderData(dsWO);
                    LoadProjectData(ds);
                    SwitchCasesBasedOnStatus(1);
                    EnableOnlyReviseWOFields();
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
                    gvProjects.DataSource = dbh.Project_Data_SEARCH_RoleBased_EPCC3(txtProjectName.Text, CommonFunctions.GetUser());
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
        #endregion
        #region Click Event
        private void MsgBox(string message)
        {


            ScriptManager.RegisterStartupScript(Page, typeof(Page), "wrongDateAlert", "alert('" + message + "');", true);

        }
        public void SaveData(string isSubmit, out string href)
        {
            href = "";
            try
            {
                DBHandler handler = new DBHandler();
                string UserId = CommonFunctions.GetUser();

                string message1 = "";
                string wo_number = "";
                //string href = "";
                if (hidEPCC3ID.Value != "" && hidEPCC3ID.Value != null)
                {
                    string workOrderID = handler.WorkOrder_EPCC3_Update(int.Parse(hidEPCC3ID.Value), int.Parse(hidProjectID.Value), drpFinalHours.SelectedValue,
                        drpFinalWO.SelectedValue, drpCanadianReview.SelectedValue, drpGermanInsider.SelectedValue, drpTRATwo.SelectedValue, txtNote.Text.Trim(),
                        txtConflicts.Text.Trim(), drpDIJVConflict.SelectedValue, drpAllDocument.SelectedValue, drpNDA.SelectedValue,
                        drpDIJVMngr.Text, drpDIJVPartner.Text, UserId, isSubmit,
                        out message1, out href);                    
                    if (workOrderID != "-1")
                    {
                        if (isSubmit == "1")
                        {
                            DisableFieldsPostSubmit(false);
                            MsgBox("EPCC3 successfully submitted for approval");
                        }
                        else
                        {
                            MsgBox("Update successful");

                        }
                    }
                    else
                    {
                        MsgBox(message1);

                    }

                    //href = "";
                }
                else
                {
                    string workOrderID = handler.WorkOrder_EPCC3_Insert(int.Parse(hidProjectID.Value), drpFinalHours.SelectedValue,
                        drpFinalWO.SelectedValue, drpCanadianReview.SelectedValue, drpGermanInsider.SelectedValue, drpTRATwo.SelectedValue, txtNote.Text.Trim(),
                        txtConflicts.Text.Trim(),drpDIJVConflict.SelectedValue, drpAllDocument.SelectedValue, drpNDA.SelectedValue,
                        drpDIJVMngr.Text, drpDIJVPartner.Text, UserId, isSubmit,
                        out message1, out wo_number, out href);

                    if (workOrderID != "-1")
                    {
                        MsgBox("EPCC 3 has been successfully saved.");
                        hidWOID.Value = workOrderID.ToString();
                        bindGrid();
                        if (isSubmit == "1")
                        {
                            MsgBox("EPCC 3 Successfully Submitted for Approval");
                            DisableFieldsPostSubmit(false);
                        }
                    }
                    else
                    {
                        MsgBox(message1);
                    }
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
                dh.EPCC3UpdateStatusFromDIJVApprover(status, woLogNumber, DIJVApprover, 0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                string href = "";
                string href_WO = "";
                SaveData("0", out href);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                btnSubmit.Enabled = false;
                string UserId = CommonFunctions.GetUser();
                string href = "";
                string href_WO = "";

                SaveData("1", out href);
                if (href != null && href != "")
                {
                    EmailManager em = new EmailManager();
                    DataSet dsWO = dbh.WorkOrder_EPCC3_PDFData_GET_Report(int.Parse(hidProjectID.Value));
                    LocalReport lr = new LocalReport();
                    ReportViewerWO.ProcessingMode = ProcessingMode.Local;
                    lr.ReportPath = HttpContext.Current.Server.MapPath("~/Report/EPCC3Report.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DT_TRAT_EPCC3", dsWO.Tables[0]);
                    ReportViewerWO.LocalReport.DataSources.Clear();
                    ReportViewerWO.LocalReport.DataSources.Add(datasource);
                    string WOLogNumber = dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString();

                    string templateName = "EPCC3";
                    string Subject = "EPCC 3(" + hidWOID.Value + ") Request For Approval";

                    string mBodyForIJVManager = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVManagerEmail"].ToString(),
                                                UserId, hidWOID.Value, dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, href);

                    em.SendMailWith_MultipleAttachment(dsWO.Tables[0].Rows[0]["DIJVManagerEmail"].ToString(), "indijvProjects@deloitte.com", Subject, mBodyForIJVManager, UserId, "", dsWO, ReportViewerWO);
                }
                MsgBox("EPCC3 has been submitted successfully. A mail has been sent to the DIJV Manager to approve or reject the EPCC3.");

                dtAppOrReject.Text = string.Empty;
                txtStatusDIJV.Text = string.Empty;
                dtPartnerApprRej.Text = string.Empty;
                txtStatusPartner.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnDIJVAppr_Click(object sender, EventArgs e)
        {
            try
            {
                btnDIJVAppr.Enabled = false;
                string userid = CommonFunctions.GetUser();
                UpdateStatusFromDIJV("1", int.Parse(hidWOID.Value), userid);
                txtStatusDIJV.Text = "Approved";
                dtAppOrReject.Text = DateTime.UtcNow.ToString();


                btnDIJVAppr.Visible = false;
                btnDIJVReject.Visible = false;

                valPartnerEmail.Enabled = false;
                EmailManager em = new EmailManager();

                DataSet dsWO = dbh.WorkOrder_EPCC3_PDFData_GET_Report(int.Parse(hidWOID.Value));
                LocalReport lr = new LocalReport();
                ReportViewerWO.ProcessingMode = ProcessingMode.Local;
                lr.ReportPath = HttpContext.Current.Server.MapPath("~/Report/EPCC3Report.rdlc");

                ReportDataSource datasource = new ReportDataSource("DT_TRAT_EPCC3", dsWO.Tables[0]);
                ReportViewerWO.LocalReport.DataSources.Clear();
                ReportViewerWO.LocalReport.DataSources.Add(datasource);



                if (dsWO.Tables[0].Rows[0]["DIJVManagerApproval"].ToString() == "Approved" && dsWO.Tables[0].Rows[0]["EngagementManagerApproval"].ToString() == "Pending")
                {
                    string templateName = "EPCC3";
                    string Subject = "EPCC 3(" + hidWOID.Value + ") Request For Approval";
                    string mBody = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                           dsWO.Tables[0].Rows[0]["DIJVEngagementManagerEmailId"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                           + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                           hidWOID.Value, dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, dsWO.Tables[0].Rows[0]["href"].ToString());
                    em.SendMailWith_MultipleAttachment(dsWO.Tables[0].Rows[0]["DIJVPartnerEmail"].ToString(), "indijvProjects@deloitte.com", Subject, mBody,
                        dsWO.Tables[0].Rows[0]["DIJVManagerEmail"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["CreatorEmail"].ToString(), "", dsWO, ReportViewerWO);
                    MsgBox("EPCC 3 has been approved. A mail has been sent to the Enagagement Partner to approve or reject the EPCC3.");

                }
                else if (dsWO.Tables[0].Rows[0]["DIJVManagerApproval"].ToString() == "Approved" && dsWO.Tables[0].Rows[0]["EngagementManagerApproval"].ToString() == "Approved")
                {
                    string templateName = "EPCC3_FinalApprooved";
                    string Subject = "EPCC 3(" + hidWOID.Value + ") Request Is Approved";
                    txtStatusPartner.Text = "Approved";
                    dtPartnerApprRej.Text = DateTime.UtcNow.ToString();
                    string mBody = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                           dsWO.Tables[0].Rows[0]["DIJVEngagementManagerEmailId"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                           + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                           hidWOID.Value, dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, dsWO.Tables[0].Rows[0]["href"].ToString());
                    em.SendMailWith_MultipleAttachment(dsWO.Tables[0].Rows[0]["CreatorEmail"].ToString(), "indijvProjects@deloitte.com", Subject, mBody,
                        dsWO.Tables[0].Rows[0]["DIJVManagerEmail"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(), "", dsWO, ReportViewerWO);
                    MsgBox("EPCC3 has been approved. A mail has been sent to the Creator");
                }
                bindGrid();

                SwitchCasesBasedOnStatus(3);

            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        protected void btnDIJVReject_Click(object sender, EventArgs e)
        {
            try
            {
                btnDIJVReject.Enabled = false;
                UpdateStatusFromDIJV("0", int.Parse(hidWOID.Value), drpDIJVPartner.SelectedValue.ToString());
                txtStatusDIJV.Text = "Rejected";

                valPartnerEmail.Enabled = false;

                MsgBox("EPCC 3 has been rejected");

                SwitchCasesBasedOnStatus(-3);
                dtAppOrReject.Text = DateTime.UtcNow.ToString();
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        #endregion
        #region "Approve/Reject from DIJV Engagement Partner Email Link"
        public void LoadWorkOrderFromEmailLink(string url)
        {
            try
            {


                Uri currentURL = new Uri(url);
                byte[] dataProjDetails = Convert.FromBase64String(HttpUtility.ParseQueryString(currentURL.Query).Get("projD").ToString());
                string decodedStringProjDeatils = Encoding.UTF8.GetString(dataProjDetails);

                byte[] dataWONumber = Convert.FromBase64String(HttpUtility.ParseQueryString(currentURL.Query).Get("WONumber").ToString());
                string decodedStringWONumber = Encoding.UTF8.GetString(dataWONumber);

                byte[] dataapproverDetails = Convert.FromBase64String(HttpUtility.ParseQueryString(currentURL.Query).Get("approverD").ToString());
                string decodedStringApproverDetails = Encoding.UTF8.GetString(dataapproverDetails);

                string projectId = HttpUtility.ParseQueryString(decodedStringProjDeatils).Get("projectId");
                string createdBy = HttpUtility.ParseQueryString(decodedStringProjDeatils).Get("createdBy");
                string WONumber = decodedStringWONumber;
                string countryId = HttpUtility.ParseQueryString(decodedStringApproverDetails).Get("countryId");
                string DIJVPartner = HttpUtility.ParseQueryString(decodedStringApproverDetails).Get("dijvPartner");

                string UserId = CommonFunctions.GetUser();
                DBHandler dh = new DBHandler();

                hidProjectID.Value = (projectId).ToString();
                DataSet ds = new DataSet();
                DataSet dsWO = new DataSet();
                DataSet dsWODetails = new DataSet();
                EmailManager em = new EmailManager();

                ds = dh.GetProjectData(int.Parse(projectId));
                dsWO = (dh.GetWorkOrder_EPCC3_Data(int.Parse(projectId)));
                string memberFirmID = dh.GetEmployeeCountryMailID(Convert.ToInt32(createdBy), Convert.ToInt32(countryId));
                hidMemberFirmID.Value = memberFirmID;
                GetDesignations(int.Parse(projectId));
                hidWONumber.Value = WONumber;
                if (dsWO.Tables[0].Rows.Count > 0)
                {
                    dsWODetails = dh.WorkOrder_EPCC3_PDFData_GET_Report(Convert.ToInt32(dsWO.Tables[0].Rows[0]["ProjectId"]));
                }
                divWorkOrder.Visible = true;

                DataTable dsEmployee = new DataTable();

                LoadWorkOrderData(dsWO);
                LoadProjectData(ds);
                dsEmployee = dh.GetSingleEmployeeDetails(drpDIJVPartner.SelectedValue, "");
                string statusDIJVPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["DIJVManagerApproval"]) ? (dsWO.Tables[0].Rows[0]["DIJVManagerApproval"]).ToString() : null;
                if (statusDIJVPartner == "True") //DIJV Partner Approved
                {
                    txtStatusDIJV.Text = "Approved";
                }
                else if (statusDIJVPartner == "False")
                {
                    txtStatusDIJV.Text = "Rejected";
                }
                else
                {
                    txtStatusDIJV.Text = string.Empty;
                }

                string statusMFEngagementPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["EngagementManagerApproval"]) ? (dsWO.Tables[0].Rows[0]["EngagementManagerApproval"]).ToString() : null;
                if (statusMFEngagementPartner == "True")
                {

                    //SwitchCasesBasedOnStatus(-4);
                    txtStatusPartner.Text = "Approved";
                }
                else if (statusMFEngagementPartner == "False") // Approver: MFEngagementPartner Status: Rejected
                {
                    txtStatusPartner.Text = "Rejected";
                }
                else
                {
                    txtStatusPartner.Text = string.Empty;
                }

                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;

                btnDIJVAppr.Visible = false;
                btnDIJVReject.Visible = false;

                if ((Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsSubmitted"])) == false)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Enabled = true;
                    drpDIJVMngr.Enabled = true;
                    drpDIJVPartner.Enabled = true;
                }
                else if ((Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsSubmitted"])) == true)
                {
                    drpDIJVMngr.Enabled = false;
                    drpDIJVPartner.Enabled = false;
                    btnSave.Visible = false;
                    btnSave.Enabled = false;
                    btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;

                    if ((txtStatusDIJV.Text) == "" || (txtStatusDIJV.Text) == string.Empty)
                    {
                        if ((dsWODetails.Tables[0].Rows[0]["DIJVManagerEmail"].ToString()) == UserId)
                        {
                            btnDIJVAppr.Visible = true;
                            btnDIJVReject.Visible = true;
                        }
                    }

                    if (((txtStatusPartner.Text) == "" || (txtStatusPartner.Text) == string.Empty) && txtStatusDIJV.Text == "Approved")
                    {
                        if ((dsWODetails.Tables[0].Rows[0]["DIJVPartnerEmail"].ToString()) == UserId)
                        {
                            btnDIJVAppr.Visible = true;
                            btnDIJVReject.Visible = true;
                        }
                    }
                }

                if (statusDIJVPartner != "True" && !Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsSubmitted"])
                    && CommonFunctions.CheckUserAccessOfWorkOrderEdit(CommonFunctions.GetUser().ToLower(), Convert.ToInt32(dsWO.Tables[0].Rows[0]["ProjectID"])))
                {
                    SwitchCasesBasedOnStatus(1);
                }


                if (statusMFEngagementPartner == "False") // MF Pertner Rejected
                {
                    SwitchCasesBasedOnStatus(1);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToWorkOrder", "setTimeout(scrollToWorkOrder, 1);", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion       
        #region "Body HTML"
        public string ApprovalEmailBody()
        {
            string emailBody = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'><HTML> <HEAD> <TITLE></TITLE> <META http-equiv=Content-Type content='text/html; charset=utf-8'> <META content='MSHTML 6.00.6001.18319' name=GENERATOR> </META> </HEAD> <BODY bgColor=#ffffff> <P class=one style='WIDTH: 444px; HEIGHT: 257px'><BR> <TABLE name='ApprovalInfo'> <COLGROUP width=25></COLGROUP> <TBODY> <TR> <TD></TD> <TH><SPAN style='FONT-FAMILY: Arial Narrow'> Approval Information:</SPAN> </TH> </TR> <TR> <TD></TD> <TD><SPAN style='FONT-FAMILY: Arial Narrow'> <INPUT type=radio CHECKED name={/pd:myFields/pd:Approved}> Approved</INPUT> </SPAN> </TD> </TR> <TR> <TD></TD> <TD><SPAN style='FONT-FAMILY: Arial Narrow'> <INPUT type=radio name={/pd:myFields/pd:Approved}> Rejected</SPAN> </TD> </TR> <TR> <TD></TD> <TD><SPAN style='FONT-FAMILY: Arial Narrow'>Remarks: </SPAN><INPUT style='WIDTH: 250px' size=25 value='Test Remark' name={/pd:myFields/pd:Remarks}> </TD> </TR> <TR> <TD></TD> <TD><SPAN style='FONT-FAMILY: Arial Narrow'>Please don not remove or edit the information below this line. </SPAN> </TD> </TR> <TR> <TD></TD> <TD> <SPAN style = 'FONT-FAMILY: Arial Narrow'> <HR> </SPAN> </TD> </TR> <TR> <TD></TD> <TD><SPAN style = 'FONT-FAMILY: Arial Narrow'> TaskId: ${ TaskID}</SPAN> </TD> </TR> <TR></TR> <TR></TR> </TBODY> </TABLE> </P> </BODY></HTML> ";
            return emailBody;
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
        #endregion

        protected void Upload(object sender, EventArgs e)
        {
            try
            {

                DBHandler handler = new DBHandler();
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string contentType = FileUpload1.PostedFile.ContentType;
                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string Result = "";
                handler.UploadEPCCFile_EPCC3(filename, contentType, bytes, int.Parse(hidProjectID.Value), out Result);
                DocumentBindGrid();
                MsgBox(Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                DBHandler handler = new DBHandler();

                int id = int.Parse((sender as LinkButton).CommandArgument);
                handler.DeleteFile_EPCC3(id, int.Parse(hidProjectID.Value), out message);
                DocumentBindGrid();
                MsgBox(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {

            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            DBHandler dh = new DBHandler();
            DataSet ds = new DataSet();
            ds = dh.DownloadFile_EPCC3(id);
            bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
            contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
            fileName = ds.Tables[0].Rows[0]["Name"].ToString();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        private void DocumentBindGrid()
        {
            try
            {
                if ((hidProjectID.Value) != "")
                {
                    GridView1.DataSource = dbh.GetFileData_EPCC3(int.Parse(hidProjectID.Value));
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}