using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MAsterSAPCode : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    LoadMasters();
                }
                alert.Visible = false;
                gvSapCodes.EditIndex = -1;
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

                drpServiceLine.DataSource = ds.Tables[2];
                drpServiceLine.DataTextField = "Name";
                drpServiceLine.DataValueField = "Id";
                drpServiceLine.DataBind();
                drpServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

                drpSubServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

                drpMemberFirm.DataSource = ds.Tables[4];
                drpMemberFirm.DataTextField = "Name";
                drpMemberFirm.DataValueField = "Id";
                drpMemberFirm.DataBind();
                drpMemberFirm.Items.Insert(0, new ListItem("--Select--", "-1"));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetSAPCode()
        {
            try
            {

                DBHandler handler = new DBHandler();
                if (drpMemberFirm.SelectedValue != "-1" && drpServiceLine.SelectedValue != "-1" && (drpChargeable.SelectedValue == "1" || drpChargeable.SelectedValue == "0"))
                {

                    DataSet ds = null;
                    if (drpSubServiceLine.SelectedValue != "-1")
                    {
                        ds = handler.GetSAPCodeForMasterPage(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue),
                            Convert.ToInt32(drpChargeable.SelectedValue), (drpSubServiceLine.SelectedValue), Convert.ToInt32(drpMemLocation.SelectedValue));
                    }
                    if (ds == null || ds.Tables == null)
                    {
                        ds = handler.GetSAPCodeForMasterPage(Convert.ToInt32(drpMemberFirm.SelectedValue), Convert.ToInt32(drpServiceLine.SelectedValue),
                            Convert.ToInt32(drpChargeable.SelectedValue), null, Convert.ToInt32(drpMemLocation.SelectedValue));

                    }
                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        gvSapCodes.DataSource = ds;
                        gvSapCodes.DataBind();
                    }
                    else
                    {
                        gvSapCodes.DataSource = null;
                        gvSapCodes.DataBind();
                    }
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


                }
                else
                {
                    drpSubServiceLine.DataSource = dt;
                    drpSubServiceLine.DataTextField = "Name";
                    drpSubServiceLine.DataValueField = "SubServiceLineId";
                    drpSubServiceLine.DataBind();
                    drpSubServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

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
                GetSAPCode();


                if (drpMemberFirm.SelectedValue != "-1")
                {
                    if (drpMemberFirm.SelectedValue != "151")
                    {
                        drpMemLocation.Enabled = false;
                        drpMemLocation.Items.Insert(0, new ListItem("--Select--", "-1"));
                    }
                    else
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

                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void drpMemLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSAPCode();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtSapCode.Text.Trim() != "")
                {
                    lblAlert.InnerText = dbh.MasterSapCode("0", drpMemberFirm.SelectedValue, drpMemLocation.SelectedValue,
                        drpServiceLine.SelectedValue, drpChargeable.SelectedValue, drpSubServiceLine.SelectedValue, txtSapCode.Text, "0", CommonFunctions.GetUser());
                    GetSAPCode();
                    alert.Visible = true;

                    txtSapCode.Text = string.Empty;
                }
                else
                {
                    lblAlert.InnerText = "Please enter the SAP Code!";
                    alert.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvSapCodes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string userid = CommonFunctions.GetUser();
                if (e.CommandName == "Edit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string projectId = this.gvSapCodes.DataKeys[rowIndex][0].ToString();
                }

                if (e.CommandName == "Delete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    GetSAPCode();
                    alert.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvSapCodes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                //lblAlert.InnerText = dbh.MasterNatureOfWork(e.Keys["Id"].ToString(), drpServiceLine.SelectedValue, e.Values["Name"].ToString(), "1", CommonFunctions.GetUser());
                lblAlert.InnerText = dbh.MasterSapCode(e.Keys["Id"].ToString(), drpMemberFirm.SelectedValue, drpMemLocation.SelectedValue,
                        drpServiceLine.SelectedValue, drpChargeable.SelectedValue, drpSubServiceLine.SelectedValue, e.Values["ChargeCode"].ToString(), "1", CommonFunctions.GetUser());
                gvSapCodes.EditIndex = -1;
                GetSAPCode();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvSapCodes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                gvSapCodes.EditIndex = e.NewEditIndex;
                GetSAPCode();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvSapCodes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {


                gvSapCodes.EditIndex = -1;
                GetSAPCode();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvSapCodes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {


                string newval = e.NewValues["ChargeCode"].ToString();
                //lblAlert.InnerText = dbh.MasterNatureOfWork(e.Keys["Id"].ToString(), drpSubServiceLine.SelectedValue, newval, "0", CommonFunctions.GetUser());
                lblAlert.InnerText = dbh.MasterSapCode(e.Keys["Id"].ToString(), drpMemberFirm.SelectedValue, drpMemLocation.SelectedValue,
                        drpServiceLine.SelectedValue, drpChargeable.SelectedValue, drpSubServiceLine.SelectedValue, newval, "0", CommonFunctions.GetUser());
                alert.Visible = true;
                gvSapCodes.EditIndex = -1;
                GetSAPCode();
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

                GetSAPCode();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}