using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

namespace TRAT
{
    public partial class WorkOrder : System.Web.UI.Page
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
                    btnUpdate.Visible = false;
                    LoadMasters();
                    SwitchCasesBasedOnStatus(0);
                    if (Request.QueryString["projD"] != null && Request.QueryString["woNumber"] != null && Request.QueryString["approverD"] != null)
                    {
                        LoadWorkOrderFromEmailLink(HttpContext.Current.Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        divWorkOrder.Visible = false;
                    }
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

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtConflictCheck.Text = ds.Tables[0].Rows[0]["ConflictCheckNumber"].ToString();
                    txtWONumber.Text = ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString();
                    txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectCode"].ToString();
                    txtEngmntName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtJobCode.Text = ds.Tables[0].Rows[0]["EngagementCode"].ToString();
                    dtStartDate.Value = ds.Tables[0].Rows[0]["startDt"].ToString();
                    txtDIJVPartner.Text = ds.Tables[0].Rows[0]["IJVPartnerEmail"].ToString();
                    txtEngmntManager.Text = ds.Tables[0].Rows[0]["FirmManager"].ToString();
                    txtEngmntPartner.Text = ds.Tables[0].Rows[0]["FirmPartner"].ToString();
                    txtEngmntType.Text = ds.Tables[0].Rows[0]["NatureOfWorkName"].ToString();
                    txtMemberFirm.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                    txtRefOffice.Text = ds.Tables[0].Rows[0]["OfficeName"].ToString();
                    txtMemberFirmPartnerAppr.Text = ds.Tables[0].Rows[0]["FirmPartner"].ToString();
                    //dtEndDt.Value = ds.Tables[0].Rows[0]["endDt"].ToString();

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
                lblDirector.Text = dt.Rows[5]["Designation"].ToString();
                lblAD.Text = dt.Rows[0]["Designation"].ToString();
                lblManager.Text = dt.Rows[1]["Designation"].ToString();
                lblDM.Text = dt.Rows[2]["Designation"].ToString();
                lblAM.Text = dt.Rows[3]["Designation"].ToString();
                lblExec.Text = dt.Rows[4]["Designation"].ToString();
                lblExec1.Text = dt.Rows[6]["Designation"].ToString();

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
                    hidWOID.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                    txtWONumber.Text = ds.Tables[0].Rows[0]["WOLogNumber"].ToString();
                    txtClientName.Text = ds.Tables[0].Rows[0]["ClientName"].ToString();

                    dtEndDt.Value = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString()).ToString("yyyy-MM-dd");
                    //chkCC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MFConflictCheck"]);
                    //chkCCCompleted.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DIJVConflictCheck"]);
                    //chkDRMS.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isDRMSForwarded"]);
                    //chkDataSharing.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DataConfirmedByMF"]);
                    txtOverview.Text = ds.Tables[0].Rows[0]["EngagementOverview"].ToString();
                    txtServicesRequired.Text = ds.Tables[0].Rows[0]["ServicesOverview"].ToString();
                    txtSharedDrive.Text = ds.Tables[0].Rows[0]["SharedDrivePath"].ToString();
                    txtContact2.Text = ds.Tables[0].Rows[0]["DIJVContact2"].ToString();
                    txtContact3.Text = ds.Tables[0].Rows[0]["DIJVContact3"].ToString();
                    drpDIJVPartner.SelectedValue = ds.Tables[0].Rows[0]["DIJVApprover"].ToString();
                    dtAppOrReject.Text = !Convert.IsDBNull(ds.Tables[0].Rows[0]["DIJVApprovalDate"].ToString()) ? ds.Tables[0].Rows[0]["DIJVApprovalDate"].ToString() : "";
                    txtMemberFirmPartnerAppr.Text = !Convert.IsDBNull(ds.Tables[0].Rows[0]["MFApprover"].ToString()) ? ds.Tables[0].Rows[0]["MFApprover"].ToString() : "";
                    dtPartnerApprRej.Text = !Convert.IsDBNull(ds.Tables[0].Rows[0]["MFApprovalDate"].ToString()) ? ds.Tables[0].Rows[0]["MFApprovalDate"].ToString() : "";

                    dtMFConflictCheck.Value = ds.Tables[0].Rows[0]["MFConflictCheckDate"].ToString() != "" ?
                                              DateTime.Parse(ds.Tables[0].Rows[0]["MFConflictCheckDate"].ToString()).ToString("yyyy-MM-dd") : dtMFConflictCheck.Value = "";
                    dtDIJVSubmitted.Value = ds.Tables[0].Rows[0]["isDRMSForwardedDate"].ToString() != "" ?
                                    DateTime.Parse(ds.Tables[0].Rows[0]["isDRMSForwardedDate"].ToString()).ToString("yyyy-MM-dd") : dtDIJVSubmitted.Value = "";
                    dtCCCompleted.Value = ds.Tables[0].Rows[0]["DIJVConflictCheckDate"].ToString() != "" ?
                                          DateTime.Parse(ds.Tables[0].Rows[0]["DIJVConflictCheckDate"].ToString()).ToString("yyyy-MM-dd") : dtCCCompleted.Value = "";
                    dtDataSharing.Value = ds.Tables[0].Rows[0]["DataConfirmedByMFDate"].ToString() != "" ?
                                          DateTime.Parse(ds.Tables[0].Rows[0]["DataConfirmedByMFDate"].ToString()).ToString("yyyy-MM-dd") : dtDataSharing.Value = "";
                    txtMFConflictCheck.Text = ds.Tables[0].Rows[0]["MFConflictCheckConcernedPerson"].ToString();
                    txtDIJVSubmitted.Text = ds.Tables[0].Rows[0]["isDRMSForwardedConcernedPerson"].ToString();
                    txtCCCompleted.Text = ds.Tables[0].Rows[0]["DIJVConflictCheckConcernedPerson"].ToString();
                    txtDataSharing.Text = ds.Tables[0].Rows[0]["DataConfirmedByMFConcernedPerson"].ToString();

                    dtInternalRisk.Value = ds.Tables[0].Rows[0]["InternalRiskDate"].ToString() != "" ?
                                              DateTime.Parse(ds.Tables[0].Rows[0]["InternalRiskDate"].ToString()).ToString("yyyy-MM-dd") : dtInternalRisk.Value = "";
                    txtInternalRisk.Text = ds.Tables[0].Rows[0]["InternalRisk"].ToString();

