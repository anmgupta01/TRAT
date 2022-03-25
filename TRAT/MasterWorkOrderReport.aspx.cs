using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterWorkOrderReport : System.Web.UI.Page
    {
        DBHandler dbh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                LoadMaster();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadMaster()
        {
            try
            {

                DataSet ds = new DataSet();
                ds = dbh.GetMasters();

                drpServiceLine.DataSource = ds.Tables[2];
                drpServiceLine.DataTextField = "Name";
                drpServiceLine.DataValueField = "Id";
                drpServiceLine.DataBind();
                //drpServiceLine.Items.Insert(0, new ListItem("--Select--", "-1"));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnDownloadReport_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = new DataSet();
                string message = "";
                ds = dbh.GetOverViewReport(int.Parse(drpServiceLine.SelectedValue));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}