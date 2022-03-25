using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterUpdateProjectDetails : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        EventArgs e1 = new EventArgs();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                dvProjectData.Visible = false;
                gvProjects.EditIndex = -1;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSearchProject_Click(object sender, EventArgs e)
        {

            bindGrid();
        }

        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string userid = CommonFunctions.GetUser();
                if (e.CommandName == "Edit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string projectId = this.gvProjects.DataKeys[rowIndex][0].ToString();
                    LoadProjectData(projectId);
                    dvProjectData.Visible = true;
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
                    gvProjects.DataSource = dbh.Project_Data_SEARCH(txtProjectName.Text.Trim());
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

        void LoadProjectData(string projectID)
        {
            try
            {

                LoadMasters();
                DataSet ds = new DataSet();
                ds = dbh.GetProjectData(int.Parse(projectID));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtPjctName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    drpServiceLine.SelectedValue = ds.Tables[0].Rows[0]["ServiceLine"].ToString();
                    drpServiceLine_SelectedIndexChanged(drpServiceLine.Items, e1);

                    drpSubServiceLine.SelectedValue = ds.Tables[0].Rows[0]["SubServiceLine"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["SubServiceLine"].ToString();
                    drpSubServiceLine_SelectedIndexChanged(drpSubServiceLine.Items, e1);
                    drpNatureOfWork.SelectedValue = ds.Tables[0].Rows[0]["NatureOfWork"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["NatureOfWork"].ToString();

                    drpChargeable.SelectedValue = ds.Tables[0].Rows[0]["isChargeabletInt"].ToString();
                    drpIdealInvolvement.SelectedValue = ds.Tables[0].Rows[0]["isIdealInvolvementInt"].ToString();

                    //Sector dropdown and sunsector
                    drpSector.SelectedValue = ds.Tables[0].Rows[0]["Sector"].ToString();
                    drpSector_SelectedIndexChanged(drpSector.Items, e1);
                    //drpSubSector.SelectedValue = ds.Tables[0].Rows[0]["SubSector"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["SubSector"].ToString();
                    drpMemberFirm.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                    drpMemberFirm_SelectedIndexChanged(drpMemberFirm.Items, e1);
                    drpMemLocation.SelectedValue = ds.Tables[0].Rows[0]["FirmLocation"].ToString();
                    txtOthers.Text = ds.Tables[0].Rows[0]["Others"].ToString();

                    dtStartDate.Value = ds.Tables[0].Rows[0]["startDt"].ToString();

                    txtManagerMemFirm.Text = ds.Tables[0].Rows[0]["FirmManager"].ToString();
                    txtPartnerMemfirm.Text = ds.Tables[0].Rows[0]["FirmPartner"].ToString();
                    txtMFManagerName.Text = ds.Tables[0].Rows[0]["MFManagerName"].ToString();
                    txtMFPartnerName.Text = ds.Tables[0].Rows[0]["MFPartnerName"].ToString();
                    txtPartnerDIJV.Text = ds.Tables[0].Rows[0]["IJVPartnerEmail"].ToString();
                    ddlManagerList.SelectedValue = ds.Tables[0].Rows[0]["IJVManager"].ToString();

                    txtEngagementCode.Text = ds.Tables[0].Rows[0]["EngagementCode"].ToString();
                    drpConflictCheck.SelectedValue = ds.Tables[0].Rows[0]["isConflictCheckCompleteInt"].ToString() == null ? "-1" : ds.Tables[0].Rows[0]["isConflictCheckCompleteInt"].ToString();
                    txtConflictCheck.Text = ds.Tables[0].Rows[0]["ConflictCheckNumber"].ToString();
                    txtSAPCode.Text = ds.Tables[0].Rows[0]["SAPCode"].ToString();
                    txtPhaseTaskCode.Text = ds.Tables[0].Rows[0]["TaskCode"].ToString();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        void LoadMasters()
        {
            try
            {

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

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
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

                if (drpServiceLine.SelectedValue == "2" || drpServiceLine.SelectedValue == "4")
                {
                    txtPhaseTaskCode.Enabled = true;
                    txtPhaseTaskCode.ReadOnly = false;
                    valPhaseCode.Enabled = true;
                    lblPhaseCode.Visible = true;
                }
                else
                {
                    txtPhaseTaskCode.ReadOnly = true;
                    txtPhaseTaskCode.Text = string.Empty;
                    valPhaseCode.Enabled = false;
                    valPhaseCode.ValidationGroup = string.Empty;
                    lblPhaseCode.Visible = false;
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
                    dt = handler.GetSubSector(drpSector.SelectedIndex);
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
            try
            {

                if (drpChargeable.SelectedValue == "0" || drpChargeable.SelectedValue == "-1")
                {
                    drpConflictCheck.SelectedValue = "-1";
                    lblConflict.Visible = false;
                    txtConflictCheck.Text = string.Empty;
                    //txtConflictCheck.Enabled = false;
                    txtConflictCheck.ReadOnly = true;

                    //drpConflictCheck.Enabled = false;
                    valConflictCheck.Enabled = false;
                    drpConflictCheck.Items.Clear();
                    drpConflictCheck.Items.Insert(0, new ListItem("--Select--", "-1"));

                }
                else
                {
                    drpConflictCheck.Enabled = true;
                    drpConflictCheck.Items.Clear();
                    drpConflictCheck.Items.Insert(0, new ListItem("--Select--", "-1"));

                    drpConflictCheck.Items.Insert(1, new ListItem("Y", "1"));
                    drpConflictCheck.Items.Insert(2, new ListItem("N", "0"));


                    lblConflict.Visible = true;
                    valConflictCheck.Enabled = true;

                }
                GetSAPCode();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}