                    DtTransactionRelate.Value = ds.Tables[0].Rows[0]["DataTransactionRelateDate"].ToString() != "" ?
                                          DateTime.Parse(ds.Tables[0].Rows[0]["DataTransactionRelateDate"].ToString()).ToString("yyyy-MM-dd") : DtTransactionRelate.Value = "";
                    dtAreYouWorking.Value = ds.Tables[0].Rows[0]["DataAreYouWorkingDate"].ToString() != "" ?
                                          DateTime.Parse(ds.Tables[0].Rows[0]["DataAreYouWorkingDate"].ToString()).ToString("yyyy-MM-dd") : dtAreYouWorking.Value = "";
                    dtIsThereConfidentiality.Value = ds.Tables[0].Rows[0]["DataIsThereConfidentialityDate"].ToString() != "" ?
                                          DateTime.Parse(ds.Tables[0].Rows[0]["DataIsThereConfidentialityDate"].ToString()).ToString("yyyy-MM-dd") : dtIsThereConfidentiality.Value = "";

                    txtTransactionRelate.Text = ds.Tables[0].Rows[0]["DataTransactionRelate"].ToString();
                    txtAreYouWorking.Text = ds.Tables[0].Rows[0]["DataAreYouWorking"].ToString();
                    txtIsThereConfidentiality.Text = ds.Tables[0].Rows[0]["DataIsThereConfidentiality"].ToString();

                    dtBriefingOfDIJVTeam.Value = ds.Tables[0].Rows[0]["BriefingOfDIJVTeamDate"].ToString() != "" ?
                                          DateTime.Parse(ds.Tables[0].Rows[0]["BriefingOfDIJVTeamDate"].ToString()).ToString("yyyy-MM-dd") : dtBriefingOfDIJVTeam.Value = "";
                    txtBriefingOfDIJVTeam.Text = ds.Tables[0].Rows[0]["BriefingOfDIJVTeamConcernedPerson"].ToString();

                    PopulateHours(ds.Tables[1]);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PopulateHours(DataTable dt)
        {
            try
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["ShortName"].ToString())
                    {
                        case "HoursAD":
                            txtADHours.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                        case "HoursAM":
                            txtAMHours.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                        case "HoursDir":
                            txtDirectorHours.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                        case "HoursDM":
                            txtDMHours.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                        case "HoursExec":
                            txtExecutiveHours.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                        case "HoursExec1":
                            txtExec1.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                        case "HoursMgr":
                            txtManagerHours.Text = dt.Rows[i]["Hours"].ToString();
                            break;
                    }

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
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;

