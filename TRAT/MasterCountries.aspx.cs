using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        DBHandler handler = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                alert.Visible = false;
                if (!IsPostBack)
                {
                    if (CommonFunctions.GetUseRole(CommonFunctions.GetUser()) != 5)
                        Response.Redirect("~/Home.aspx", true);
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


                if (txtCountryName.Text.Trim() != "")
                {
                    DataSet ds = new DataSet();
                    ds = handler.MasterCountriesSearch(txtCountryName.Text.Trim());
                    gvCountry.DataSource = ds.Tables[0];
                    //gvCountry.EditIndex = -1;
                    gvCountry.DataBind();
                    if (gvCountry.Rows.Count > 0)
                    { gvCountry.HeaderRow.TableSection = TableRowSection.TableHeader; }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvCountry.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string Response = handler.MasterCountries(e.Keys["Id"].ToString(), "", "1", CommonFunctions.GetUser());
                lblAlert.InnerText = Response;
                alert.Visible = true;
                gvCountry.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCountry_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvCountry.EditIndex = e.NewEditIndex;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCountry_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string name = e.NewValues["Name"].ToString().Trim();
                if (name != "")
                {
                    string Response = handler.MasterCountries(e.Keys["Id"].ToString(), name, "0", CommonFunctions.GetUser());
                    lblAlert.InnerText = Response;
                    alert.Visible = true;
                    gvCountry.EditIndex = -1;
                    bindGrid();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvCountry_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            // bindGrid();

        }

        protected void gvCountry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvCountry.EditIndex = -1;
                bindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewName.Text = txtNewName.Text.Trim();
                if (txtNewName.Text != "")
                {
                    string Response = handler.MasterCountries("0", txtNewName.Text, "0", CommonFunctions.GetUser());
                    lblAlert.InnerText = Response;
                    alert.Visible = true;
                    divAddNew.Visible = false;
                    divSearch.Visible = true;
                    txtCountryName.Text = txtNewName.Text;
                    gvCountry.EditIndex = -1;
                    bindGrid();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // string Response = handler.MasterCountries("0",txt, "0", CommonFunctions.GetUser());
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                divAddNew.Visible = true;
                divSearch.Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnAddBack_Click(object sender, EventArgs e)
        {
            try
            {

                divAddNew.Visible = false;
                divSearch.Visible = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}