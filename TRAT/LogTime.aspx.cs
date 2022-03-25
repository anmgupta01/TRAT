using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace TRAT
{
    public static class PageExtensionMethods
    {
        public static Control FindControlRecursive(this Control ctrl, string controlID)
        {
            if (string.Compare(ctrl.ID, controlID, true) == 0)
            {
                // We found the control!
                return ctrl;
            }
            else
            {
                // Recurse through ctrl's Controls collections
                foreach (Control child in ctrl.Controls)
                {
                    Control lookFor = FindControlRecursive(child, controlID);

                    if (lookFor != null)
                        return lookFor;  // We found the control
                }

                // If we reach here, control was not found
                return null;
            }
        }
    }

    public partial class LogTime : System.Web.UI.Page
    {
        DBHandler dh = new DBHandler();
        public string[] dates = new string[7]; // = new DateTime();
        public DataTable dt = new DataTable();
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
                    calDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    bindTimesheet("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable MapTimeSheetTable(DataTable Source)
        {
            try
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("Project");
                DataColumn dc0 = new DataColumn("Project");
                dt.Columns.Add("D1");
                dt.Columns.Add("D2");
                dt.Columns.Add("D3");
                dt.Columns.Add("D4");
                dt.Columns.Add("D5");
                dt.Columns.Add("D6");
                dt.Columns.Add("D7");
                dt.Columns.Add("D1Flag");
                dt.Columns.Add("D2Flag");
                dt.Columns.Add("D3Flag");
                dt.Columns.Add("D4Flag");
                dt.Columns.Add("D5Flag");
                dt.Columns.Add("D6Flag");
                dt.Columns.Add("D7Flag");
                dt.Columns.Add("PID");
                //dt.Columns.Add("PDate");

                dates = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    dates[i] = Source.Columns[i + 2].ColumnName.Replace(",", @"<br />");
                }

                foreach (DataRow datarow in Source.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = datarow[0] + "-" + datarow[9];
                    dr[1] = datarow[2].ToString().Split('|')[0];
                    dr[2] = datarow[3].ToString().Split('|')[0];
                    dr[3] = datarow[4].ToString().Split('|')[0];
                    dr[4] = datarow[5].ToString().Split('|')[0];
                    dr[5] = datarow[6].ToString().Split('|')[0];
                    dr[6] = datarow[7].ToString().Split('|')[0];
                    dr[7] = datarow[8].ToString().Split('|')[0];

                    dr[8] = datarow[2].ToString() != "" ? (datarow[2].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[9] = datarow[3].ToString() != "" ? (datarow[3].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[10] = datarow[4].ToString() != "" ? (datarow[4].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[11] = datarow[5].ToString() != "" ? (datarow[5].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[12] = datarow[6].ToString() != "" ? (datarow[6].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[13] = datarow[7].ToString() != "" ? (datarow[7].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[14] = datarow[8].ToString() != "" ? (datarow[8].ToString().Split('|')[1] == "1" ? "True" : "False") : "False";
                    dr[15] = datarow[0];
                    dt.Rows.Add(dr);

                    SetValuesForComments(datarow);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal void SetValuesForComments(DataRow datarow)
        {
            try
            {

                HiddenField hfMonday = new HiddenField()
                {
                    ID = datarow[0] + "-Monday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[2].ToString()) && !string.IsNullOrWhiteSpace(datarow[2].ToString()) ? datarow[2].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfMonday);
                HiddenField hfTuesday = new HiddenField()
                {
                    ID = datarow[0] + "-Tuesday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[3].ToString()) && !string.IsNullOrWhiteSpace(datarow[3].ToString()) ? datarow[3].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfTuesday);
                HiddenField hfWednesday = new HiddenField()
                {
                    ID = datarow[0] + "-Wednesday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[4].ToString()) && !string.IsNullOrWhiteSpace(datarow[4].ToString()) ? datarow[4].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfWednesday);
                HiddenField hfThursday = new HiddenField()
                {
                    ID = datarow[0] + "-Thursday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[5].ToString()) && !string.IsNullOrWhiteSpace(datarow[5].ToString()) ? datarow[5].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfThursday);
                HiddenField hfFriday = new HiddenField()
                {
                    ID = datarow[0] + "-Friday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[6].ToString()) && !string.IsNullOrWhiteSpace(datarow[6].ToString()) ? datarow[6].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfFriday);
                HiddenField hfSaturday = new HiddenField()
                {
                    ID = datarow[0] + "-Saturday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[7].ToString()) && !string.IsNullOrWhiteSpace(datarow[7].ToString()) ? datarow[7].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfSaturday);
                HiddenField hfSunday = new HiddenField()
                {
                    ID = datarow[0] + "-Sunday",
                    ClientIDMode = ClientIDMode.Static,
                    Value = !string.IsNullOrEmpty(datarow[8].ToString()) && !string.IsNullOrWhiteSpace(datarow[8].ToString()) ? datarow[8].ToString().Split('|')[2] : string.Empty
                };
                hiddenControlContainer.Controls.Add(hfSunday);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {

                alert.Visible = false;
                Button btn = (Button)sender;
                bindTimesheet(btn.ID);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        void bindTimesheet(string ID)
        {
            try
            {
                DateTime date = new DateTime();
                date = DateTime.Parse(calDate.Value);
                if (date.DayOfWeek != DayOfWeek.Monday)
                {
                    int delta = DayOfWeek.Monday - date.DayOfWeek;
                    if (delta > 0)
                        delta -= 7;
                    date = date.AddDays(delta);
                    if (ID == "btnPrev")
                    {
                        date = date.AddDays(-7);
                    }
                    if (ID == "btnNext")
                    {
                        date = date.AddDays(7);
                    }
                    calDate.Value = date.ToString("yyyy-MM-dd");
                }
                else
                {
                    if (ID == "btnPrev")
                    {
                        date = date.AddDays(-7);
                    }
                    if (ID == "btnNext")
                    {
                        date = date.AddDays(7);
                    }
                    calDate.Value = date.ToString("yyyy-MM-dd");
                }
                //string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
                //user = user + "@deloitte.com";
                dt = dh.sp_GET_Timesheet_Data(date.ToString("dd-MMM-yyyy"), CommonFunctions.GetUser()).Tables[0];
              //  dt = dh.sp_GET_Timesheet_Data(date.ToString("dd-MMM-yyyy"), "gawagh@deloitte.com").Tables[0];

                gvMain.DataSource = MapTimeSheetTable(dt);
                gvMain.DataBind();
                if (gvMain.Rows.Count > 0)
                {
                    gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvMain.FooterRow.TableSection = TableRowSection.TableFooter;
                    hrHasData.Visible = true;
                    HfStartDate.Value = date.ToString("dd-MMM-yyyy");
                }
                else
                {
                    hrHasData.Visible = false;
                    HfStartDate.Value = "";
                }
            }
            catch (Exception e) { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("ProjectCode", typeof(int));
                dt.Columns.Add("EmpEmail", typeof(String));
                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("Hours", typeof(decimal));
                dt.Columns.Add("Comment", typeof(string));
                //dt.Columns.Add("IsEnabled", typeof(bool));

                string cell1 = gvMain.HeaderRow.Cells[0].Text;



                foreach (GridViewRow row in gvMain.Rows)
                {
                    DateTime startDate = Convert.ToDateTime(HfStartDate.Value);
                    //DataRow row1 = dt.NewRow();
                    for (int j = 1; j < gvMain.Columns.Count; j++)
                    {
                        DataRow row1 = dt.NewRow();
                        string id = "d" + j;

                        if (string.IsNullOrEmpty(((TextBox)row.Cells[j].FindControl(id)).Text))
                        {
                            startDate = (startDate).AddDays(1);
                            continue;
                        }


                        int ID = Convert.ToInt32(gvMain.DataKeys[row.RowIndex].Value); // value of the datakey
                                                                                       //row1[0] = row.Cells[0].Text;
                        row1[0] = ID;

                        row1[1] = CommonFunctions.GetUser();

                        row1[2] = startDate.ToString("yyyy-MM-dd");
                        // gvMain.HeaderRow.Cells[j].Text;//(((TableCell)(gvMain.HeaderRow.Cells[1])).).            ((Label)(gvMain.HeaderRow.Cells[1].FindControl("lblDate1"))).Text    ""     string
                        row1[3] = ((TextBox)row.Cells[j].FindControl(id)).Text;
                        row1[4] = Page.Request.Form["ctl00$formSection$" + ID + "-" + startDate.DayOfWeek];

                        dt.Rows.Add(row1);
                        startDate = (startDate).AddDays(1);
                    }

                }
                InsertTimesheetData(dt); // code changed by sidd
                bindTimesheet("");
                alert.Visible = true;
            }
            catch (Exception ex)
            {

                Server.Transfer("ErrorPage.aspx");
            }
        }
        public void InsertTimesheetData(DataTable dt)
        {
            try
            {
                string message = "";
                new DBHandler().TimeSheetData_INSERT(dt, "0", CommonFunctions.GetUser(), out message);
                alert.InnerText = message;

            }
            catch (Exception ex)
            {

                Server.Transfer("ErrorPage.aspx");
            }

        }
    }
}
