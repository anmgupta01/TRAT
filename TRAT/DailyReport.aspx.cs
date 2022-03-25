using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;

namespace TRAT
{
    public partial class DailyReport : System.Web.UI.Page
    {
        DBHandler dh = new DBHandler();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void MsgBox(string message)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + message + "');";
            msg += "</script>";
            Response.Write(msg);
        }

        protected void btnDownloadReport_Click(object sender, EventArgs e)
        {
            try
            {


                int reportType = 0;

                int projectId = -1;



                DataSet ds = new DataSet();
                string message = "";
                ds = dh.GetDailyReport(dtStartDate.Value.ToString(), dtEndDate.Value.ToString(), out message);
                if (message != null && message != "")
                {
                    MsgBox(message);
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ExportToExcel(ds.Tables[0]);
                }
                else
                {
                    alert.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        void ExportToExcel(DataTable dt)
        {
            try
            {


                if (dt.Rows.Count > 0)
                {
                    string filename = "Workorder Summary Data.xls";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    DataGrid dgGrid = new DataGrid();
                    dgGrid.DataSource = dt;
                    dgGrid.DataBind();
                    //dgGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
                    //Get the HTML for the control.
                    dgGrid.RenderControl(hw);
                    //Write the HTML back to the browser.
                    //Response.ContentType = application/vnd.ms-excel;

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");

                    this.EnableViewState = false;
                    Response.Write(tw.ToString());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}