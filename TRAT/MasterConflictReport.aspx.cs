using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAT
{
    public partial class MasterConflictReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AssignDates();
                LoadMasters();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AssignDates()
        {

            if (DateTime.Today.Month < 6)
            {

                dtStartDate.Value = (DateTime.Today.Year - 1) + "-06-01";
                dtEndDate.Value = DateTime.Today.Year + "-05-31";
            }
            else
            {

                dtStartDate.Value = (DateTime.Today.Year) + "-06-01";
                dtEndDate.Value = (DateTime.Today.Year + 1) + "-05-31";
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
                drpServiceLine.Items.Insert(0, new ListItem("All", "-1"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}