                        break;
                    case 1: // saved, not submitted
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        valPartnerEmail.Enabled = false;
                        break;
                    case 2:// submitted
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        break;
                    case 3: // approved level 1
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        break;
                    case 4: // approved level 2
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        break;
                    case -3: // rejected level 1
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        break;
                    case -4: // rejected level 2
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EnableEditableFields()
        {
            try
            {

                txtClientName.ReadOnly = false;
                txtClientName.Enabled = true;
                dtEndDt.Disabled = false;
                chkCC.Enabled = true;
                chkCCCompleted.Enabled = true;
                chkDataSharing.Enabled = true;
                //chkDRMS.Enabled = true;

                dtMFConflictCheck.Disabled = false;
                dtInternalRisk.Disabled = false;
                dtDIJVSubmitted.Disabled = false;
                dtCCCompleted.Disabled = false;
                dtDataSharing.Disabled = false;

                DtTransactionRelate.Disabled = false;
                dtAreYouWorking.Disabled = false;
                dtIsThereConfidentiality.Disabled = false;

                txtTransactionRelate.Enabled = true;
                txtTransactionRelate.ReadOnly = false;

                txtAreYouWorking.Enabled = true;
                txtAreYouWorking.ReadOnly = false;

                txtIsThereConfidentiality.Enabled = true;
                txtIsThereConfidentiality.ReadOnly = false;

                txtMFConflictCheck.Enabled = true;
                txtMFConflictCheck.ReadOnly = false;

                txtInternalRisk.Enabled = true;
                txtInternalRisk.ReadOnly = false;

                txtDIJVSubmitted.Enabled = true;
                txtDIJVSubmitted.ReadOnly = false;
                txtCCCompleted.Enabled = true;
                txtCCCompleted.ReadOnly = false;
                txtDataSharing.Enabled = true;
                txtDataSharing.ReadOnly = false;

                dtBriefingOfDIJVTeam.Disabled = false;
                txtBriefingOfDIJVTeam.Enabled = true;
                txtBriefingOfDIJVTeam.ReadOnly = false;

                txtOverview.ReadOnly = false;
                txtOverview.Enabled = true;

                txtServicesRequired.Enabled = true;
                txtServicesRequired.ReadOnly = false;

                txtContact2.Enabled = true;
                txtContact2.ReadOnly = false;

                txtContact3.Enabled = true;
                txtContact3.ReadOnly = false;
                drpDIJVPartner.Enabled = true;
                txtDirectorHours.Enabled = true;
                txtDMHours.Enabled = true;
                txtADHours.Enabled = true;
                txtAMHours.Enabled = true;
                txtExecutiveHours.Enabled = true;
                txtExec1.Enabled = true;

                txtManagerHours.Enabled = true;
                txtDirectorHours.ReadOnly = false;
                txtDMHours.ReadOnly = false;
                txtADHours.ReadOnly = false;
                txtAMHours.ReadOnly = false;
                txtExecutiveHours.ReadOnly = false;
                txtExec1.ReadOnly = false;

                txtManagerHours.ReadOnly = false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DisablePostApproveorReject()
        {
            try
            {

                DisableFieldsPostSubmit(false);
                txtMemberFirmPartnerAppr.Enabled = false;
                txtMemberFirmPartnerAppr.ReadOnly = true;

                txtADHours.Enabled = false;
                txtADHours.ReadOnly = true;
                txtAMHours.Enabled = false;
                txtAMHours.ReadOnly = true;
                txtDMHours.Enabled = false;
                txtDMHours.ReadOnly = true;
                txtExecutiveHours.Enabled = false;
                txtExecutiveHours.ReadOnly = true;
                txtExec1.Enabled = false;
                txtExec1.ReadOnly = true;
                txtManagerHours.Enabled = false;
                txtManagerHours.ReadOnly = true;
                txtDirectorHours.Enabled = false;
                txtDirectorHours.ReadOnly = true;

            }
            catch (Exception ex)
            {

                throw ex;
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
                    btnDIJVAppr.Enabled = true;
                    btnDIJVAppr.Visible = true;
                    btnDIJVReject.Visible = true;
                    btnDIJVReject.Enabled = true;
                    valPartnerEmail.Enabled = true;
                    txtMemberFirmPartnerAppr.Enabled = true;
                    txtMemberFirmPartnerAppr.ReadOnly = false;
                }


                txtClientName.ReadOnly = true;
                txtClientName.Enabled = false;
                dtStartDate.Disabled = true;
                dtEndDt.Disabled = true;
                chkCC.Enabled = false;
                chkCCCompleted.Enabled = false;
                chkDataSharing.Enabled = false;
                //chkDRMS.Enabled = false;

                dtMFConflictCheck.Disabled = true;
                dtInternalRisk.Disabled = true;
                //dtDRMS.Disabled = true;
                dtDIJVSubmitted.Disabled = true;
                dtCCCompleted.Disabled = true;
                dtDataSharing.Disabled = true;
                txtMFConflictCheck.Enabled = false;
                txtMFConflictCheck.ReadOnly = true;

                txtInternalRisk.Enabled = false;
                txtInternalRisk.ReadOnly = true;

                txtDIJVSubmitted.Enabled = false;
                txtDIJVSubmitted.ReadOnly = true;
                txtCCCompleted.Enabled = false;
                txtCCCompleted.ReadOnly = true;
                txtDataSharing.Enabled = false;
                txtDataSharing.ReadOnly = true;
                dtBriefingOfDIJVTeam.Disabled = true;
                txtBriefingOfDIJVTeam.Enabled = false;
                txtBriefingOfDIJVTeam.ReadOnly = true;

                txtOverview.ReadOnly = true;
                txtOverview.Enabled = false;

                txtServicesRequired.Enabled = false;
                txtServicesRequired.ReadOnly = true;

                txtSharedDrive.ReadOnly = true;
                txtSharedDrive.Enabled = false;

                txtContact2.Enabled = false;
                txtContact2.ReadOnly = true;

                txtContact3.Enabled = false;
                txtContact3.ReadOnly = true;
                drpDIJVPartner.Enabled = false;

                txtExecutiveHours.ReadOnly = true;
                txtExec1.ReadOnly = true;

                txtManagerHours.ReadOnly = true;
                txtDMHours.ReadOnly = true;
                txtAMHours.ReadOnly = true;
                txtADHours.ReadOnly = true;
                txtDirectorHours.ReadOnly = true;

                txtDirectorHours.Enabled = false;
                txtManagerHours.Enabled = false;
                txtDMHours.Enabled = false;
                txtAMHours.Enabled = false;
                txtADHours.Enabled = false;
                txtExecutiveHours.Enabled = false;
                txtExec1.Enabled = false;


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

                dtEndDt.Value = string.Empty;
                chkCC.Checked = false;
                chkCCCompleted.Checked = false;
                chkDataSharing.Checked = false;
                //chkDRMS.Checked = false;

                dtMFConflictCheck.Value = string.Empty;
                dtInternalRisk.Value = string.Empty;
                //dtDRMS.Value = string.Empty;
                dtDIJVSubmitted.Value = string.Empty;
                dtCCCompleted.Value = string.Empty;
                dtDataSharing.Value = string.Empty;
                txtMFConflictCheck.Text = string.Empty;
                txtInternalRisk.Text = string.Empty;
                txtCCCompleted.Text = string.Empty;
                txtDataSharing.Text = string.Empty;
                dtBriefingOfDIJVTeam.Value = string.Empty;
                txtBriefingOfDIJVTeam.Text = string.Empty;

                DtTransactionRelate.Value = string.Empty;
                txtTransactionRelate.Text = string.Empty;

                dtAreYouWorking.Value = string.Empty;
                txtAreYouWorking.Text = string.Empty;

                dtIsThereConfidentiality.Value = string.Empty;
                txtIsThereConfidentiality.Text = string.Empty;
                //txtEpccStage1.Text = string.Empty;

                txtManagerHours.Text = string.Empty;
                txtExecutiveHours.Text = string.Empty;
                txtExec1.Text = string.Empty;
                txtDirectorHours.Text = string.Empty;
                txtDMHours.Text = string.Empty;
                txtAMHours.Text = string.Empty;
                txtADHours.Text = string.Empty;
                txtDMHours.Text = string.Empty;
                dtAppOrReject.Text = string.Empty;
                txtOverview.Text = string.Empty;
                txtServicesRequired.Text = string.Empty;
                txtSharedDrive.Text = string.Empty;
                txtContact2.Text = string.Empty;
                txtContact3.Text = string.Empty;
                //txtMemberFirmPartnerAppr.Text = string.Empty;
                dtPartnerApprRej.Text = string.Empty;
                txtStatusDIJV.Text = string.Empty;
                txtStatusPartner.Text = string.Empty;
                txtClientName.Text = string.Empty;
                txtWONumber.Text = string.Empty;

                DataSet dsMasters = new DataSet();
                DBHandler dh = new DBHandler();
                dsMasters = dh.GetMasters();
                drpDIJVPartner.DataSource = dsMasters.Tables[8];
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
            try
            {

                txtProjectCode.Text = string.Empty;
                txtMemberFirm.Text = string.Empty;
                txtRefOffice.Text = string.Empty;
                txtEngmntName.Text = string.Empty;
                txtEngmntType.Text = string.Empty;
                txtEngmntManager.Text = string.Empty;
                txtEngmntPartner.Text = string.Empty;
                txtDIJVPartner.Text = string.Empty;
                txtConflictCheck.Text = string.Empty;
                txtJobCode.Text = string.Empty;
                dtStartDate.Value = string.Empty;
                hidWOID.Value = "";
                ClearWOFieldsOnly();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EnableOnlyReviseWOFields()
        {
            try
            {

                txtDirectorHours.Enabled = true;
                txtDMHours.Enabled = true;
                txtADHours.Enabled = true;
                txtAMHours.Enabled = true;
                txtExecutiveHours.Enabled = true;
                txtExec1.Enabled = true;

                txtManagerHours.Enabled = true;
                txtDirectorHours.ReadOnly = false;
                txtDMHours.ReadOnly = false;
                txtADHours.ReadOnly = false;
                txtAMHours.ReadOnly = false;
                txtExecutiveHours.ReadOnly = false;
                txtExec1.ReadOnly = false;

                txtManagerHours.ReadOnly = false;
                drpDIJVPartner.Enabled = true;

                dtEndDt.Disabled = false;

                dtCCCompleted.Disabled = false;
                txtCCCompleted.ReadOnly = false;
                txtCCCompleted.Enabled = true;
                dtMFConflictCheck.Disabled = false;
                dtInternalRisk.Disabled = false;
                txtMFConflictCheck.ReadOnly = false;
                txtMFConflictCheck.Enabled = true;

                txtInternalRisk.ReadOnly = false;
                txtInternalRisk.Enabled = true;
                //dtDRMS.Value = string.Empty;
                dtDIJVSubmitted.Disabled = false;
                txtDIJVSubmitted.ReadOnly = false;
                txtDIJVSubmitted.Enabled = true;

                dtDataSharing.Disabled = false;
                txtDataSharing.ReadOnly = false;
                txtDataSharing.Enabled = true;

                DtTransactionRelate.Disabled = false;
                txtTransactionRelate.ReadOnly = false;
                txtTransactionRelate.Enabled = true;

                dtAreYouWorking.Disabled = false;
                txtAreYouWorking.ReadOnly = false;
                txtAreYouWorking.Enabled = true;

                dtIsThereConfidentiality.Disabled = false;
                txtIsThereConfidentiality.ReadOnly = false;
                txtIsThereConfidentiality.Enabled = true;

                txtJobCode.ReadOnly = false;
                txtJobCode.Enabled = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EnableHoursFields()
        {
            try
            {

                txtDirectorHours.Enabled = true;
                txtADHours.Enabled = true;
                txtManagerHours.Enabled = true;
                txtDMHours.Enabled = true;
                txtAMHours.Enabled = true;
                txtExecutiveHours.Enabled = true;
                txtExec1.Enabled = true;
                txtDirectorHours.ReadOnly = false;
                txtADHours.ReadOnly = false;
                txtManagerHours.ReadOnly = false;
                txtDMHours.ReadOnly = false;
                txtAMHours.ReadOnly = false;
                txtExecutiveHours.ReadOnly = false;
                txtExec1.ReadOnly = false;

            }
            catch (Exception ex)
            {

                throw ex;
            }

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

                hidProjectID.Value = (e.CommandArgument.ToString()).ToString();
                DataSet ds = new DataSet();
                DataSet dsWO = new DataSet();
                DataSet dsWODetails = new DataSet();
                ds = dh.GetProjectData(int.Parse(e.CommandArgument.ToString()));
                dsWO = (dh.GetWorkOrderData(int.Parse(e.CommandArgument.ToString()), ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString()));
                //string memberFirmID = dh.GetEmployeeCountryMailID(Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]), Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]));
                string memberFirmID = dh.GetEmployeeCountryMailID(Convert.ToInt32(ds.Tables[0].Rows[0]["IJVManager"]), Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]));
                hidMemberFirmID.Value = memberFirmID;
                GetDesignations(int.Parse(e.CommandArgument.ToString()));
                hidWONumber.Value = ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString();
                if (dsWO.Tables[0].Rows.Count > 0)
                {
                    dsWODetails = dh.WorkOrder_PDFData_GET_Report(dsWO.Tables[0].Rows[0]["Id"].ToString());
                }
                if (e.CommandName == "edt")
                {
                    divWorkOrder.Visible = true;

                    DataTable dsEmployee = new DataTable();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());

                    LoadWorkOrderData(dsWO);
                    LoadProjectData(ds);

                    //txtContact1.Text = dh.GetEmployeeCountryMailID(Convert.ToInt32(ds.Tables[0].Rows[0]["IJVManager"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]));
                    txtContact1.Text = memberFirmID;
                    //string userid = CommonFunctions.GetUser();

                    dsEmployee = dh.GetSingleEmployeeDetails(drpDIJVPartner.SelectedValue, "");
                    string statusDIJVPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["isDIJVApproved"]) ? (dsWO.Tables[0].Rows[0]["isDIJVApproved"]).ToString() : null;
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
                        EnableHoursFields();
                    }

                    string statusMFEngagementPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["isMFApproved"]) ? (dsWO.Tables[0].Rows[0]["isMFApproved"]).ToString() : null;
                    if (statusMFEngagementPartner == "True")
                    {
                        EnableHoursFields();
                        SwitchCasesBasedOnStatus(-4);
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

                    if (!Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsWOSubmitted"]))
                    {
                        EnableEditableFields();
                        SwitchCasesBasedOnStatus(1);
                    }
                    else
                    {
                        DisableFieldsPostSubmit(false);
                        SwitchCasesBasedOnStatus(2);
                    }

                    if (dsEmployee.Rows[0]["Email"].ToString().ToLower() == CommonFunctions.GetUser().ToLower() && statusDIJVPartner == null)   //Login User : DIJV approver and status: Not approved                
                    {
                        DisableFieldsPostSubmit(true);
                        btnDIJVAppr.Visible = true;
                        btnDIJVReject.Visible = true;
                        valPartnerEmail.Enabled = true;
                    }
                    if (dsEmployee.Rows[0]["Email"].ToString().ToLower() == CommonFunctions.GetUser().ToLower() && statusDIJVPartner == "True" && statusMFEngagementPartner != "False")   //Login User : DIJV approver and status: Approved                
                    {
                        DisableFieldsPostSubmit(true);
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        valPartnerEmail.Enabled = false;
                        txtMemberFirmPartnerAppr.ReadOnly = true;
                        txtMemberFirmPartnerAppr.Enabled = false;

                    }
                    if (dsEmployee.Rows[0]["Email"].ToString().ToLower() == CommonFunctions.GetUser().ToLower() && statusDIJVPartner == "False")   //Login User : DIJV approver and status: Rejected
                    {
                        DisableFieldsPostSubmit(false);
                        btnDIJVAppr.Visible = false;
                        btnDIJVReject.Visible = false;
                        valPartnerEmail.Enabled = false;
                        txtMemberFirmPartnerAppr.ReadOnly = true;
                        txtMemberFirmPartnerAppr.Enabled = false;
                        EnableEditableFields();
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
                    }

                    if (statusDIJVPartner != "True" && !Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsWOSubmitted"])
                        && CommonFunctions.CheckUserAccessOfWorkOrderEdit(CommonFunctions.GetUser().ToLower(), Convert.ToInt32(dsWO.Tables[0].Rows[0]["ProjectID"])))
                    {
                        EnableEditableFields();
                        SwitchCasesBasedOnStatus(1);
                    }

                    if (statusDIJVPartner == null) // Approver: DIJVEngagementPartner Status: Pending
                    {
                        EnableHoursFields();
                        if (Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsWOSubmitted"])
                            && dsWODetails.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() == CommonFunctions.GetUser().ToLower())   // status not approved and creater               
                        {
                            EnableEditableFields();
                            SwitchCasesBasedOnStatus(1);
                        }
                    }

                    if (statusMFEngagementPartner == "False") // MF Pertner Rejected
                    {
                        EnableHoursFields();
                        EnableEditableFields();
                        SwitchCasesBasedOnStatus(1);
                    }
                    // Code added  by vijay as on 12 Feb
                    //if (statusDIJVPartner == "True" && (statusMFEngagementPartner == "False" || statusMFEngagementPartner == null || statusMFEngagementPartner == ""))
                    //{
                    txtMFConflictCheck.Enabled = true;
                    txtMFConflictCheck.ReadOnly = false;

                    txtInternalRisk.Enabled = true;
                    txtInternalRisk.ReadOnly = false;

                    dtMFConflictCheck.Disabled = false;
                    dtInternalRisk.Disabled = false;

                    txtDIJVSubmitted.Enabled = true;
                    txtDIJVSubmitted.ReadOnly = false;
                    dtDIJVSubmitted.Disabled = false;

                    txtCCCompleted.Enabled = true;
                    txtCCCompleted.ReadOnly = false;
                    dtCCCompleted.Disabled = false;

                    txtConflictCheck.Enabled = true;
                    txtConflictCheck.ReadOnly = false;

                    btnUpdate.Visible = true;
                    //}
                    //else
                    //{
                    //    btnUpdate.Visible = false;
                    //}


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToWorkOrder", "setTimeout(scrollToWorkOrder, 1);", true);

                }

                if (e.CommandName == "GenerateWO")
                {
                    //GetDesignations(int.Parse(e.CommandArgument.ToString()));
                    divWorkOrder.Visible = true;
                    LoadProjectData(ds);

                    ClearWOFieldsOnly();
                    //txtContact1.Text = memberFirmID;
                    txtContact1.Text = dh.GetEmployeeCountryMailID(Convert.ToInt32(ds.Tables[0].Rows[0]["IJVManager"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]));
                    if (drpDIJVPartner.Items.FindByValue(ds.Tables[0].Rows[0]["IJVPartner"].ToString()) != null)
                    {
                        drpDIJVPartner.SelectedValue = ds.Tables[0].Rows[0]["IJVPartner"].ToString();
                        EnableEditableFields();
                        //bindGrid();
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
                    drpVersion.SelectedValue = "Revise";
                    txtStatusPartner.Text = "Approved";
                    txtStatusDIJV.Text = "Approved";
                    LoadWorkOrderData(dsWO);
                    LoadProjectData(ds);
                    DisablePostApproveorReject();
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
                    gvProjects.DataSource = dbh.Project_Data_SEARCH_RoleBased(txtProjectName.Text, CommonFunctions.GetUser());
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
            try
            {

                DBHandler handler = new DBHandler();
                string UserId = CommonFunctions.GetUser();

                string message1 = "";
                string wo_number = "";
                //string href = "";
                if (hidWOID.Value != "" && hidWOID.Value != null)
                {
                    string workOrderID = handler.WorkOrder_Update(int.Parse(hidProjectID.Value), int.Parse(hidWOID.Value), txtClientName.Text.Trim(),
                        dtEndDt.Value.ToString(),
                        chkCC.Checked == true ? "1" : "0",
                        chkCCCompleted.Checked == true ? "1" : "0",
                        chkDataSharing.Checked == true ? "1" : "0",
                        chkDIJV.Checked == true ? "1" : "0",
                        txtOverview.Text,
                        txtServicesRequired.Text,
                        txtADHours.Text != "" ? decimal.Parse(txtADHours.Text) : 0,
                        txtAMHours.Text != "" ? decimal.Parse(txtAMHours.Text) : 0,
                        txtDMHours.Text != "" ? decimal.Parse(txtDMHours.Text) : 0,
                        txtExecutiveHours.Text != "" ? decimal.Parse(txtExecutiveHours.Text) : 0,
                        txtExec1.Text != "" ? decimal.Parse(txtExec1.Text) : 0,
                        txtManagerHours.Text != "" ? decimal.Parse(txtManagerHours.Text) : 0,
                        txtDirectorHours.Text != "" ? decimal.Parse(txtDirectorHours.Text) : 0,
                        txtSharedDrive.Text, txtContact2.Text, txtContact3.Text,
                        UserId, drpDIJVPartner.Text, isSubmit, txtProjectCode.Text, hidMemberFirmID.Value,

                        dtMFConflictCheck.Value != "" ? DateTime.Parse(dtMFConflictCheck.Value) : (DateTime?)null, txtMFConflictCheck.Text,
                        dtInternalRisk.Value != "" ? DateTime.Parse(dtInternalRisk.Value) : (DateTime?)null, txtInternalRisk.Text,

                        dtDIJVSubmitted.Value != "" ? DateTime.Parse(dtDIJVSubmitted.Value) : (DateTime?)null, txtDIJVSubmitted.Text,
                        dtCCCompleted.Value != "" ? DateTime.Parse(dtCCCompleted.Value) : (DateTime?)null, txtCCCompleted.Text,
                        dtDataSharing.Value != "" ? DateTime.Parse(dtDataSharing.Value) : (DateTime?)null, txtDataSharing.Text,

                        DtTransactionRelate.Value != "" ? DateTime.Parse(DtTransactionRelate.Value) : (DateTime?)null, txtTransactionRelate.Text,
                        dtAreYouWorking.Value != "" ? DateTime.Parse(dtAreYouWorking.Value) : (DateTime?)null, txtAreYouWorking.Text,
                        dtIsThereConfidentiality.Value != "" ? DateTime.Parse(dtIsThereConfidentiality.Value) : (DateTime?)null, txtIsThereConfidentiality.Text,
                        //txtEpccStage1.Text,

                        dtBriefingOfDIJVTeam.Value != "" ? DateTime.Parse(dtBriefingOfDIJVTeam.Value) : (DateTime?)null, txtBriefingOfDIJVTeam.Text,
                        drpVersion.SelectedValue,
                        out message1, out href);
                    //bindGrid();
                    if (workOrderID != "-1")
                    {
                        if (isSubmit == "1")
                        {
                            DisableFieldsPostSubmit(false);
                            MsgBox("Work order successfully submitted for approval");
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
                    string workOrderID = handler.WorkOrder_Insert(int.Parse(hidProjectID.Value), txtClientName.Text.Trim(),
                        dtEndDt.Value.ToString(),
                        chkCC.Checked == true ? "1" : "0",
                        chkCCCompleted.Checked == true ? "1" : "0",
                        chkDataSharing.Checked == true ? "1" : "0",
                        chkDIJV.Checked == true ? "1" : "0",
                        txtOverview.Text,
                        txtServicesRequired.Text,
                        txtADHours.Text != "" ? decimal.Parse(txtADHours.Text) : 0,
                        txtAMHours.Text != "" ? decimal.Parse(txtAMHours.Text) : 0,
                        txtDMHours.Text != "" ? decimal.Parse(txtDMHours.Text) : 0,
                        txtExecutiveHours.Text != "" ? decimal.Parse(txtExecutiveHours.Text) : 0,
                        txtExec1.Text != "" ? decimal.Parse(txtExec1.Text) : 0,
                        txtManagerHours.Text != "" ? decimal.Parse(txtManagerHours.Text) : 0,
                        txtDirectorHours.Text != "" ? decimal.Parse(txtDirectorHours.Text) : 0,
                        txtSharedDrive.Text, txtContact2.Text, txtContact3.Text,
                        UserId, drpDIJVPartner.Text, isSubmit, txtProjectCode.Text, hidMemberFirmID.Value,
                        dtMFConflictCheck.Value != "" ? DateTime.Parse(dtMFConflictCheck.Value) : (DateTime?)null, txtMFConflictCheck.Text,
                        dtInternalRisk.Value != "" ? DateTime.Parse(dtInternalRisk.Value) : (DateTime?)null, txtInternalRisk.Text,
                        dtDIJVSubmitted.Value != "" ? DateTime.Parse(dtDIJVSubmitted.Value) : (DateTime?)null, txtDIJVSubmitted.Text,
                        dtCCCompleted.Value != "" ? DateTime.Parse(dtCCCompleted.Value) : (DateTime?)null, txtCCCompleted.Text,
                        dtDataSharing.Value != "" ? DateTime.Parse(dtDataSharing.Value) : (DateTime?)null, txtDataSharing.Text,

                        DtTransactionRelate.Value != "" ? DateTime.Parse(DtTransactionRelate.Value) : (DateTime?)null, txtTransactionRelate.Text,
                        dtAreYouWorking.Value != "" ? DateTime.Parse(dtAreYouWorking.Value) : (DateTime?)null, txtAreYouWorking.Text,
                        dtIsThereConfidentiality.Value != "" ? DateTime.Parse(dtIsThereConfidentiality.Value) : (DateTime?)null, txtIsThereConfidentiality.Text,
                        //txtEpccStage1.Text,
                        dtBriefingOfDIJVTeam.Value != "" ? DateTime.Parse(dtBriefingOfDIJVTeam.Value) : (DateTime?)null, txtBriefingOfDIJVTeam.Text,
                        drpVersion.SelectedValue,
                        out message1, out wo_number, out href);

                    if (workOrderID != "-1")
                    {
                        MsgBox("Work order has been successfully saved. " +
                            "Work Order number updated in work order code");
                        //txtWONumber.Text = "WO-PN-" + int.Parse(hidProjectID.Value);
                        txtWONumber.Text = wo_number;
                        hidWOID.Value = workOrderID.ToString();
                        bindGrid();
                        if (isSubmit == "1")
                        {
                            MsgBox("Work Order Successfully Submitted for Approval");

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

        public void UpdateData(string isSubmit, out string href)
        {
            try
            {

                DBHandler handler = new DBHandler();
                string UserId = CommonFunctions.GetUser();

                string message1 = "";
                string wo_number = "";
                //string href = "";
                href = "";
                if (hidWOID.Value != "" && hidWOID.Value != null)
                {
                    string workOrderID = handler.WorkOrder_Partial_Update(int.Parse(hidWOID.Value), 
                        txtConflictCheck.Text,txtMFConflictCheck.Text,txtDIJVSubmitted.Text,txtCCCompleted.Text,
                        txtInternalRisk.Text, dtInternalRisk.Value != "" ? DateTime.Parse(dtInternalRisk.Value) : (DateTime?)null,
                        dtMFConflictCheck.Value != "" ? DateTime.Parse(dtMFConflictCheck.Value) : (DateTime?)null,
                        
                        dtDIJVSubmitted.Value != "" ? DateTime.Parse(dtDIJVSubmitted.Value) : (DateTime?)null,
                        dtCCCompleted.Value != "" ? DateTime.Parse(dtCCCompleted.Value) : (DateTime?)null,
                        txtTransactionRelate.Text,
                        txtAreYouWorking.Text,
                        txtIsThereConfidentiality.Text,
                        DtTransactionRelate.Value != "" ? DateTime.Parse(DtTransactionRelate.Value) : (DateTime?)null,
                        dtAreYouWorking.Value != "" ? DateTime.Parse(dtAreYouWorking.Value) : (DateTime?)null,
                        dtIsThereConfidentiality.Value != "" ? DateTime.Parse(dtIsThereConfidentiality.Value) : (DateTime?)null,
                        out message1, out href);
                    //bindGrid();
                    if (workOrderID != "-1")
                    {
                        if (isSubmit == "1")
                        {
                            MsgBox("Work order successfully submitted for approval");
                        }
                        else
                        {
                            //MsgBox("Update successful");
                            //Response.Redirect("~/WorkOrder.aspx", true);
                            string script = "window.onload = function(){ alert('";
                            script += "Update successful !";
                            script += "');";
                            script += "window.location = '";
                            script += "WorkOrder.aspx";
                            script += "'; }";
                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                        }
                    }
                    else
                    {
                        MsgBox(message1);
                    }
                    //href = "";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void SaveWorkOrderHoursData()
        {
            try
            {

                DBHandler handler = new DBHandler();
                int UserId = handler.GetUserId();
                if (hidWOID.Value != "" && hidWOID.Value != null)
                {
                    string workOrderID = handler.WorkOrderHours_Update(int.Parse(hidWOID.Value),
                        txtADHours.Text != "" ? decimal.Parse(txtADHours.Text) : 0,
                        txtAMHours.Text != "" ? decimal.Parse(txtAMHours.Text) : 0,
                        txtDMHours.Text != "" ? decimal.Parse(txtDMHours.Text) : 0,
                        txtExecutiveHours.Text != "" ? decimal.Parse(txtExecutiveHours.Text) : 0,
                        txtExec1.Text != "" ? decimal.Parse(txtExec1.Text) : 0,
                        txtManagerHours.Text != "" ? decimal.Parse(txtManagerHours.Text) : 0,
                        txtDirectorHours.Text != "" ? decimal.Parse(txtDirectorHours.Text) : 0);
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

                DBHandler dh = new DBHandler();
                dh.UpdateStatusFromDIJVApprover(status, woLogNumber, DIJVApprover, memberFirmPartner);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string href = "";
            SaveData("0", out href);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string href = "";
            UpdateData("0", out href);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                string href = "";
                SaveData("1", out href);
                if (href != null && href != "")
                {
                    EmailManager em = new EmailManager();
                    DataSet dsWO = dbh.WorkOrder_PDFData_GET_Report(hidWOID.Value);
                    LocalReport lr = new LocalReport();
                    ReportViewerWO.ProcessingMode = ProcessingMode.Local;
                    lr.ReportPath = HttpContext.Current.Server.MapPath("~/Report/WorkOrderDetails.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSetWorkOrder", dsWO.Tables[0]);
                    ReportViewerWO.LocalReport.DataSources.Clear();
                    ReportViewerWO.LocalReport.DataSources.Add(datasource);
                    string WOLogNumber = dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString();

                    string templateName = string.Empty;
                    string Subject = string.Empty;
                    if (dsWO.Tables[0].Rows[0]["WOVersion"].ToString() == "Revise")
                    {
                        templateName = "WorkOrderApproval_IJV_RevisedOnSubmit";
                        Subject = "Revised Work Order(" + WOLogNumber + ") Request For Approval";
                    }
                    else
                    {
                        templateName = "WorkOrderApproval_IJV_InitialOnSubmit";
                        Subject = "Initial Work Order(" + WOLogNumber + ") Request For Approval";
                    }

                    string mBodyForIJVManager = PopulateBody(WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                                dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString() + ';' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString(),
                                                hidWOID.Value, dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, href);
                    em.SendMailWithAttachment(dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString(), "indijvProjects@deloitte.com", Subject, mBodyForIJVManager, dsWO.Tables[0].Rows[0]["Creator"].ToString(), "", null, ReportViewerWO);
                }
                MsgBox("Work order has been approved. A mail has been sent to the Enagagement Partner to approve or reject the work order.");

                dtAppOrReject.Text = string.Empty;
                txtStatusDIJV.Text = string.Empty;
                dtPartnerApprRej.Text = string.Empty;
                txtStatusPartner.Text = string.Empty;
                txtMemberFirmPartnerAppr.Text = string.Empty;

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

                UpdateStatusFromDIJV("1", int.Parse(hidWOID.Value), drpDIJVPartner.SelectedValue.ToString(), txtMemberFirmPartnerAppr.Text);
                txtStatusDIJV.Text = "Approved";
                dtAppOrReject.Text = DateTime.UtcNow.ToString();
                btnDIJVAppr.Enabled = false;
                btnDIJVReject.Enabled = false;
                valPartnerEmail.Enabled = false;
                EmailManager em = new EmailManager();
                SaveWorkOrderHoursData();
                DataSet dsWO = dbh.WorkOrder_PDFData_GET_Report(hidWOID.Value);
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
                    Subject = "Revised Work Order(" + hidWOID.Value + ") Request For Approval";
                }
                else
                {
                    templateName = "WorkOrderApproval_IJV_Initial";
                    Subject = "Initial Work Order(" + hidWOID.Value + ") Request For Approval";
                }

                string mBody = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                            dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                            + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                            hidWOID.Value, dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), templateName, "");
                em.SendMailWithAttachment(dsWO.Tables[0].Rows[0]["MFPartnerEmailId"].ToString(), "indijvProjects@deloitte.com", Subject, mBody,
                    dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["Creator"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["MFManagerEmailId"].ToString(), "", null, ReportViewerWO);

                string mBodyForIJVManager = PopulateBody(dsWO.Tables[0].Rows[0]["DIJVEngagementPartnerEmailId"].ToString(),
                                            dsWO.Tables[0].Rows[0]["DIJV_Approver_Email"].ToString() + ';' + ' ' + dsWO.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() + ';' + ' '
                                            + WebConfigurationManager.AppSettings["mailboxEmaiId"],
                                            hidWOID.Value, dsWO.Tables[0].Rows[0]["MFPartnerName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVEngagementManagerName"].ToString(), dsWO.Tables[0].Rows[0]["EngagementName"].ToString(), dsWO.Tables[0].Rows[0]["DIJVProjectCode"].ToString(), "WorkOrderNotification", "");
                em.SendMailWithAttachment(dsWO.Tables[0].Rows[0]["MFManagerEmailId"].ToString(), "indijvProjects@deloitte.com", "Work Order Request For Approval", mBodyForIJVManager, "", "", null, ReportViewerWO);
                MsgBox("Work order has been approved. A mail has been sent to the Enagagement Partner to approve or reject the work order.");
                DisablePostApproveorReject();
                SwitchCasesBasedOnStatus(3);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnDIJVReject_Click(object sender, EventArgs e)
        {
            try
            {

                UpdateStatusFromDIJV("0", int.Parse(hidWOID.Value), drpDIJVPartner.SelectedValue.ToString(), "");
                txtStatusDIJV.Text = "Rejected";
                btnDIJVAppr.Enabled = false;
                valPartnerEmail.Enabled = false;
                btnDIJVReject.Enabled = false;
                MsgBox("Work order has been rejected");
                DisablePostApproveorReject();
                SwitchCasesBasedOnStatus(-3);
                dtAppOrReject.Text = DateTime.UtcNow.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
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

                DBHandler dh = new DBHandler();

                hidProjectID.Value = (projectId).ToString();
                DataSet ds = new DataSet();
                DataSet dsWO = new DataSet();
                DataSet dsWODetails = new DataSet();

                ds = dh.GetProjectData(int.Parse(projectId));
                dsWO = (dh.GetWorkOrderData(int.Parse(projectId), WONumber));
                string memberFirmID = dh.GetEmployeeCountryMailID(Convert.ToInt32(createdBy), Convert.ToInt32(countryId));
                hidMemberFirmID.Value = memberFirmID;
                GetDesignations(int.Parse(projectId));
                hidWONumber.Value = WONumber;
                if (dsWO.Tables[0].Rows.Count > 0)
                {
                    dsWODetails = dh.WorkOrder_PDFData_GET_Report(dsWO.Tables[0].Rows[0]["Id"].ToString());
                }
                divWorkOrder.Visible = true;

                DataTable dsEmployee = new DataTable();

                LoadWorkOrderData(dsWO);
                LoadProjectData(ds);

                //txtContact1.Text = memberFirmID;
                txtContact1.Text = dh.GetEmployeeCountryMailID(Convert.ToInt32(ds.Tables[0].Rows[0]["IJVManager"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]));
                //string userid = CommonFunctions.GetUser();

                dsEmployee = dh.GetSingleEmployeeDetails(DIJVPartner, "");
                string status = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["isDIJVApproved"]) ? (dsWO.Tables[0].Rows[0]["isDIJVApproved"]).ToString() : null;
                if (status == "True")
                {
                    txtStatusDIJV.Text = "Approved";
                }
                else if (status == "False")
                {
                    txtStatusDIJV.Text = "Rejected";
                }
                else
                {
                    txtStatusDIJV.Text = string.Empty;
                    EnableHoursFields();
                }

                string statusMFEngagementPartner = !Convert.IsDBNull(dsWO.Tables[0].Rows[0]["isMFApproved"]) ? (dsWO.Tables[0].Rows[0]["isMFApproved"]).ToString() : null;
                if (statusMFEngagementPartner == "True")
                {
                    txtStatusPartner.Text = "Approved";
                }
                else if (statusMFEngagementPartner == "False")
                {
                    txtStatusPartner.Text = "Rejected";
                }
                else
                {
                    txtStatusPartner.Text = string.Empty;
                }

                if (!Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsWOSubmitted"]))
                {
                    EnableEditableFields();
                    SwitchCasesBasedOnStatus(1);
                }
                else
                {
                    DisableFieldsPostSubmit(false);
                    SwitchCasesBasedOnStatus(2);
                }
                if (dsEmployee.Rows[0]["Email"].ToString().ToLower() == CommonFunctions.GetUser().ToLower() && status == null)   //if approver and status not approved                
                {
                    DisableFieldsPostSubmit(true);
                    btnDIJVAppr.Visible = true;
                    btnDIJVReject.Visible = true;
                    valPartnerEmail.Enabled = true;
                }
                if (dsEmployee.Rows[0]["Email"].ToString().ToLower() == CommonFunctions.GetUser().ToLower() && status == "True")   //if approver and status approved                
                {
                    DisableFieldsPostSubmit(true);
                    btnDIJVAppr.Visible = false;
                    btnDIJVReject.Visible = false;
                    valPartnerEmail.Enabled = false;
                    txtMemberFirmPartnerAppr.ReadOnly = true;
                    txtMemberFirmPartnerAppr.Enabled = false;

                }
                if (dsEmployee.Rows[0]["Email"].ToString().ToLower() == CommonFunctions.GetUser().ToLower() && status == "False")   //if approver and status rejected                
                {
                    DisableFieldsPostSubmit(false);
                    btnDIJVAppr.Visible = false;
                    btnDIJVReject.Visible = false;
                    valPartnerEmail.Enabled = false;
                    txtMemberFirmPartnerAppr.ReadOnly = true;
                    txtMemberFirmPartnerAppr.Enabled = false;
                    EnableEditableFields();
                    btnSave.Visible = true;
                    btnSubmit.Visible = true;
                }

                if (!Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsWOSubmitted"])
                    || dsWODetails.Tables[0].Rows[0]["DIJV_team_member_2"].ToString() == CommonFunctions.GetUser().ToLower()
                    || dsWODetails.Tables[0].Rows[0]["DIJV_team_member_3"].ToString() == CommonFunctions.GetUser().ToLower())
                {
                    EnableEditableFields();
                    SwitchCasesBasedOnStatus(1);
                }

                if (status == null)
                {
                    EnableHoursFields();
                    if (Convert.ToBoolean(dsWO.Tables[0].Rows[0]["IsWOSubmitted"]) && !Convert.ToBoolean(dsWODetails.Tables[0].Rows[0]["isDIJVApproved"])
                        && dsWODetails.Tables[0].Rows[0]["DIJV_team_member_1"].ToString() == CommonFunctions.GetUser().ToLower())   // status not approved and creater               
                    {
                        EnableEditableFields();
                        SwitchCasesBasedOnStatus(1);
                    }
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "scrollToWorkOrder", "setTimeout(scrollToWorkOrder, 1);", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public bool ValidateWOEmailIds()
        {
            try
            {

            bool IsEmailValid = true;
            if (!CommonFunctions.ValidateEmail(txtContact2.Text))
            {
                MsgBox("Invalid DIJV Team member 2 EmailId!");
                IsEmailValid = false;
            }
            else if (!CommonFunctions.ValidateEmail(txtContact3.Text))
            {
                MsgBox("Invalid DIJV Team member 3 EmailId!");
                IsEmailValid = false;
            }
            return IsEmailValid;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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
    }
}