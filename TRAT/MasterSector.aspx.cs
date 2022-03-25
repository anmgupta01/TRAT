using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class AddSectorSubSector : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    var intRole = CommonFunctions.GetUseRole(CommonFunctions.GetUser());
                    if (intRole != 5 && intRole != 2)
                        Response.Redirect("~/Home.aspx", true);
                    else
                        bindGrid();
                }
                alert.Visible = false;
                gvProjects.EditIndex = -1;
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

                DBHandler handler = new DBHandler();
                DataSet ds = new DataSet();
                ds = handler.GetMasters();
                gvProjects.DataSource = ds.Tables[0];
                gvProjects.DataBind();
                if (gvProjects.Rows.Count > 0)
                { gvProjects.HeaderRow.TableSection = TableRowSection.TableHeader; }
                ViewState["CurrentTable"] = ds.Tables[0];

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtSectorName.Text.Trim() != "")
                {
                    lblAlert.InnerText = dbh.MasterSector("0", txtSectorName.Text, "0", CommonFunctions.GetUser());
                    lblAlert.Visible = true;
                    bindGrid();
                    txtSectorName.Text = string.Empty;
                }
                else
                {
                    lblAlert.InnerText = "Enter the name of the sector!";
                    alert.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                }

                if (e.CommandName == "Delete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    bindGrid();
                    alert.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                lblAlert.InnerText = dbh.MasterSector(e.Keys["Id"].ToString(), e.Values["Name"].ToString(), "1", CommonFunctions.GetUser());
                gvProjects.EditIndex = -1;
                bindGrid();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                gvProjects.EditIndex = e.NewEditIndex;
                bindGrid();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvProjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvProjects.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvProjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string newval = e.NewValues["Name"].ToString();
                lblAlert.InnerText = dbh.MasterSector(e.Keys["Id"].ToString(), newval, "0", CommonFunctions.GetUser());
                alert.Visible = true;
                gvProjects.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();

        }
        private void AddNewRowToGrid()
        {
            try
            {

                int rowIndex = 0;

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            //extract the TextBox values
                            TextBox box1 = (TextBox)gvProjects.Rows[rowIndex].Cells[1].FindControl("txtSectorName");

                            drCurrentRow = dtCurrentTable.NewRow();
                            //drCurrentRow["Name"] = i + 1;

                            //dtCurrentTable.Rows[i - 1]["Name"] = box1!=null ? box1.Text : "";

                            rowIndex++;
                        }
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;

                        gvProjects.DataSource = dtCurrentTable;
                        gvProjects.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Set Previous Data on Postbacks
            //SetPreviousData();
        }
        protected void Insert(object sender, EventArgs e)
        {
            try
            {


                ///SqlDataSource1.Insert();
                bindGrid();
                //txtName.Text = string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}