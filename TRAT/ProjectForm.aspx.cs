using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using static TRAT.DBHandler;


namespace TRAT
{
    public partial class ProjectForm : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        EventArgs e1 = new EventArgs();

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
                    lblConcession.Visible = false;
                    divConcessionDocument.Visible = false;
                    FileUploadDiv.Visible = false;
                    divAnalyticsTool.Visible = false;
                    lblToolKnown.Visible = false;
                    divshowToolKnown.Visible = false;
                    
                    LoadMasters();

                    if (!string.IsNullOrEmpty(Request.QueryString["project"]))
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["Replication"]))
                        {
                            hidProjectID.Value = "";
                        }
                        else
                        {
                            hidProjectID.Value = (Request.QueryString["project"]);
                        }
                        DBHandler dh = new DBHandler();
                        DataSet ds = new DataSet();

                        ds = dh.GetProjectData(int.Parse(Request.QueryString["project"]));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            LoadProjectData(ds);
                            dropdownToolsKnown(ds);
                            if (!string.IsNullOrEmpty(Request.QueryString["NEW"]))
                            {
                                DisableAllFields(0);
                                MsgBox("Project has been successfully saved. " +
                            "Project number updated in project code and mail to the engagement manager has been sent. For any updates or " +
                            "changes please go to the search page and edit the project.");
                            }
                            if (ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString() != null && ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString() != "")
                            {
                                DisableAllFields(1);
                                DataSet dsWO = (dh.GetWorkOrderData(int.Parse(ds.Tables[0].Rows[0]["Id"].ToString()), ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString()));
                                if (dsWO.Tables[0].Rows.Count > 0)
                                {
                                    DataSet dsWODetails = dh.WorkOrder_PDFData_GET_Report(dsWO.Tables[0].Rows[0]["Id"].ToString());
                                    bool isDIJVApproved = Convert.ToBoolean(dsWODetails.Tables[0].Rows[0]["isDIJVApproved"]);
                                    bool isMFApproved = Convert.ToBoolean(dsWODetails.Tables[0].Rows[0]["isMFApproved"]);
                                    if (!isDIJVApproved || (isDIJVApproved && isMFApproved) || !isMFApproved)
                                    {
                                        EnableFieldsAfterWorkOrderCreated();
                                    }
                                    //As per shruti mantri instruction code has been commented by Vijay Bhagat on 9 March 2020
                                    //if (isDIJVApproved && (dsWO.Tables[0].Rows[0]["isMFApproved"] == null || dsWO.Tables[0].Rows[0]["isMFApproved"].ToString() == ""))
                                    //{
                                    //    txtConflictCheck.ReadOnly = true;
                                    //    drpConflictCheck.Enabled = false;
                                    //}

                                }
                            }
                            else
                            {
                                DisableEditViewModeFields();
                            }
                        }
                    }
                    else
                    {
                        InitialiseNewProjectForm();

                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InitialiseNewProjectForm()
        {
            try
            {
                //Fields readonly at the start
                txtProjectCode.ReadOnly = true;

                divOthers.Visible = false;
                divDocument.Visible = false;
               
                FileUploadDiv.Visible = false;
                txtConflictCheck.ReadOnly = true; // enables when is conflict check is set to yes
                txtWorkOrderNumber.ReadOnly = true;
                valNatureOfWork.Enabled = false;
                lblNatureOfWork.Visible = false;
                drpNatureOfWork.SelectedValue = "-1";
                drpNatureOfWork.Items.Clear();
                drpNatureOfWork.Items.Insert(0, new ListItem("--Select--", "-1"));
                drpSubSector.Items.Clear();
                drpSubSector.Items.Insert(0, new ListItem("--Select--", "-1"));
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

                divMainDocument.Visible = false;
                DBHandler handler = new DBHandler();
                DataSet ds = new DataSet();
                ds = handler.GetMasters();

                drpSector.DataSource = ds.Tables[0];
                drpSector.DataTextField = "Name";
                drpSector.DataValueField = "Id";
                drpSector.DataBind();
                drpSector.Items.Insert(0, new ListItem("--Select--", "-1"));


                drpSubSector.Items.Insert(0, new ListItem("--Select--", "-1"));

                drpServiceLine.DataSource = ds.Tables[2];
                drpServiceLine.DataTextField = "Name";
                drpServiceLine.DataValueField = "Id";
                drpServiceLine.DataBind();
                drpServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

                drpSubServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

                drpNatureOfWork.Items.Insert(0, new ListItem("--Select--", "-1"));

                drpMemberFirm.DataSource = ds.Tables[4];
                drpMemberFirm.DataTextField = "Name";
                drpMemberFirm.DataValueField = "Id";
                drpMemberFirm.DataBind();
                drpMemberFirm.Items.Insert(0, new ListItem("--Select--", "-1"));


                ds.Tables[7].DefaultView.Sort = "Employee_Name ASC";
                ds.Tables[7].Columns.Add(new DataColumn("Title", System.Type.GetType("System.String"), "Employee_Name + ' - ' + Email"));

                ddlManagerList.DataSource = ds.Tables[7];

                ddlManagerList.DataTextField = "Title";
                ddlManagerList.DataValueField = "Id";
                ddlManagerList.DataBind();
                ddlManagerList.Items.Insert(0, new ListItem("--Select--", "-1"));

                ds.Tables[8].DefaultView.Sort = "Employee_Name ASC";
                ds.Tables[8].Columns.Add(new DataColumn("Title1", System.Type.GetType("System.String"), "Employee_Name + ' - ' + Email"));

                drpToolchk.DataSource = ds.Tables[12];
                drpToolchk.DataTextField = "LanguageName";
                drpToolchk.DataValueField = "ID";
                drpToolchk.DataBind();
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        //used post save to disable all fields including btnsave and btnreset
        public void DisableAllFields(int fromWhere)
        {
            try
            {

                chkWO.Enabled = false;

                if (hidProjectID.Value != "")
                {
                    txtWorkOrderNumber.ReadOnly = true;
                    txtComments.ReadOnly = true;
                }
                else
                {
                    txtWorkOrderNumber.ReadOnly = false;
                    txtComments.ReadOnly = false;
                }
                //txtWorkOrderNumber.Enabled = false;           
                btnCheck.Enabled = false;
                txtPjctName.ReadOnly = true;
                drpMemberFirm.Enabled = false;
                txtPartnerMemfirm.ReadOnly = true;
                txtManagerMemFirm.ReadOnly = true;
                ddlManagerList.Enabled = false;
                txtOthers.ReadOnly = true;
                dtStartDate.Disabled = true;
                drpServiceLine.Enabled = false;
                drpSubServiceLine.Enabled = false;
                drpChargeable.Enabled = false;
                if (fromWhere == 0)
                {
                    drpNatureOfWork.Enabled = false; //making it available after work order is created as well as per request from Sean
                   // drpIdealInvolvement.Enabled = false;//making it available after work order is created as well as per request from Sean
                }

                drpConflictCheck.Enabled = false;
                txtSAPCode.ReadOnly = true;
                txtProjectCode.ReadOnly = true;

                txtMFManagerName.ReadOnly = true;
                txtMFPartnerName.ReadOnly = true;

                btnSave.Enabled = false;
                txtConflictCheck.ReadOnly = true;
                txtEngagementCode.ReadOnly = true;
                drpMemLocation.Enabled = false;
                txtPartnerDIJV.ReadOnly = true;

                drpSector.Enabled = false;
                drpSubSector.Enabled = false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DisableEditViewModeFields()
        {
            try
            {

                //txtPjctName.ReadOnly = true;

                txtProjectCode.ReadOnly = true;
                drpMemberFirm.Enabled = true;

                if (drpChargeable.SelectedValue == "0")//chargeable
                {
                    



                }
                if (drpConflictCheck.SelectedValue == "0")
                {
                    txtConflictCheck.ReadOnly = true;
                }
               

                if (drpMemLocation.Items[drpMemLocation.SelectedIndex].Text == "Others")
                {
                    divOthers.Visible = true;
                }
                if (drpDocument.SelectedValue == "0")
                {
                    divDocument.Visible = true;
                }


                txtWorkOrderNumber.ReadOnly = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //used post save to disable all fields including btnsave and btnreset
        public void EnableFieldsAfterWorkOrderCreated()
        {
            try
            {

                btnSave.Enabled = true;
                txtConflictCheck.ReadOnly = false;
                txtConflictCheck.Enabled = true;

                txtEngagementCode.ReadOnly = false;
                txtEngagementCode.Enabled = true;
                txtPartnerDIJV.ReadOnly = false;
                txtPartnerDIJV.Enabled = true;
                txtPartnerMemfirm.ReadOnly = false;
                txtPartnerMemfirm.Enabled = true;
                txtMFPartnerName.ReadOnly = false;
                txtMFPartnerName.Enabled = true;
                txtMFManagerName.Enabled = true;
                txtMFManagerName.ReadOnly = false;
                txtManagerMemFirm.ReadOnly = false;
                txtManagerMemFirm.Enabled = true;
                txtSAPCode.ReadOnly = false;
                txtSAPCode.Enabled = true;
                dtStartDate.Disabled = false;
                ddlManagerList.Enabled = true;
                txtEngagementCode.Enabled = true;
                txtEngagementCode.ReadOnly = false;
                txtPjctName.ReadOnly = false;
                txtPjctName.Enabled = true;

                drpServiceLine.Enabled = true;
                drpSubServiceLine.Enabled = true;
             //   drpIdealInvolvement.Enabled = true;
                drpNatureOfWork.Enabled = true;
                drpSector.Enabled = true;
                drpSubSector.Enabled = true;
                drpMemberFirm.Enabled = true;
                drpMemLocation.Enabled = true;
                drpConflictCheck.Enabled = true;
                drpChargeable.Enabled = true;

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

        //TSA DROP DOWN LOGIC SUGGESTED BY AISHWARYA  
        public void dropdownToolsKnown(DataSet ds)
        {

           
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                drpToolUse.SelectedValue = ds.Tables[0].Rows[0]["IsToolInUse"].ToString();
                
                if (string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["IsToolInUse"].ToString()) == false)
                {
                    var se3Va31 = ds.Tables[0].Rows[0]["IsToolInUse"].ToString().Split(',');
                    for (int i = 0; i < drpToolUse.Items.Count; i++)
                    {
                        if (se3Va31.Contains(drpToolUse.Items[i].Value) == true)
                        {
                            drpToolUse.Items[i].Selected = true;
                            drpToolchk.Visible = true;
                          

                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["ToolsKnown"].ToString()) == false)
                {
                    var se3Va3 = ds.Tables[0].Rows[0]["ToolsKnown"].ToString().Split(',');
                    for (int i = 0; i < drpToolchk.Items.Count; i++)
                    {
                        if (se3Va3.Contains(drpToolchk.Items[i].Value) == true)
                        {
                            drpToolchk.Items[i].Selected = true;
                        }
                    }
                }
                //}	
            }
        }
        public void LoadProjectData(DataSet ds)
        {
            try
            {

                if (hidProjectID.Value != "")
                {
                    txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectCode"].ToString();
                    txtPjctName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtWorkOrderNumber.Text = ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString() != null ? ds.Tables[0].Rows[0]["WorkOrderNumber"].ToString() : "";

                    drpConcession.SelectedValue = ds.Tables[0].Rows[0]["isConcessionInt"].ToString();
                    txtConcessionRatio.Text = ds.Tables[0].Rows[0]["ConcessionRatio"].ToString();
                    drpConcession_SelectedIndexChanged(drpConcession.Items, e1);
                    drpToolUse.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["IsToolInUse"]).ToString();
                    drpToolUse_SelectedIndexChanged(drpToolUse.Items, e1);
                    dropdownToolsKnown(ds);
                }


                hidConcessionId.Value = ds.Tables[0].Rows[0]["SecondoryProjectID"].ToString();
                hiddrpToolUseId.Value = ds.Tables[0].Rows[0]["SecondoryProjectID"].ToString();
                txtManagerMemFirm.Text = ds.Tables[0].Rows[0]["FirmManager"].ToString();
                txtPartnerMemfirm.Text = ds.Tables[0].Rows[0]["FirmPartner"].ToString();

                txtPartnerDIJV.Text = ds.Tables[0].Rows[0]["IJVPartnerEmail"].ToString();


                drpServiceLine.SelectedValue = ds.Tables[0].Rows[0]["ServiceLine"].ToString();
                drpServiceLine_SelectedIndexChanged(drpServiceLine.Items, e1);

                drpSubServiceLine.SelectedValue = ds.Tables[0].Rows[0]["SubServiceLine"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["SubServiceLine"].ToString();
                drpSubServiceLine_SelectedIndexChanged(drpSubServiceLine.Items, e1);
                drpNatureOfWork.SelectedValue = ds.Tables[0].Rows[0]["NatureOfWork"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["NatureOfWork"].ToString();

                drpChargeable.SelectedValue = ds.Tables[0].Rows[0]["isChargeabletInt"].ToString();
               // drpIdealInvolvement.SelectedValue = ds.Tables[0].Rows[0]["isIdealInvolvementInt"].ToString();

                //Sector dropdown and sunsector
                drpSector.SelectedValue = ds.Tables[0].Rows[0]["Sector"].ToString();
                drpSector_SelectedIndexChanged(drpSector.Items, e1);
                drpSubSector.SelectedValue = ds.Tables[0].Rows[0]["SubSector"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["SubSector"].ToString();


                drpConflictCheck.SelectedValue = ds.Tables[0].Rows[0]["isConflictCheckCompleteInt"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["isConflictCheckCompleteInt"].ToString();

                ///Member firm country and office and others text box
                drpMemberFirm.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                drpMemberFirm_SelectedIndexChanged(drpMemberFirm.Items, e1);
                drpMemLocation.SelectedValue = ds.Tables[0].Rows[0]["FirmLocation"].ToString();
                txtOthers.Text = ds.Tables[0].Rows[0]["Others"].ToString();

                dtStartDate.Value = ds.Tables[0].Rows[0]["startDt"].ToString();

                txtEngagementCode.Text = ds.Tables[0].Rows[0]["EngagementCode"].ToString();
                txtConflictCheck.Text = ds.Tables[0].Rows[0]["ConflictCheckNumber"].ToString();
                txtSAPCode.Text = ds.Tables[0].Rows[0]["SAPCode"].ToString();
                txtPhaseTaskCode.Text = ds.Tables[0].Rows[0]["TaskCode"].ToString();
                chkWO.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isWorkOrderRequired"]);

                txtComments.Text = ds.Tables[0].Rows[0]["Comments"].ToString();
                drpDocument.SelectedValue = ds.Tables[0].Rows[0]["SetUpDocument"].ToString();
                txtReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();

                ddlManagerList.SelectedValue = ds.Tables[0].Rows[0]["IJVManager"].ToString();

                txtMFManagerName.Text = ds.Tables[0].Rows[0]["MFManagerName"].ToString();
                txtMFPartnerName.Text = ds.Tables[0].Rows[0]["MFPartnerName"].ToString();




                valNatureOfWork.Enabled = false;
                lblNatureOfWork.Visible = false;
                DocumentProjectBindGrid();
                dropdownToolsKnown(ds);
                if (hidConcessionId.Value != "" || hiddrpToolUseId.Value != "")
                {
                    // btnSave.Visible = false;	
                }
                else
                {
                    btnSave.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool CheckIfPresent()
        {
            try
            {

                DBHandler dh = new DBHandler();
                bool isValid = dh.ValidateProject(txtPjctName.Text, txtPartnerMemfirm.Text, txtManagerMemFirm.Text, drpMemberFirm.Text, drpMemLocation.Text);
                return false;

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
                if (txtPjctName.Text.Trim() != "")
                {
                    GridView1.DataSource = dbh.Project_Data_SEARCH_RoleBased_EPCC2(txtPjctName.Text, CommonFunctions.GetUser());
                    GridView1.DataBind();
                    if (GridView1.Rows.Count > 0)
                    { GridView1.HeaderRow.TableSection = TableRowSection.TableHeader; }
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }


        private void DocumentProjectBindGrid()
        {
            try
            {
                if ((hidProjectID.Value) != "")
                {
                    GridView1.DataSource = dbh.GetProjectFileData(int.Parse(hidProjectID.Value));
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                DBHandler handler = new DBHandler();

                int id = int.Parse((sender as LinkButton).CommandArgument);
                handler.DeleteProjectFile(id, int.Parse(hidProjectID.Value), out message);
                DocumentProjectBindGrid();
                MsgBox(message);
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {


                int id = int.Parse((sender as LinkButton).CommandArgument);
                byte[] bytes;
                string fileName, contentType;
                DBHandler dh = new DBHandler();
                DataSet ds = new DataSet();
                ds = dh.DownloadProjectFile(id);
                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["Name"].ToString();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                // Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }

        }

        #region Save and Update
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string filename = "";
            string contentType = "";
            byte[] bytes = new byte[0];
            try
            {
                btnSave.Enabled = false;
                string UserId = CommonFunctions.GetUser();


                string message1 = "";
                string generatedCode = "";
                DBHandler handler = new DBHandler();

                if (drpServiceLine.SelectedValue == "1025" )
                {

                filename = Path.GetFileName(FileUpload2.PostedFile.FileName);
                //filename = string.Empty;
                contentType = FileUpload2.PostedFile.ContentType;
                Stream fs = FileUpload2.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                 }


                string __drpTool = "";
                var xxx = drpToolUse;
                for (int i = 0; i < drpToolchk.Items.Count; i++)
                {
                    if (drpToolchk.Items[i].Selected == true)
                    {
                        __drpTool = __drpTool + drpToolchk.Items[i].Value + ",";
                    }
                }
                __drpTool = __drpTool.Trim(',');


                if (hidProjectID.Value != "")
                {
                    string result = handler.Project_Data_UPDATE(int.Parse(hidProjectID.Value),
                        txtPjctName.Text.Trim(),
                        drpMemLocation.Text, drpMemberFirm.Text,
                        txtPartnerMemfirm.Text.Trim(), txtManagerMemFirm.Text.Trim(),
                        txtPartnerDIJV.Text.Trim(), ddlManagerList.Text,
                        dtStartDate.Value.ToString(),
                        drpChargeable.Text, drpServiceLine.Text,
                        drpNatureOfWork.Text != "-1" ? drpNatureOfWork.Text : null,
                        drpSector.Text, drpSubSector.Text != "-1" ? drpSubSector.Text : null,
                        txtEngagementCode.Text, txtPhaseTaskCode.Text,
                        txtSAPCode.Text,
                        drpConflictCheck.Text == "-1" ? null : drpConflictCheck.Text, txtConflictCheck.Text, chkWO.Checked,
                        UserId, txtOthers.Text, txtComments.Text, drpDocument.Text, txtReason.Text, drpSubServiceLine.Text != "-1" ? drpSubServiceLine.Text : null,
                        txtMFPartnerName.Text, txtMFManagerName.Text, drpConcession.Text, txtConcessionRatio.Text, drpToolUse.Text, __drpTool,
                        out message1, filename, contentType, bytes, int.Parse(hidProjectID.Value), out generatedCode);

                    //DocumentProjectBindGrid();
                    if (message1 != "")
                    {
                        btnSave.Enabled = true;

                        MsgBox(message1);

                    }
                    else
                    {
                        MsgBox("Update Successfull");
                        txtProjectCode.Text = generatedCode;
                        DisableAllFields(0);
                        Response.Redirect("~/Allocate.aspx?project=" + txtPjctName.Text.Trim());
                    }



                }
                else if (hidProjectID.Value == "")

                {
                    string prjctID = handler.Project_Data_INSERT(txtPjctName.Text.Trim(), 0,
                                    drpMemLocation.Text, drpMemberFirm.Text,
                                    txtPartnerMemfirm.Text.Trim(),
                                    txtManagerMemFirm.Text.Trim()
                                    , txtPartnerDIJV.Text.Trim(),
                                    ddlManagerList.Text,
                                    dtStartDate.Value.ToString(),
                                    drpChargeable.Text, drpServiceLine.Text,
                                    drpNatureOfWork.Text != "-1" ? drpNatureOfWork.Text : null,
                                    drpSector.Text,
                                    drpSubSector.Text != "-1" ? drpSubSector.Text : null,
                                    txtEngagementCode.Text, txtPhaseTaskCode.Text,
                                    txtSAPCode.Text,

                                    drpConflictCheck.Text == "-1" ? null : drpConflictCheck.Text, txtConflictCheck.Text, chkWO.Checked,
                                    UserId, UserId, txtOthers.Text, txtComments.Text, drpDocument.Text, txtReason.Text, drpSubServiceLine.Text != "-1" ? drpSubServiceLine.Text : null,
                                    txtMFPartnerName.Text, txtMFManagerName.Text, drpConcession.Text, txtConcessionRatio.Text, drpToolUse.Text, __drpTool,
                                    out message1, filename, contentType, bytes, 0, out generatedCode);


                    if (prjctID != "-1")
                    {
                        DocumentProjectBindGrid();
                        MsgBox("Project has been successfully saved. " +
                            "Project number updated in project code and mail to the engagement manager has been sent. For any updates or " +
                            "changes please go to the search page and edit the project.");
                        txtProjectCode.Text = generatedCode;

                        DisableAllFields(0);
                        Response.Redirect("~/Allocate.aspx?project=" + txtPjctName.Text.Trim());
                    }
                    else
                    {
                        btnSave.Enabled = true;

                        MsgBox(message1);
                    }
                }
                else
                {
                    string prjctID = handler.Project_Data_INSERT(txtPjctName.Text.Trim(), 0,
                                    drpMemLocation.Text, drpMemberFirm.Text,
                                    txtPartnerMemfirm.Text.Trim(),
                                    txtManagerMemFirm.Text.Trim()
                                    , txtPartnerDIJV.Text.Trim(),
                                    ddlManagerList.Text,
                                    dtStartDate.Value.ToString(),
                                    drpChargeable.Text, drpServiceLine.Text,
                                    drpNatureOfWork.Text != "-1" ? drpNatureOfWork.Text : null,
                                    drpSector.Text,
                                    drpSubSector.Text != "-1" ? drpSubSector.Text : null,
                                    txtEngagementCode.Text, txtPhaseTaskCode.Text,
                                    txtSAPCode.Text,
                                    drpConflictCheck.Text == "-1" ? null : drpConflictCheck.Text, txtConflictCheck.Text, chkWO.Checked,
                                    UserId, UserId, txtOthers.Text, txtComments.Text, drpDocument.Text, txtReason.Text, drpSubServiceLine.Text != "-1" ? drpSubServiceLine.Text : null,
                                    txtMFPartnerName.Text, txtMFManagerName.Text, drpConcession.Text, txtConcessionRatio.Text, drpToolUse.Text, __drpTool,
                                    out message1, filename, contentType, bytes, int.Parse(hidProjectID.Value ?? "0"), out generatedCode);


                    if (prjctID != "-1")
                    {
                        DocumentProjectBindGrid();
                        MsgBox("Project has been successfully saved. " +
                            "Project number updated in project code and mail to the engagement manager has been sent. For any updates or " +
                            "changes please go to the search page and edit the project.");
                        txtProjectCode.Text = generatedCode;
                        DisableAllFields(0);
                        Response.Redirect("~/Allocate.aspx?project=" + txtPjctName.Text.Trim());
                    }
                }
                Response.Redirect("~/Allocate.aspx?project=" + txtPjctName.Text.Trim());

            }
            catch (Exception ex)
            {
                dbh.SendExcepToDB(ex);
                Server.Transfer("ErrorPage.aspx");
            }
        }
        #endregion


        #region Change Events
        private void GetSAPCode()
        {
            try
            {

                DBHandler handler = new DBHandler();
                if (drpMemberFirm.SelectedValue != "-1" && drpServiceLine.SelectedValue != "-1" && (drpChargeable.SelectedValue == "1" || drpChargeable.SelectedValue == "0"))
                {
                    // riddhi
                    //string code1 = handler.GetSAPCode(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue), Convert.ToInt32(drpChargeable.SelectedValue));
                    string code1 = "";
                    if (drpSubServiceLine.SelectedValue != "-1")
                    {
                        code1 = handler.GetSAPCode(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue),
                            Convert.ToInt32(drpChargeable.SelectedValue), (drpSubServiceLine.SelectedValue), Convert.ToInt32(drpMemLocation.SelectedValue));
                    }
                    if (code1 == "")
                    {
                        code1 = handler.GetSAPCode(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue),
                            Convert.ToInt32(drpChargeable.SelectedValue), null, Convert.ToInt32(drpMemLocation.SelectedValue));

                    }

                    // end
                    //call sp
                    //string code = handler.GetSAPCode(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue), Convert.ToInt32(drpChargeable.SelectedValue));
                    //if(drpSubServiceLine.SelectedValue != "-1")
                    //{
                    //    code = handler.GetSAPCode(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue), Convert.ToInt32(drpChargeable.SelectedValue), Convert.ToInt32(drpSubServiceLine.SelectedValue));
                    //}
                    txtSAPCode.Text = code1.ToString();

                }
                //if (drpChargeable.SelectedValue == "0" && txtSAPCode.Text != "")
                //{
                //    txtSAPCode.Text = string.Empty;
                //}
                if (drpMemberFirm.SelectedValue == "-1" || drpServiceLine.SelectedValue == "-1" || drpChargeable.SelectedValue == "-1")
                {
                    txtSAPCode.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void drpMemberFirm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DBHandler handler = new DBHandler();
                DataTable dt = new DataTable();
                if (drpMemberFirm.SelectedValue != "-1")
                {
                    dt = handler.GetMemberfirmOffices(Convert.ToInt32(drpMemberFirm.SelectedValue));
                    if (dt.Rows.Count > 0)
                    {
                        drpMemLocation.DataSource = dt;
                        drpMemLocation.DataTextField = "Office";
                        drpMemLocation.DataValueField = "Id";
                        drpMemLocation.DataBind();
                        drpMemLocation.Items.Insert(0, new ListItem("--Select--", "-1"));
                        drpMemLocation.Enabled = true;
                        valMemberOffice.Enabled = true;
                        lblFirmOffice.Visible = true;
                        divOthers.Visible = false;
                        divDocument.Visible = false;


                    }
                    else
                    {
                        //drpMemLocation.Enabled = false;

                        valMemberOffice.Enabled = false;
                        lblFirmOffice.Visible = false;

                    }


                }
                else
                {
                    //drpMemLocation.Enabled = false;

                }
                GetSAPCode();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void drpMemLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (drpMemLocation.Items[drpMemLocation.SelectedIndex].Text == "Others")
                {
                    divOthers.Visible = true;
                }
                else
                {
                    divOthers.Visible = false;
                    txtOthers.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void drpConflictCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (drpConflictCheck.SelectedValue == "1")
                {
                    txtConflictCheck.ReadOnly = false;
                    //txtConflictCheck.Enabled = true;
                }
                else
                {
                    txtConflictCheck.ReadOnly = true;
                    //txtConflictCheck.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void drpDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (drpDocument.SelectedValue == "0")
                {
                    divDocument.Visible = true;
                    txtReason.Text = "";
                }
                else
                {
                    divDocument.Visible = false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        protected void drpServiceLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DBHandler handler = new DBHandler();
                DataTable dt = new DataTable();
                GetSAPCode();
                if (drpServiceLine.SelectedValue == "-1") return;

                dt = handler.GetSubServiceLine(int.Parse(drpServiceLine.SelectedValue));
                if (dt.Rows.Count == 0)
                {

                    drpSubServiceLine.SelectedValue = "-1";
                    drpSubServiceLine.Items.Clear();
                    drpSubServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

                    valNatureOfWork.Enabled = false;
                    lblNatureOfWork.Visible = false;
                    drpNatureOfWork.SelectedValue = "-1";
                    drpNatureOfWork.Items.Clear();
                    drpNatureOfWork.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    drpSubServiceLine.DataSource = dt;
                    drpSubServiceLine.DataTextField = "Name";
                    drpSubServiceLine.DataValueField = "SubServiceLineId";
                    drpSubServiceLine.DataBind();
                    drpSubServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

                }


                //Code added by Vijay bhgagat as on 16 March 2020 as per shruti mantri instruction (Only for TS)
                if (drpServiceLine.SelectedValue == "1025")
                {
                    FileUploadDiv.Visible = true;
                    divMainDocument.Visible = false;
                    divConcessionDocument.Visible = false;
                    divAnalyticsTool.Visible = false;
                   
                }
                else if (drpServiceLine.SelectedValue == "1")
                {
                    divMainDocument.Visible = true;
                    divAnalyticsTool.Visible = true;
                    FileUploadDiv.Visible = false;
                    divConcessionDocument.Visible = false;
                 

                }

                else if (drpServiceLine.SelectedValue == "5")
                {
                    divMainDocument.Visible = false;
                    divConcessionDocument.Visible = true;
                    divAnalyticsTool.Visible = false;
                    FileUploadDiv.Visible = false;
                   

                }
                else
                {
                    divConcessionDocument.Visible = false;
                    divMainDocument.Visible = false;
                    divAnalyticsTool.Visible = false;
                    FileUploadDiv.Visible = false;


                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void drpSubServiceLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                DBHandler handler = new DBHandler();
                DataTable dt = new DataTable();
                GetSAPCode();
                if (drpSubServiceLine.SelectedValue != "-1")
                {
                    dt = handler.GetNatureOfWork(int.Parse(drpSubServiceLine.SelectedValue));
                    if (dt.Rows.Count > 0)
                    {
                        drpNatureOfWork.DataSource = dt;
                        drpNatureOfWork.DataTextField = "Name";
                        drpNatureOfWork.DataValueField = "Id";
                        drpNatureOfWork.DataBind();
                        drpNatureOfWork.Items.Insert(0, new ListItem("--Select--", "-1"));
                        drpNatureOfWork.Enabled = true;
                        valNatureOfWork.Enabled = true;
                        lblNatureOfWork.Visible = true;


                    }
                    else
                    {
                        valNatureOfWork.Enabled = false;
                        lblNatureOfWork.Visible = false;
                        drpNatureOfWork.SelectedValue = "-1";
                        drpNatureOfWork.Items.Clear();
                        drpNatureOfWork.Items.Insert(0, new ListItem("--Select--", "-1"));

                        //txtPhaseTaskCode.ReadOnly = true;
                        //txtPhaseTaskCode.Text = string.Empty;
                        //valPhaseCode.Enabled = false;
                        //valPhaseCode.ValidationGroup = string.Empty;
                        //lblPhaseCode.Visible = false;
                    }
                }
                else
                {
                    valNatureOfWork.Enabled = false;
                    lblNatureOfWork.Visible = false;
                    drpNatureOfWork.SelectedValue = "-1";
                    drpNatureOfWork.Items.Clear();
                    drpNatureOfWork.Items.Insert(0, new ListItem("--Select--", "-1"));

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void drpSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                DBHandler handler = new DBHandler();
                DataTable dt = new DataTable();
                if (drpSector.SelectedValue != "-1")
                {
                    dt = handler.GetSubSector(Convert.ToInt32(drpSector.SelectedValue));
                    if (dt.Rows.Count > 0)
                    {
                        drpSubSector.DataSource = dt;
                        drpSubSector.DataTextField = "Name";
                        drpSubSector.DataValueField = "Id";
                        drpSubSector.DataBind();
                        drpSubSector.Items.Insert(0, new ListItem("--Select--", "-1"));
                        drpSubSector.Enabled = true;
                        valSubSector.Enabled = true;
                        lblSubSector.Visible = true;


                    }
                    else
                    {
                        //drpSubSector.Enabled = false;
                        valSubSector.Enabled = false;
                        drpSubSector.SelectedValue = "-1";
                        drpSubSector.Items.Clear();
                        drpSubSector.Items.Insert(0, new ListItem("--Select--", "-1"));

                        lblSubSector.Visible = false;

                    }
                }
                else
                {
                    //drpSubSector.Enabled = false;
                    valSubSector.Enabled = false;
                    drpSubSector.SelectedValue = "-1";
                    lblSubSector.Visible = false;

                    drpSubSector.Items.Clear();
                    drpSubSector.Items.Insert(0, new ListItem("--Select--", "-1"));

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void drpChargeable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drpChargeable.SelectedValue == "0" || drpChargeable.SelectedValue == "-1")
            //{
            //    drpConflictCheck.SelectedValue = "-1";
            //    lblConflict.Visible = false;
            //    txtConflictCheck.Text = string.Empty;
            //    //txtConflictCheck.Enabled = false;
            //    txtConflictCheck.ReadOnly = true;

            //    //drpConflictCheck.Enabled = false;
            //    valConflictCheck.Enabled = false;
            //    drpConflictCheck.Items.Clear();
            //    drpConflictCheck.Items.Insert(0, new ListItem("--Select--", "-1"));

            //}
            //else
            //{
            //    drpConflictCheck.Enabled = true;
            //    drpConflictCheck.Items.Clear();
            //    drpConflictCheck.Items.Insert(0, new ListItem("--Select--", "-1"));

            //    drpConflictCheck.Items.Insert(1, new ListItem("Y", "1"));
            //    drpConflictCheck.Items.Insert(2, new ListItem("N", "0"));


            //    lblConflict.Visible = true;
            //    valConflictCheck.Enabled = true;

            //}
            GetSAPCode();
        }

        protected void drpConcession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpConcession.SelectedValue == "1")
                {
                    lblConcession.Visible = true;
                    txtConcessionRatio.Visible = true;
                }
                else
                {
                    drpConcession.SelectedValue = "0";
                    txtConcessionRatio.Text = "0";
                    lblConcession.Visible = false;
                    txtConcessionRatio.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TSA DROP DOWN LOGIC SUGGESTED BY AISHWARYA  
        protected void drpToolUse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (drpToolUse.SelectedValue == "1"  )
                {
                    
                    divshowToolKnown.Visible = true;
                    lblToolKnown.Visible = true;
                    drpToolchk.Visible = true;
                  
                    
                }

                else if(drpToolUse.SelectedValue == "-1")
                {
                    divshowToolKnown.Visible = false;
                    lblToolKnown.Visible = false;
                    drpToolchk.Visible = false;
                 }
                else
                {
                    drpToolUse.SelectedValue = "0";
                    drpToolchk.SelectedValue = null;
                    divshowToolKnown.Visible = false;
                    lblToolKnown.Visible = false;
                    drpToolchk.Visible = false;


                 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        /// <summary>
        /// Checks if the project Name, member firm location and e-mail combination already exists or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                #region Get data from all fields and create necesaary objects
                var strProjectName = txtPjctName.Text;
                var strlocationValue = drpMemberFirm.SelectedValue;
                var strEMail = txtPartnerMemfirm.Text;
                int intlocation;
                //var regEx = RegularExpressionValidator1.ValidationExpression;
                var regEx = "";
                var strMessage = string.Empty;
                #endregion

                #region Validate input data and read returned value from database and set the return message accordingly
                if (int.TryParse(strlocationValue, out intlocation) && intlocation != -1
                && !string.IsNullOrEmpty(strProjectName) && !string.IsNullOrWhiteSpace(strProjectName)
                && !string.IsNullOrEmpty(strEMail) && !string.IsNullOrWhiteSpace(strEMail)
                && (System.Text.RegularExpressions.Regex.Match(strEMail, regEx)).Success)
                {
                    var dtProjectList = new DBHandler().CheckDuplicateRecordForProjectData(projectName: strProjectName, location: intlocation, eMail: strEMail);
                    // if rows exists then display the modal popup else show alert message
                    if (dtProjectList.Rows.Count > 0)
                    {
                        gvProjectList.DataSource = dtProjectList;
                        gvProjectList.DataBind();
                        ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
                    }
                    else
                    {
                        gvProjectList.DataSource = null;
                        gvProjectList.DataBind();
                        strMessage = "No match found. You can continue...";
                    }
                }
                else
                    strMessage = "Please enter project name, member firm and partner email properly to check for duplicates.";
                #endregion

                #region Display the return message to the end user
                if (strMessage != string.Empty)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "alert('" + strMessage + "');", true);
                #endregion


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}


