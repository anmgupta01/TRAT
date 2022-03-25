using System;
using System.Data;
using System.Data.SqlClient;

namespace TRAT
{
    internal class DBHandler
    {
        private static String exepurl;
        DBConnect dc = new DBConnect();
        #region Common Functions
        public string Validate_AdminByEmail(int id, string email, out string timesheetRole)
        {
            try
            {
                string message = "";
                SqlCommand scom = new SqlCommand("Validate_AdminByEmail");
                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.Add("@Role", SqlDbType.Int, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.Add("@TimesheetRole", SqlDbType.Bit, 4000).Direction = ParameterDirection.Output;


                scom.Parameters.AddWithValue("@Email", email);

                string result = dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Role"].Value.ToString();
                timesheetRole = scom.Parameters["@TimesheetRole"].Value.ToString();

                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSingleEmployeeDetails(string id, string email)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("EmployeeDetails_GET");
                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.AddWithValue("@id", id);
                scom.Parameters.AddWithValue("@email", email);


                return dt = dc.GetDataSetUsingCommand(scom).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Masters
        public DataSet GetMasters()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Masters_GET");
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetMastersEmployeeSrMngrDirector()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Masters_GET_Employee_SrMngrDirector");
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_Sectors_Masters()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Masters_GET_Sectors");
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetUserRoles()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("UserRoles_GET");
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetUserId()
        {
            try
            {
                SqlCommand scom = new SqlCommand("Validate_Email");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Email", CommonFunctions.GetUser());
                scom.Parameters.Add("@Id", SqlDbType.Int);
                scom.Parameters["@Id"].Direction = ParameterDirection.Output;
                dc.ExecuteCommand(scom);
                return (int)scom.Parameters["@Id"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateEmail(string emailId)
        {
            try
            {
                SqlCommand scom = new SqlCommand("Validate_Email");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Email", emailId);
                scom.Parameters.Add("@Id", SqlDbType.Int);
                scom.Parameters["@Id"].Direction = ParameterDirection.Output;
                dc.ExecuteCommand(scom);
                if ((int)scom.Parameters["@Id"].Value == -1)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetNatureOfWork(int Id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("NatureOfWork_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetMemberfirmOffices(int Id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("MemberFirmOffices_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSubSector(int Id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("SubSector_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSubDepartment(int Id, string depatmentName)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("SubDepartment_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@DepartmentName", depatmentName);

                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeesData(string name)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Employee_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ServiceLine", name);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetSubServiceLine(int Id)
        {
            try
            {
                SqlCommand scom = new SqlCommand("SubServiceLine_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                return dc.GetDataSetUsingCommand(scom).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  Project Form
        public bool ValidateProject(string projectName, string partnerMF, string managerMF, string mfCountry, string mfLocation)
        {
            return false;
        }
        public DataSet GetProjectData(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Project_Data_INSERT(string prjctName, int projectcode, string memberLoc, string memberCountry, string partnerFirm, string managerMemberFirm, string partnerDIJV,
             string managerDIJV, string projectDate, string chargeableNon, string serviceLine, string natureOfWork, string sector,
             string subSector, string engCode, string phaseCode, string sapCode,
             //decimal budgetHours, 
             string conflictCheck, string conflictcheckNumber, bool isWORequired,
             string createdby, string lastupdatedby, string others, string comments, string drpDocument, string Reason, string subserviceLine,
             string mfPartnerName, string mfManagerName, string drpConcession, string ConcessionRatio, string drpTooluse, string drptoolsknown, out string msg, string FileName, string ContentType, byte[] ByteCode, int ProjectId, out string generatedCode)
        {
            try
            {

                SqlCommand scom = new SqlCommand("Project_Data_INSERT");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                SqlParameter generateCode = new SqlParameter("@GeneratedCode", SqlDbType.NVarChar, 1000);

                string endDate = "";
                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                generateCode.Value = "";
                generateCode.Direction = ParameterDirection.Output;
                generateCode.ParameterName = "GeneratedCode";

                //scom.Parameters["@Message"].Size = 1000;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", 0);
                scom.Parameters.Add(message1);

                scom.Parameters.AddWithValue("@Name", prjctName);
                scom.Parameters.AddWithValue("@ProjectCode", projectcode);
                scom.Parameters.Add(generateCode);

                scom.Parameters.AddWithValue("@FirmLocation", memberLoc);
                scom.Parameters.AddWithValue("@FirmCountry", memberCountry);

                scom.Parameters.AddWithValue("@FirmPartner", partnerFirm);
                scom.Parameters.AddWithValue("@FirmManager", managerMemberFirm);
                scom.Parameters.AddWithValue("@IJVPartner", partnerDIJV);
                scom.Parameters.AddWithValue("@IJVManager", managerDIJV);
                scom.Parameters.AddWithValue("@StartDate", projectDate);
                scom.Parameters.AddWithValue("@EndDate", endDate);
                scom.Parameters.AddWithValue("@isChargeable", chargeableNon);
                scom.Parameters.AddWithValue("@ServiceLine", serviceLine);
                scom.Parameters.AddWithValue("@NatureOfWork", natureOfWork);
             //   scom.Parameters.AddWithValue("@isIdealInvolvement", idealInvol);
                scom.Parameters.AddWithValue("@Sector", sector);
                scom.Parameters.AddWithValue("@SubSector", subSector);
                scom.Parameters.AddWithValue("@EngagementCode", engCode);
                scom.Parameters.AddWithValue("@TaskCode", phaseCode);
                scom.Parameters.AddWithValue("@SAPCode", sapCode);
                scom.Parameters.AddWithValue("@isConflictCheckComplete", conflictCheck);
                scom.Parameters.AddWithValue("@ConflictCheckNumber", conflictcheckNumber);
                scom.Parameters.AddWithValue("@CreatedBy", createdby);
                scom.Parameters.AddWithValue("@LastUpdateBy", lastupdatedby);
                scom.Parameters.AddWithValue("@Others", others);
                scom.Parameters.AddWithValue("@isWorkOrderRequired", isWORequired);
                scom.Parameters.AddWithValue("@Comments", comments);
                scom.Parameters.AddWithValue("@Document", drpDocument);
                scom.Parameters.AddWithValue("@Reason", Reason);
                scom.Parameters.AddWithValue("@SubServiceLine", subserviceLine);
                scom.Parameters.AddWithValue("@MFPartnerName", mfPartnerName);
                scom.Parameters.AddWithValue("@MFManagerName", mfManagerName);

                scom.Parameters.AddWithValue("@DrpConcession", drpConcession);
                scom.Parameters.AddWithValue("@ConcessionRatio", ConcessionRatio);

                scom.Parameters.AddWithValue("@DrpToolsUse", drpTooluse);
                scom.Parameters.AddWithValue("@DrpToolsKnown", drptoolsknown);


                scom.Parameters.AddWithValue("@FileName", FileName);
                scom.Parameters.AddWithValue("@FileExtension", ContentType);
                scom.Parameters.AddWithValue("@Data", ByteCode);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);

                string result = dc.GetOutStringCommand(ref scom);
                msg = message1.Value.ToString();
                generatedCode = generateCode.Value.ToString();


                //msg = scom.Parameters["@Message"].Value.ToString();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Project_Data_UPDATE(int ID, string prjctName, string memberLoc, string memberCountry, string partnerFirm, string managerMemberFirm, string partnerDIJV,
             string managerDIJV, string projectDate, string chargeableNon, string serviceLine, string natureOfWork, string sector, string subSector,
             string engCode, string phaseCode, string sapCode, string conflictCheck, string conflictcheckNumber,
             bool isWORequired, string lastupdatedby, string others, string comments, string DrpDocument, string Reason, string subserviceLine, string mfPartnerName, string mfManagerName, string drpConcession, string ConcessionRatio, string drpTooluse, string drptoolsknown,
             out string msg, string FileName, string ContentType, byte[] ByteCode, int ProjectId, out string generatedCode)


        {
            try
            {
                SqlCommand scom = new SqlCommand("Project_Data_UPDATE");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                SqlParameter generateCode = new SqlParameter("@GeneratedCode", SqlDbType.NVarChar, 1000);

                string endDate = "2018-06-30";

                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";
                generateCode.Value = "";
                generateCode.Direction = ParameterDirection.Output;
                generateCode.ParameterName = "GeneratedCode";

                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", ID);
                scom.Parameters.Add(message1);
                scom.Parameters.Add(generateCode);


                scom.Parameters.AddWithValue("@Name", prjctName);

                scom.Parameters.AddWithValue("@FirmLocation", memberLoc);
                scom.Parameters.AddWithValue("@FirmCountry", memberCountry);
                scom.Parameters.AddWithValue("@FirmPartner", partnerFirm);
                scom.Parameters.AddWithValue("@FirmManager", managerMemberFirm);
                scom.Parameters.AddWithValue("@IJVPartner", partnerDIJV);
                scom.Parameters.AddWithValue("@IJVManager", managerDIJV);
                scom.Parameters.AddWithValue("@StartDate", projectDate);
                scom.Parameters.AddWithValue("@EndDate", endDate);
                scom.Parameters.AddWithValue("@isChargeable", chargeableNon);
                scom.Parameters.AddWithValue("@ServiceLine", serviceLine);
                scom.Parameters.AddWithValue("@NatureOfWork", natureOfWork);
            //    scom.Parameters.AddWithValue("@isIdealInvolvement", idealInvol);
                scom.Parameters.AddWithValue("@Sector", sector);
                scom.Parameters.AddWithValue("@SubSector", subSector);
                scom.Parameters.AddWithValue("@EngagementCode", engCode);
                scom.Parameters.AddWithValue("@TaskCode", phaseCode);
                scom.Parameters.AddWithValue("@SAPCode", sapCode);

                scom.Parameters.AddWithValue("@isConflictCheckComplete", conflictCheck);
                scom.Parameters.AddWithValue("@ConflictCheckNumber", conflictcheckNumber);

                scom.Parameters.AddWithValue("@LastUpdateBy", lastupdatedby);
                scom.Parameters.AddWithValue("@Others", others);
                scom.Parameters.AddWithValue("@isWorkOrderRequired", isWORequired);
                scom.Parameters.AddWithValue("@Comments", comments);
                scom.Parameters.AddWithValue("@Document", DrpDocument);
                scom.Parameters.AddWithValue("@Reason", Reason);
                scom.Parameters.AddWithValue("@SubServiceLine", subserviceLine);
                scom.Parameters.AddWithValue("@MFPartnerName", mfPartnerName);
                scom.Parameters.AddWithValue("@MFManagerName", mfManagerName);

                scom.Parameters.AddWithValue("@DrpConcession", drpConcession);
                scom.Parameters.AddWithValue("@ConcessionRatio", ConcessionRatio);

                scom.Parameters.AddWithValue("@DrpToolsUse", drpTooluse);
                scom.Parameters.AddWithValue("@DrpToolsKnown", drptoolsknown);


                scom.Parameters.AddWithValue("@FileName", FileName);
                scom.Parameters.AddWithValue("@FileExtension", ContentType);
                scom.Parameters.AddWithValue("@Data", ByteCode);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);

                string result = dc.GetOutStringCommand(ref scom);
                msg = message1.Value.ToString();
                generatedCode = generateCode.Value.ToString();


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckDuplicateRecordForProjectData(string projectName, int location, string eMail)
        {
            try
            {
                SqlCommand objSqlCmd = new SqlCommand("CheckDuplicateRecordForProjectData");
                objSqlCmd.CommandType = CommandType.StoredProcedure;
                objSqlCmd.Parameters.AddWithValue("@projectName", projectName);
                objSqlCmd.Parameters.AddWithValue("@location", location);
                objSqlCmd.Parameters.AddWithValue("@eMail", eMail);
                return dc.GetDataSetUsingCommand(objSqlCmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetSAPCode(int memCountry, int serviceLine, int chargeable, string subserviceLine, int officeID)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand("GetSAPCode");
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@chargeCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.AddWithValue("@memCountry", memCountry);
                sqlCmd.Parameters.AddWithValue("@serviceLine", serviceLine);
                sqlCmd.Parameters.AddWithValue("@chargeable", chargeable);
                sqlCmd.Parameters.AddWithValue("@subserviceLine", subserviceLine);
                sqlCmd.Parameters.AddWithValue("@CountrylocationId", officeID);
                string sapCOde = "";
                dc.ExecuteCommand(sqlCmd);
                sapCOde = sqlCmd.Parameters["@chargeCode"].Value.ToString();

                return sapCOde;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetSAPCodeForMasterPage(int memCountry, int serviceLine, int chargeable, string subserviceLine, int officeID)
        {
            try
            {

                SqlCommand sqlCmd = new SqlCommand("GetSAPCode_ForMasterPage");
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@memCountry", memCountry);
                sqlCmd.Parameters.AddWithValue("@serviceLine", serviceLine);
                sqlCmd.Parameters.AddWithValue("@chargeable", chargeable);
                sqlCmd.Parameters.AddWithValue("@subserviceLine", subserviceLine);
                sqlCmd.Parameters.AddWithValue("@CountrylocationId", officeID);

                return dc.GetDataSetUsingCommand(sqlCmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reports

        public DataSet GetFilteredEmployeeReport(int ReportType, string DateFrom, string DateTo, int ServiceLine, int Geography, int Employee, int ProjectId, int DeptId, int SubDeptId, out string message)
        {
            try
            {
                DataSet ds = new DataSet();
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("[Reports_GET]");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ReportType", ReportType);
                scom.Parameters.AddWithValue("@DateFrom", DateFrom);
                scom.Parameters.AddWithValue("@DateTo", DateTo);
                scom.Parameters.AddWithValue("@ServiceLine", ServiceLine);
                scom.Parameters.AddWithValue("@Geography", Geography);
                scom.Parameters.AddWithValue("@Employee", Employee);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                scom.Parameters.AddWithValue("@DeptId", DeptId);
                scom.Parameters.AddWithValue("@SubDeptId", SubDeptId);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUser());
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                ds = db.GetDataSetAndOutStringUsingCommand_Report(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDailyReport(string DateFrom, string DateTo, out string message)
        {
            try
            {
                DataSet ds = new DataSet();
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("[Reports_GET_DailyReport]");
                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.AddWithValue("@DateFrom", DateFrom);
                scom.Parameters.AddWithValue("@DateTo", DateTo);
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                ds = db.GetDataSetAndOutStringUsingCommand_Report(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Reports_Data_SEARCH(string Name, int serviceLineID, int countryID)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("sp_ReportsData_Search");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@ServiceLineID", serviceLineID);
                scom.Parameters.AddWithValue("@CountryID", countryID);

                return dc.GetDataSetUsingCommand_Report(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEmployeeReport(int Id, int EmpID, string startDate, string endDate)
        {
            try
            {
                DataSet ds = new DataSet();
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("Project_Report");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@EmpId", EmpID);
                scom.Parameters.AddWithValue("@FromDate", startDate);
                scom.Parameters.AddWithValue("@ToDate", endDate);

                ds = db.GetDataSetUsingCommand_Report(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetFilteredEmployeeReport(int ReportType, string DateFrom, string DateTo, int ServiceLine, int Geography, int Employee, int ProjectId, out string message)
        {
            try
            {


                DataSet ds = new DataSet();
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("[Reports_GET_R]");
                scom.CommandTimeout = 100;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ReportType", ReportType);
                scom.Parameters.AddWithValue("@DateFrom", DateFrom);
                scom.Parameters.AddWithValue("@DateTo", DateTo);
                scom.Parameters.AddWithValue("@ServiceLine", ServiceLine);
                scom.Parameters.AddWithValue("@Geography", Geography);
                scom.Parameters.AddWithValue("@Employee", Employee);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUser());
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                ds = db.GetDataSetAndOutStringUsingCommand_Report(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Allocate
        public DataSet Project_Data_SEARCH(string Name)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_SEARCH");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Project_Data_SEARCH_Project(string Name)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_SEARCH_Project");
                
                
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@UserId", CommonFunctions.GetUserId());
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Project_Data_Allocation(string Name)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_Allocation");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@UserEmail", CommonFunctions.GetUser());
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ResourceAllocation_GET(string ProjectId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("ResourceAllocation_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ResourceAllocation_INSERT(out string ID, string ProjectId, string EmpEmailID, string FromDate, string ToDate, string @CreatedByMail)
        {
            try
            {
                string message = "";
                SqlCommand scom = new SqlCommand("ResourceAllocation_INSERT");
                scom.CommandTimeout = 900;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@ProjectID", ProjectId);
                scom.Parameters.AddWithValue("@EmpEmailID", EmpEmailID);
                scom.Parameters.AddWithValue("@CreatedByMail", CreatedByMail);
                scom.Parameters.AddWithValue("@FromDate", FromDate);
                scom.Parameters.AddWithValue("@ToDate", ToDate);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                ID = scom.Parameters["@Id"].Value.ToString();
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TimeSheet
        public DataSet sp_GET_Timesheet_Data(string StartDate, string EmpEmail)
        {
            try
            {


                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("sp_GET_Timesheet_Data");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@startDate", StartDate);
                scom.Parameters.AddWithValue("@EmpEmail", EmpEmail);
                
                
                scom.Parameters.AddWithValue("@UserId", CommonFunctions.GetUserId());
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TimeSheetData_INSERT(DataTable dt, string entryPoint, string userid, out string message)
        {
            try
            {


                DBConnect db = new DBConnect();
                //SqlCommand scom = new SqlCommand("sp_INSERT_Timesheet_Data");
                SqlCommand scom = new SqlCommand("sp_INSERT_Timesheet_Data");

                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;
                parameter.ParameterName = "@tblTimesheet";

                parameter.Value = dt;

                scom.Parameters.Add(parameter);
                scom.Parameters.AddWithValue("@entryPoint", entryPoint);
                scom.Parameters.AddWithValue("@lastUpdatedBy", userid);
                scom.Parameters.AddWithValue("@UserId", CommonFunctions.GetUserId());
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteCommandWithRef(ref scom);

                //db.GetOutStringCommand(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeactivateProject
        public string Project_Data_Deactivate(string ProjectId, string IsActive, string CreatedByMail)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("Project_Data_Deactivate");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@ProjectID", ProjectId);
                scom.Parameters.AddWithValue("@IsActive", IsActive);
                scom.Parameters.AddWithValue("@CreatedByMail", CreatedByMail);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                //ID = scom.Parameters["@Id"].Value.ToString();
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        #endregion

        #region Work Order

        public DataSet GetWorkOrderData(int Id, string workOrderNumber)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_Data_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@WorkOrderNumber", workOrderNumber);

                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetWorkOrder_EPCC2_Data(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_EPCC2_Data_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetWorkOrder_EPCC3_Data(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_EPCC3_Data_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string WorkOrder_Insert(int prjctID, string clientName, string endDate, string chkCC, string chkCCCompleted, string chkDataSharing, string chkDRMS, string overview, string servicesRequired,
            decimal ADhours, decimal AMHours, decimal DMHours, decimal ExecHours, decimal exec1Hours, decimal managerHours, decimal directorHours, string sharedDrivePath, string contact2, string contact3, string userID,
            string DIJVApprover, string isWOSubmitted, string projectCode, string memberFirmID,
           DateTime? MFConflictCheckDate, string MFConflictCheckConcernedPerson,
           DateTime? InternalRiskDate, string InternalRisk,
           DateTime? isDRMSForwardedDate, string isDRMSForwardedConcernedPerson,
           DateTime? DIJVConflictCheckDate, string DIJVConflictCheckConcernedPerson,
           DateTime? DataConfirmedByMFDate, string DataConfirmedByMFConcernedPerson,

            DateTime? DataDtTransactionRelate, string DatatxtTransactionRelate,
            DateTime? DatadtAreYouWorking, string txtAreYouWorking,
            DateTime? DatadtIsThereConfidentiality, string txtIsThereConfidentiality,
            //string txtEpccStage1,

            DateTime? BriefingOfDIJVTeamDate, string BriefingOfDIJVTeamConcernedPerson, string version,
            out string message, out string wo_number, out string href)
        {
            try
            {


                SqlCommand scom = new SqlCommand("Work_Order_INSERT");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);


                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                SqlParameter WO_Number = new SqlParameter("@WO_Number", SqlDbType.NVarChar, 1000);


                WO_Number.Value = "";
                WO_Number.Direction = ParameterDirection.Output;
                WO_Number.ParameterName = "WO_Number";

                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);


                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";

                //scom.Parameters["@Message"].Size = 1000;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", 0);
                scom.Parameters.Add(message1);
                scom.Parameters.Add(WO_Number);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                scom.Parameters.AddWithValue("@ProjectCode", projectCode);
                scom.Parameters.AddWithValue("@ClientName", clientName);
                scom.Parameters.AddWithValue("@EndDate", endDate);
                scom.Parameters.AddWithValue("@MFConflictCheck", chkCC);
                scom.Parameters.AddWithValue("@isDRMSForwarded", chkDRMS);
                scom.Parameters.AddWithValue("@DIJVConflictCheck", chkCCCompleted);
                scom.Parameters.AddWithValue("@DataConfirmedByMF", chkDataSharing);
                scom.Parameters.AddWithValue("@EngagementOverview", overview);
                scom.Parameters.AddWithValue("@ServicesOverview", servicesRequired);
                scom.Parameters.AddWithValue("@HoursAD", ADhours);
                scom.Parameters.AddWithValue("@HoursMgr", managerHours);
                scom.Parameters.AddWithValue("@HoursDM", DMHours);
                scom.Parameters.AddWithValue("@HoursAM", AMHours);
                scom.Parameters.AddWithValue("@HoursExec", ExecHours);
                scom.Parameters.AddWithValue("@HoursExec1", exec1Hours);
                scom.Parameters.AddWithValue("@HoursDir", directorHours);
                scom.Parameters.AddWithValue("@SharedDrivePath", sharedDrivePath);
                scom.Parameters.AddWithValue("@DIJVContact2", contact2);
                scom.Parameters.AddWithValue("@DIJVContact3", contact3);
                scom.Parameters.AddWithValue("@IsWOSubmitted", isWOSubmitted);
                scom.Parameters.AddWithValue("@CreatedBy", userID);
                scom.Parameters.AddWithValue("@DIJVApprover", DIJVApprover);
                scom.Parameters.AddWithValue("@DIJVContact1", memberFirmID);
                scom.Parameters.AddWithValue("@MemberFirmID", memberFirmID);

                scom.Parameters.AddWithValue("@MFConflictCheckDate", MFConflictCheckDate);
                scom.Parameters.AddWithValue("@MFConflictCheckConcernedPerson", MFConflictCheckConcernedPerson);

                scom.Parameters.AddWithValue("@InternalRiskDate", InternalRiskDate);
                scom.Parameters.AddWithValue("@InternalRisk", InternalRisk);

                scom.Parameters.AddWithValue("@isDRMSForwardedDate", isDRMSForwardedDate);
                scom.Parameters.AddWithValue("@isDRMSForwardedConcernedPerson", isDRMSForwardedConcernedPerson);
                scom.Parameters.AddWithValue("@DIJVConflictCheckDate", DIJVConflictCheckDate);
                scom.Parameters.AddWithValue("@DIJVConflictCheckConcernedPerson", DIJVConflictCheckConcernedPerson);
                scom.Parameters.AddWithValue("@DataConfirmedByMFDate", DataConfirmedByMFDate);
                scom.Parameters.AddWithValue("@DataConfirmedByMFConcernedPerson", DataConfirmedByMFConcernedPerson);

                scom.Parameters.AddWithValue("@DataDtTransactionRelate", DataDtTransactionRelate);
                scom.Parameters.AddWithValue("@DatatxtTransactionRelate", DatatxtTransactionRelate);
                scom.Parameters.AddWithValue("@DatadtAreYouWorking", DatadtAreYouWorking);
                scom.Parameters.AddWithValue("@txtAreYouWorking", txtAreYouWorking);
                scom.Parameters.AddWithValue("@DatadtIsThereConfidentiality", DatadtIsThereConfidentiality);
                scom.Parameters.AddWithValue("@txtIsThereConfidentiality", txtIsThereConfidentiality);
                //scom.Parameters.AddWithValue("@EpccStage1", txtEpccStage1);



                scom.Parameters.AddWithValue("@BriefingOfDIJVTeamDate", BriefingOfDIJVTeamDate);
                scom.Parameters.AddWithValue("@BriefingOfDIJVTeamConcernedPerson", BriefingOfDIJVTeamConcernedPerson);
                scom.Parameters.AddWithValue("@WOVersion", version);
                string result = dc.GetOutStringCommand(ref scom);
                //DataSet result = dc.GetDataSetAndOutStringUsingCommand(ref scom); 
                message = message1.Value.ToString();
                wo_number = WO_Number.Value.ToString();
                href = Href.Value.ToString();
                return result;//.Tables[0].Rows[0]["is"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string WorkOrder_EPCC2_Insert(int prjctID, string CanadianReview, string Insider, string WOIssued, string Notes, string HaveAllCondition, string DoesDIJV, string AreTheir, string HasRestricted,
            string MFConfirming, string AllConflict, string NDA, string DIJVManager, string EngagementManager, string userID, string isSubmit,
            out string message, out string wo_number, out string href)
        {
            try
            {


                SqlCommand scom = new SqlCommand("Work_Order_EPCC2_INSERT");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);


                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                SqlParameter WO_Number = new SqlParameter("@WO_Number", SqlDbType.NVarChar, 1000);


                WO_Number.Value = "";
                WO_Number.Direction = ParameterDirection.Output;
                WO_Number.ParameterName = "WO_Number";

                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);


                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";

                //scom.Parameters["@Message"].Size = 1000;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", 0);
                scom.Parameters.Add(message1);
                scom.Parameters.Add(WO_Number);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                scom.Parameters.AddWithValue("@CanadianReview", CanadianReview);
                scom.Parameters.AddWithValue("@Insider", Insider);
                scom.Parameters.AddWithValue("@WOIssued", WOIssued);
                scom.Parameters.AddWithValue("@Notes", Notes);
                scom.Parameters.AddWithValue("@HaveAllCondition", HaveAllCondition);
                scom.Parameters.AddWithValue("@DoesDIJV", DoesDIJV);
                scom.Parameters.AddWithValue("@AreTheir", AreTheir);
                scom.Parameters.AddWithValue("@HasRestricted", HasRestricted);
                scom.Parameters.AddWithValue("@MFConfirming", MFConfirming);
                scom.Parameters.AddWithValue("@AllConflict", AllConflict);
                scom.Parameters.AddWithValue("@NDA", NDA);
                scom.Parameters.AddWithValue("@DIJVManager", DIJVManager);
                scom.Parameters.AddWithValue("@EngagementManager", EngagementManager);
                scom.Parameters.AddWithValue("@CreatedBy", userID);
                scom.Parameters.AddWithValue("@IsWOSubmitted", isSubmit);

                string result = dc.GetOutStringCommand(ref scom);
                message = message1.Value.ToString();
                wo_number = WO_Number.Value.ToString();
                href = Href.Value.ToString();
                return result;//.Tables[0].Rows[0]["is"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string WorkOrder_EPCC2_Update(int EPCC_Id, int prjctID, string CanadianReview, string Insider, string WOIssued, string Notes, string HaveAllCondition, string DoesDIJV, string AreTheir, string HasRestricted,
            string MFConfirming, string AllConflict, string NDA, string DIJVManager, string EngagementManager, string userID, string isSubmit,
           out string message, out string href)
        {
            try
            {

                SqlCommand scom = new SqlCommand("Work_Order_EPCC2_UPDATE");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);


                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);

                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";

                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.Add(message1);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@Id", EPCC_Id);
                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                scom.Parameters.AddWithValue("@CanadianReview", CanadianReview);
                scom.Parameters.AddWithValue("@Insider", Insider);
                scom.Parameters.AddWithValue("@WOIssued", WOIssued);
                scom.Parameters.AddWithValue("@Notes", Notes);
                scom.Parameters.AddWithValue("@HaveAllCondition", HaveAllCondition);
                scom.Parameters.AddWithValue("@DoesDIJV", DoesDIJV);
                scom.Parameters.AddWithValue("@AreTheir", AreTheir);
                scom.Parameters.AddWithValue("@HasRestricted", HasRestricted);
                scom.Parameters.AddWithValue("@MFConfirming", MFConfirming);
                scom.Parameters.AddWithValue("@AllConflict", AllConflict);
                scom.Parameters.AddWithValue("@NDA", NDA);
                scom.Parameters.AddWithValue("@DIJVManager", DIJVManager);
                scom.Parameters.AddWithValue("@EngagementManager", EngagementManager);
                scom.Parameters.AddWithValue("@LastUpdateBy", userID);
                scom.Parameters.AddWithValue("@IsWOSubmitted", isSubmit);

                string result = dc.GetCommand(ref scom);
                message = message1.Value.ToString();
                href = Href.Value.ToString();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string WorkOrder_EPCC3_Insert(int prjctID, string FinalHours, string FinalWO, string CanadianReview, string GermanInsider, string TRATwo, string Note,
            string Conflicts, string DIJVConflict, string AllDocument, string NDA, string DIJVManager, string EngagementManager, string userID, string isSubmit,
            out string message, out string wo_number, out string href)
        {
            try
            {
                SqlCommand scom = new SqlCommand("Work_Order_EPCC3_INSERT");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";
                SqlParameter WO_Number = new SqlParameter("@WO_Number", SqlDbType.NVarChar, 1000);
                WO_Number.Value = "";
                WO_Number.Direction = ParameterDirection.Output;
                WO_Number.ParameterName = "WO_Number";
                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);
                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", 0);
                scom.Parameters.Add(message1);
                scom.Parameters.Add(WO_Number);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                scom.Parameters.AddWithValue("@FinalHours", FinalHours);
                scom.Parameters.AddWithValue("@FinalWO", FinalWO);
                scom.Parameters.AddWithValue("@CanadianReview", CanadianReview);
                scom.Parameters.AddWithValue("@GermanInsider", GermanInsider);
                scom.Parameters.AddWithValue("@TRATwo", TRATwo);
                scom.Parameters.AddWithValue("@Note", Note);
                scom.Parameters.AddWithValue("@Conflicts", Conflicts);
                scom.Parameters.AddWithValue("@DIJVConflict", DIJVConflict);
                scom.Parameters.AddWithValue("@AllDocument", AllDocument);
                scom.Parameters.AddWithValue("@NDA", NDA);
                scom.Parameters.AddWithValue("@DIJVManager", DIJVManager);
                scom.Parameters.AddWithValue("@EngagementManager", EngagementManager);
                scom.Parameters.AddWithValue("@CreatedBy", userID);
                scom.Parameters.AddWithValue("@IsWOSubmitted", isSubmit);

                string result = dc.GetOutStringCommand(ref scom);
                message = message1.Value.ToString();
                wo_number = WO_Number.Value.ToString();
                href = Href.Value.ToString();
                return result;//.Tables[0].Rows[0]["is"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string WorkOrder_EPCC3_Update(int EPCC_Id, int prjctID, string FinalHours, string FinalWO, string CanadianReview, string GermanInsider, string TRATwo, string Note,
            string Conflicts, string DIJVConflict, string AllDocument, string NDA, string DIJVManager, string EngagementManager, string userID, string isSubmit,
            out string message, out string href)
        {
            try
            {

                SqlCommand scom = new SqlCommand("Work_Order_EPCC3_UPDATE");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);


                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);

                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";

                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.Add(message1);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@Id", EPCC_Id);
                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                scom.Parameters.AddWithValue("@FinalHours", FinalHours);
                scom.Parameters.AddWithValue("@FinalWO", FinalWO);
                scom.Parameters.AddWithValue("@CanadianReview", CanadianReview);
                scom.Parameters.AddWithValue("@GermanInsider", GermanInsider);
                scom.Parameters.AddWithValue("@TRATwo", TRATwo);
                scom.Parameters.AddWithValue("@Note", Note);
                scom.Parameters.AddWithValue("@Conflicts", Conflicts);
                scom.Parameters.AddWithValue("@DIJVConflict", DIJVConflict);
                scom.Parameters.AddWithValue("@AllDocument", AllDocument);
                scom.Parameters.AddWithValue("@NDA", NDA);
                scom.Parameters.AddWithValue("@DIJVManager", DIJVManager);
                scom.Parameters.AddWithValue("@EngagementManager", EngagementManager);
                scom.Parameters.AddWithValue("@LastUpdateBy", userID);
                scom.Parameters.AddWithValue("@IsWOSubmitted", isSubmit);

                string result = dc.GetCommand(ref scom);
                message = message1.Value.ToString();
                href = Href.Value.ToString();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string WorkOrder_Update(int prjctID, int woLogNumber, string clientName, string endDate, string chkCC, string chkCCCompleted, string chkDataSharing, string chkDRMS, string overview, string servicesRequired,
            decimal ADhours, decimal AMHours, decimal DMHours, decimal ExecHours, decimal exec1Hours, decimal managerHours, decimal directorHours, string sharedDrivePath, string contact2, string contact3,
            string userID, string DIJVApprover, string isWOSubmitted, string projectCode, string memberFirmID,
            DateTime? MFConflictCheckDate, string MFConflictCheckConcernedPerson,
            DateTime? InternalRiskDate, string InternalRisk,
            DateTime? isDRMSForwardedDate, string isDRMSForwardedConcernedPerson,
            DateTime? DIJVConflictCheckDate, string DIJVConflictCheckConcernedPerson,
            DateTime? DataConfirmedByMFDate, string DataConfirmedByMFConcernedPerson,

            DateTime? DataDtTransactionRelate, string DatatxtTransactionRelate,
            DateTime? DatadtAreYouWorking, string txtAreYouWorking,
            DateTime? DatadtIsThereConfidentiality, string txtIsThereConfidentiality,
            //string txtEpccStage1,

            DateTime? BriefingOfDIJVTeamDate, string BriefingOfDIJVTeamConcernedPerson, string version,
            out string message, out string href)
        {
            try
            {


                SqlCommand scom = new SqlCommand("Work_Order_Update");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);


                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);

                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";


                //scom.Parameters["@Message"].Size = 1000;
                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.Add(message1);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                scom.Parameters.AddWithValue("@ProjectCode", projectCode);
                scom.Parameters.AddWithValue("@WOlogNumber", woLogNumber);
                scom.Parameters.AddWithValue("@ClientName", clientName);
                scom.Parameters.AddWithValue("@EndDate", endDate);
                scom.Parameters.AddWithValue("@MFConflictCheck", chkCC);
                scom.Parameters.AddWithValue("@isDRMSForwarded", chkDRMS);
                scom.Parameters.AddWithValue("@DIJVConflictCheck", chkCCCompleted);
                scom.Parameters.AddWithValue("@DataConfirmedByMF", chkDataSharing);
                scom.Parameters.AddWithValue("@EngagementOverview", overview);
                scom.Parameters.AddWithValue("@ServicesOverview", servicesRequired);
                scom.Parameters.AddWithValue("@HoursAD", ADhours);
                scom.Parameters.AddWithValue("@HoursMgr", managerHours);
                scom.Parameters.AddWithValue("@HoursDM", DMHours);
                scom.Parameters.AddWithValue("@HoursAM", AMHours);
                scom.Parameters.AddWithValue("@HoursExec", ExecHours);
                scom.Parameters.AddWithValue("@HoursExec1", exec1Hours);
                scom.Parameters.AddWithValue("@HoursDir", directorHours);
                scom.Parameters.AddWithValue("@SharedDrivePath", sharedDrivePath);
                scom.Parameters.AddWithValue("@DIJVContact2", contact2);
                scom.Parameters.AddWithValue("@DIJVContact3", contact3);
                scom.Parameters.AddWithValue("@IsWOSubmitted", isWOSubmitted);
                scom.Parameters.AddWithValue("@DIJVApprover", DIJVApprover);
                scom.Parameters.AddWithValue("@LastUpdateBy", userID);
                scom.Parameters.AddWithValue("@DIJVContact1", memberFirmID);

                scom.Parameters.AddWithValue("@MFConflictCheckDate", MFConflictCheckDate);
                scom.Parameters.AddWithValue("@MFConflictCheckConcernedPerson", MFConflictCheckConcernedPerson);

                scom.Parameters.AddWithValue("@InternalRiskDate", InternalRiskDate);
                scom.Parameters.AddWithValue("@InternalRisk", InternalRisk);

                scom.Parameters.AddWithValue("@isDRMSForwardedDate", isDRMSForwardedDate);
                scom.Parameters.AddWithValue("@isDRMSForwardedConcernedPerson", isDRMSForwardedConcernedPerson);
                scom.Parameters.AddWithValue("@DIJVConflictCheckDate", DIJVConflictCheckDate);
                scom.Parameters.AddWithValue("@DIJVConflictCheckConcernedPerson", DIJVConflictCheckConcernedPerson);
                scom.Parameters.AddWithValue("@DataConfirmedByMFDate", DataConfirmedByMFDate);
                scom.Parameters.AddWithValue("@DataConfirmedByMFConcernedPerson", DataConfirmedByMFConcernedPerson);

                scom.Parameters.AddWithValue("@DataDtTransactionRelate", DataDtTransactionRelate);
                scom.Parameters.AddWithValue("@DatatxtTransactionRelate", DatatxtTransactionRelate);
                scom.Parameters.AddWithValue("@DatadtAreYouWorking", DatadtAreYouWorking);
                scom.Parameters.AddWithValue("@txtAreYouWorking", txtAreYouWorking);
                scom.Parameters.AddWithValue("@DatadtIsThereConfidentiality", DatadtIsThereConfidentiality);
                scom.Parameters.AddWithValue("@txtIsThereConfidentiality", txtIsThereConfidentiality);
                //scom.Parameters.AddWithValue("@EpccStage1", txtEpccStage1);

                scom.Parameters.AddWithValue("@BriefingOfDIJVTeamDate", BriefingOfDIJVTeamDate);
                scom.Parameters.AddWithValue("@BriefingOfDIJVTeamConcernedPerson", BriefingOfDIJVTeamConcernedPerson);
                scom.Parameters.AddWithValue("@WOVersion", version);

                string result = dc.GetCommand(ref scom);
                message = message1.Value.ToString();
                href = Href.Value.ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string WorkOrder_Partial_Update(int woLogNumber,
            string txtConflictCheck, string txtMFConflictCheck, string txtDIJVSubmitted, string txtCCCompleted,
            string InternalRisk, DateTime? DtInternalRisk,
            DateTime? dtMFConflictCheck, DateTime? dtDIJVSubmitted, DateTime? dtCCCompleted,
            string txtTransactionRelate, string txtAreYouWorking, string txtIsThereConfidentiality,
            DateTime? DtTransactionRelate, DateTime? dtAreYouWorking, DateTime? dtIsThereConfidentiality,
            out string message, out string href)
        {
            try
            {


                SqlCommand scom = new SqlCommand("Work_Order_UPDATE_Partial");
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);


                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";

                SqlParameter Href = new SqlParameter("@Href", SqlDbType.NVarChar, 1000);

                Href.Value = "";
                Href.Direction = ParameterDirection.Output;
                Href.ParameterName = "Href";


                //scom.Parameters["@Message"].Size = 1000;
                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.Add(message1);
                scom.Parameters.Add(Href);

                scom.Parameters.AddWithValue("@WOlogNumber", woLogNumber);

                scom.Parameters.AddWithValue("@ConflictCheck", txtConflictCheck);
                scom.Parameters.AddWithValue("@MFConflictCheck", txtMFConflictCheck);
                scom.Parameters.AddWithValue("@DIJVSubmitted", txtDIJVSubmitted);
                scom.Parameters.AddWithValue("@CCCompleted", txtCCCompleted);


                scom.Parameters.AddWithValue("@dtMFConflictCheck", dtMFConflictCheck);
                scom.Parameters.AddWithValue("@dtDIJVSubmitted", dtDIJVSubmitted);
                scom.Parameters.AddWithValue("@dtCCCompleted", dtCCCompleted);

                scom.Parameters.AddWithValue("@txtTransactionRelate", txtTransactionRelate);
                scom.Parameters.AddWithValue("@txtAreYouWorking", txtAreYouWorking);
                scom.Parameters.AddWithValue("@txtIsThereConfidentiality", txtIsThereConfidentiality);
                scom.Parameters.AddWithValue("@DtTransactionRelate", DtTransactionRelate);
                scom.Parameters.AddWithValue("@dtAreYouWorking", dtAreYouWorking);
                scom.Parameters.AddWithValue("@dtIsThereConfidentiality", dtIsThereConfidentiality);

                scom.Parameters.AddWithValue("@InternalRiskDate", DtInternalRisk);
                scom.Parameters.AddWithValue("@InternalRisk", InternalRisk);

                string result = dc.GetCommand(ref scom);
                message = message1.Value.ToString();
                href = Href.Value.ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string WorkOrderHours_Update(int workOrderID, decimal ADhours, decimal AMHours, decimal DMHours, decimal ExecHours, decimal ExecHours1, decimal managerHours, decimal directorHours)
        {
            try
            {


                SqlCommand scom = new SqlCommand("WorkOrderHoursUpdate");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                scom.Parameters.AddWithValue("@HoursAD", ADhours);
                scom.Parameters.AddWithValue("@HoursMgr", managerHours);
                scom.Parameters.AddWithValue("@HoursDM", DMHours);
                scom.Parameters.AddWithValue("@HoursAM", AMHours);
                scom.Parameters.AddWithValue("@HoursExec", ExecHours);
                scom.Parameters.AddWithValue("@HoursExec1", ExecHours1);

                scom.Parameters.AddWithValue("@HoursDir", directorHours);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUserId());
                string result = dc.GetCommand(ref scom);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatusFromDIJVApprover(string status, int workOrderNumber, string DIJVApprover, string memberFirmPartner)
        {
            try
            {
                SqlCommand scom = new SqlCommand("sp_DIJV_WorkOrder_Status_Update");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Status", status);
                scom.Parameters.AddWithValue("@WOLogNumber", workOrderNumber);
                scom.Parameters.AddWithValue("@DIJVApprover", DIJVApprover);
                scom.Parameters.AddWithValue("@MemberFirmApprover", memberFirmPartner);

                dc.ExecuteCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EPCC2UpdateStatusFromDIJVApprover(string status, int workOrderNumber, string DIJVApprover, int AdminFlag)
        {
            try
            {
                SqlCommand scom = new SqlCommand("sp_DIJV_EPCC_Status_Update");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Status", status);
                scom.Parameters.AddWithValue("@WOLogNumber", workOrderNumber);
                scom.Parameters.AddWithValue("@DIJVApprover", DIJVApprover);
                scom.Parameters.AddWithValue("@AdminFlag", AdminFlag);
                dc.ExecuteCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EPCC3UpdateStatusFromDIJVApprover(string status, int workOrderNumber, string DIJVApprover, int AdminFlag)
        {
            try
            {
                SqlCommand scom = new SqlCommand("sp_DIJV_EPCC3_Status_Update");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Status", status);
                scom.Parameters.AddWithValue("@WOLogNumber", workOrderNumber);
                scom.Parameters.AddWithValue("@DIJVApprover", DIJVApprover);
                scom.Parameters.AddWithValue("@AdminFlag", AdminFlag);
                dc.ExecuteCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet WorkOrder_PDFData_GET(string Id)
        {
            try
            {


                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_PDFData_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet WorkOrder_PDFData_GET_Report(string Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_PDFData_GET_Report");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet WorkOrder_EPCC2_PDFData_GET_Report(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_EPCC2_PDFData_GET_Report");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet WorkOrder_EPCC3_PDFData_GET_Report(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrder_EPCC3_PDFData_GET_Report");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Project_Data_SEARCH_RoleBased(string Name, string User)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_SEARCH_RoleBased");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@User", User);
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Project_Data_SEARCH_RoleBased_EPCC2(string Name, string User)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_SEARCH_RoleBased_EPCC2");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@User", User);
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Project_Data_SEARCH_RoleBased_EPCC3(string Name, string User)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Project_Data_SEARCH_RoleBased_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@User", User);
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetWorkOrderDesignations(int prjctID)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("GetWorkOrderDesignations");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ProjectId", prjctID);
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetEmployeeCountryMailID(int empId, int countryID)
        {
            try
            {


                SqlCommand scom = new SqlCommand("EmployeeCountryMailID_GET");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@CountryID", countryID);
                scom.Parameters.AddWithValue("@EmployeeId", empId);

                string ds = dc.GetOutStringCommand(ref scom);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUserAccessOfWorkOrderEdit(string emailId, int projectId)
        {
            try
            {


                SqlCommand scom = new SqlCommand("CheckUserAccessOfWorkOrderEdit");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@IsAllowed", SqlDbType.Bit).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@emailId", emailId);
                scom.Parameters.AddWithValue("@projectId", projectId);
                dc.ExecuteCommand(scom);

                return (bool)(scom.Parameters["@IsAllowed"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Admin
        public string EmployeesManage(int updateID, out int ID, string txtEmpID, string txtEmpName, string txtEmpEmail, string drpDesignation,
            string drpDepartment, string drpSubDepartment, string txtAusEmailID,
                string txtCanEmailId, string txtUkEmailId, string userId)
        {
            try
            {

                string message = "";

                SqlCommand scom = new SqlCommand("EmployeesManage");
                scom.CommandType = CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@Id";
                Id.Value = updateID;
                Id.SqlDbType = SqlDbType.Int;
                Id.Direction = ParameterDirection.InputOutput;
                scom.Parameters.Add(Id);

                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@EmpID", txtEmpID);
                scom.Parameters.AddWithValue("@Email", txtEmpEmail);
                scom.Parameters.AddWithValue("@USerID", txtEmpEmail);
                scom.Parameters.AddWithValue("@isActive", 1);
                scom.Parameters.AddWithValue("@Employee_Name", txtEmpName);
                scom.Parameters.AddWithValue("@Department_Name", drpDepartment);
                scom.Parameters.AddWithValue("@SubDepartment_Name", drpSubDepartment);
                scom.Parameters.AddWithValue("@Designation", drpDesignation);
                scom.Parameters.AddWithValue("@Australia_Mail_ID", txtAusEmailID);
                scom.Parameters.AddWithValue("@Canada_Mail_ID", txtCanEmailId);
                scom.Parameters.AddWithValue("@UK_Mail_ID", txtUkEmailId);
                scom.Parameters.AddWithValue("@LastUpdatedBy", userId);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();

                var ID1 = scom.Parameters["@Id"].Value.ToString();
                ID = int.Parse(ID1);
                return message;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet EmployeeSearch(string Name)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("EmployeeSearch");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@SearchParameter", Name);
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Employee_Data_Deactivate(string ProjectId, string IsActive, string CreatedByMail)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("Employee_Data_Deactivate");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@empID", ProjectId);
                scom.Parameters.AddWithValue("@IsActive", IsActive);
                scom.Parameters.AddWithValue("@userID", CreatedByMail);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                //ID = scom.Parameters["@Id"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string EnableTimesheet(string UserId, string ProjectId, string FromDate, string ToDate, string CreatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("EnableTimesheet");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@ProjectID", ProjectId);
                scom.Parameters.AddWithValue("@UserId", UserId);
                scom.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                scom.Parameters.AddWithValue("@FromDate", FromDate);
                scom.Parameters.AddWithValue("@ToDate", ToDate);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetActiveProjectsForEmployee(int employeeId)
        {
            try
            {

                SqlCommand scom = new SqlCommand("GetActiveProjectsForEmployee");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@employeeId", employeeId);
                return dc.GetDataSetUsingCommand(scom).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string EnableTimesheet_New(int UserId, int ProjectId, string FromDate, string ToDate, string CreatedBy)
        {
            try
            {

                string message = string.Empty;
                SqlCommand scom = new SqlCommand("EnableTimesheet_New");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@ProjectID", ProjectId);
                scom.Parameters.AddWithValue("@UserId", UserId);
                scom.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                scom.Parameters.AddWithValue("@FromDate", FromDate);
                scom.Parameters.AddWithValue("@ToDate", ToDate);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterSector(string Id, string Name, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterSector");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterSubSector(string Id, string sectorId, string Name, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterSubSector");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@SectorId", sectorId);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MasterCountries(string Id, string Name, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterCountries");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MasterLocation(string Id, string CountryId, string Office, string IsDeleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterLocation");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@CountryId", CountryId);
                scom.Parameters.AddWithValue("@Office", Office);
                scom.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //MasterServiceLine
        public string MasterServiceLine(string Id, string Name, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterServiceLine");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterSubServiceLine(int Id, int serviceLineId, string Name, int Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterSubServiceLine");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@ServiceLineId", serviceLineId);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet MasterCountriesSearch(string SearchText)
        {
            try
            {

                SqlCommand scom = new SqlCommand("MasterCountriesSearch");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@SearchText", SearchText);
                return dc.GetDataSetUsingCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterNatureOfWork(string Id, string SubServiceLineId, string NOWName, string IsDeleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterNatureOfWork");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@SubServiceLineId", SubServiceLineId);
                scom.Parameters.AddWithValue("@Name", NOWName);
                scom.Parameters.AddWithValue("@Deleted", IsDeleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterDepartment(string Id, string Name, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterDepartment");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterSubDepartment(string Id, string sectorId, string Name, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterSubDepartment");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@DepartmentId", sectorId);
                scom.Parameters.AddWithValue("@Name", Name);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterAdmin(string Id, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterAdmin");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MasterReports(string Id, string Deleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterReports");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@Deleted", Deleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetRights()
        {
            try
            {

                SqlCommand scom = new SqlCommand("GetAccessRights");
                scom.CommandType = CommandType.StoredProcedure;
                return dc.GetDataSetUsingCommand(scom).Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SetAccessRight(int EmpId, int RoleId, bool IsDeleted, string UpdatedBy)
        {
            try
            {

                SqlCommand scom = new SqlCommand("SetAccessRight");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@EmpId", EmpId);
                scom.Parameters.AddWithValue("@NewRoleId", RoleId);
                scom.Parameters.AddWithValue("@isDeleted", IsDeleted);
                scom.Parameters.AddWithValue("@UpdatedByEMail", UpdatedBy);
                dc.GetOutStringCommand(ref scom);
                return scom.Parameters["@Message"].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MasterSapCode(string Id, string country, string countryLocation, string serviceLine,
            string chargeable, string SubServiceLineId, string NOWName, string IsDeleted, string LastUpdatedBy)
        {
            try
            {

                string message = "";
                SqlCommand scom = new SqlCommand("MasterSapCode");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@Id", Id);
                scom.Parameters.AddWithValue("@CountryId", country);
                scom.Parameters.AddWithValue("@CountrylocationId", countryLocation);
                scom.Parameters.AddWithValue("@ServicelineId", serviceLine);
                scom.Parameters.AddWithValue("@Cahrgeble", chargeable);
                scom.Parameters.AddWithValue("@SubServiceLineId", SubServiceLineId);

                scom.Parameters.AddWithValue("@Name", NOWName);
                scom.Parameters.AddWithValue("@Deleted", IsDeleted);
                scom.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
                dc.GetOutStringCommand(ref scom);
                message = scom.Parameters["@Message"].Value.ToString();
                return message;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Compliance Report
        public DataSet GetFilteredEmployeeComplianceReport(int ReportType, string DateFrom, string DateTo, int ServiceLine, int Geography, int Employee, int ProjectId, int DeptId, int SubDeptId, out string message)
        {
            try
            {

                DataSet ds = new DataSet();
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("[Reports_GET_Compliance]");
                scom.CommandTimeout = 100;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ReportType", ReportType);
                scom.Parameters.AddWithValue("@DateFrom", DateFrom);
                scom.Parameters.AddWithValue("@DateTo", DateTo);
                scom.Parameters.AddWithValue("@ServiceLine", ServiceLine);
                scom.Parameters.AddWithValue("@Geography", Geography);
                scom.Parameters.AddWithValue("@Employee", Employee);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                scom.Parameters.AddWithValue("@DeptId", DeptId);
                scom.Parameters.AddWithValue("@SubDeptId", SubDeptId);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUser());
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                ds = db.GetDataSetAndOutStringUsingCommand_Report(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
                return ds;

            }
            catch (Exception EX)
            {
                throw EX;
            }
        }



        #endregion

        public DataSet WorkOrderList_GET()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrderList_GET");
                scom.CommandType = CommandType.StoredProcedure;
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DataSet WorkOrderData_GET(int woID)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("WorkOrderData_GET");
                scom.Parameters.AddWithValue("@WOId", woID);
                scom.CommandType = CommandType.StoredProcedure;
                ds = dc.GetDataSetUsingCommand(scom);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SetMFParterWOStatus(int woID, int statusID, string UpdatedBy)
        {
            try
            {

                SqlCommand scom = new SqlCommand("SetMFParterWOStatus");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                scom.Parameters.AddWithValue("@WOId", woID);
                scom.Parameters.AddWithValue("@status", statusID);
                scom.Parameters.AddWithValue("@UpdatedByEMail", UpdatedBy);
                dc.GetOutStringCommand(ref scom);
                return scom.Parameters["@Message"].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetOverViewReport(int ServiceLine)
        {
            try
            {

                DataSet ds = new DataSet();
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("[OverView_Reports]");
                scom.CommandTimeout = 100;
                scom.CommandType = CommandType.StoredProcedure;

                scom.Parameters.AddWithValue("@ServiceLine", ServiceLine);

                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUser());
                ds = db.GetDataSetAndOutStringUsingCommand(ref scom);

                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Work_Order_SEARCH(string WOSearchText, int requestSource)
        {
            try
            {

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Work_Order_SEARCH");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@EmailID", CommonFunctions.GetUser());
                scom.Parameters.AddWithValue("@RequestFrom", requestSource);
                scom.Parameters.AddWithValue("@WOSearchKey", WOSearchText);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable Work_Order_SEARCH_Admin(string WOSearchText, int requestSource)
        {
            try
            {

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Work_Order_SEARCH_Admin");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@EmailID", CommonFunctions.GetUser());
                scom.Parameters.AddWithValue("@RequestFrom", requestSource);
                scom.Parameters.AddWithValue("@WOSearchKey", WOSearchText);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable Work_Order_SEARCH_Admin_EPCC3(string WOSearchText, int requestSource)
        {
            try
            {

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Work_Order_SEARCH_Admin_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@EmailID", CommonFunctions.GetUser());
                scom.Parameters.AddWithValue("@RequestFrom", requestSource);
                scom.Parameters.AddWithValue("@WOSearchKey", WOSearchText);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable Work_Order_SEARCH_EPCC2(string WOSearchText)
        {
            try
            {

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("Work_Order_SEARCH_EPCC2");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@EmailID", CommonFunctions.GetUser());
                scom.Parameters.AddWithValue("@WOSearchKey", WOSearchText);
                ds = dc.GetDataSetUsingCommand(scom);
                dt = ds.Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatusFromDIJVApprover(int status, int workOrderNumber, int DIJVApprover, string memberFirmPartner)
        {
            try
            {

                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("sp_DIJV_WorkOrder_Status_Update");
                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;

                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Status", status);
                scom.Parameters.AddWithValue("@WOLogNumber", workOrderNumber);
                scom.Parameters.AddWithValue("@DIJVApprover", DIJVApprover);
                scom.Parameters.AddWithValue("@MemberFirmApprover", memberFirmPartner);

                db.ExecuteCommandWithRef(ref scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ResetStatus(int workOrderNumber, string DIJVApprover)
        {
            try
            {

                SqlCommand scom = new SqlCommand("Work_Order_Reset");
                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@UserID", DIJVApprover);
                scom.Parameters.AddWithValue("@WOID", workOrderNumber);
                dc.ExecuteCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ResetStatus_EPCC2(int workOrderNumber, int DIJVApprover)
        {
            try
            {

                SqlCommand scom = new SqlCommand("Work_Order_Reset_EPCC2");
                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@UserID", DIJVApprover);
                scom.Parameters.AddWithValue("@WOID", workOrderNumber);
                dc.ExecuteCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ResetStatus_EPCC3(int workOrderNumber, int DIJVApprover)
        {
            try
            {

                SqlCommand scom = new SqlCommand("Work_Order_Reset_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@UserID", DIJVApprover);
                scom.Parameters.AddWithValue("@WOID", workOrderNumber);
                dc.ExecuteCommand(scom);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveWorkOrderRights(int EmpID, int actionID, string EmpName, out string msg)
        {
            try
            {

                SqlCommand scom = new SqlCommand("SaveWorkOrderRights");
                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@EmpID", EmpID);
                scom.Parameters.AddWithValue("@Action", actionID);
                scom.Parameters.AddWithValue("@name", EmpName);
                // scom.Parameters.AddWithValue("@Message", msg);
                SqlParameter message1 = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                scom.Parameters.Add(message1);
                message1.Value = "";
                message1.Direction = ParameterDirection.Output;
                message1.ParameterName = "Message";
                //scom.Parameters.AddWithValue("@Message", message1);
                dc.ExecuteCommand(scom);
                msg = message1.Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendExcepToDB(Exception exdb)
        {
            try
            {
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("ExceptionLoggingToDataBase");
                scom.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();

                parameter.SqlDbType = SqlDbType.Structured;

                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
                scom.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
                scom.Parameters.AddWithValue("@ExceptionURL", exepurl);
                scom.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());

                db.ExecuteCommandWithRef(ref scom);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public class SaveFile
        {
            public string fileName { set; get; }
            public string fileExtension { set; get; }
            public byte[] data { set; get; }

            public string SaveFileToDB(string Type, int ProjectId)
            {
                using (SqlConnection conn = new SqlConnection("Data Source=INMUM0926;" +
                     "Initial Catalog=DT_TRAT_backup;Integrated Security=True;Pooling=False"))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SaveFilesProc";
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@Data", data);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FileExtension", fileExtension);
                    cmd.Parameters.AddWithValue("@Type", Type);
                    cmd.Parameters.AddWithValue("@ProjectId", ProjectId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return "File stored Successfully!!!";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Dispose();
                        conn.Dispose();
                    }
                }
            }
        }

        public void UploadEPCCFile(string FileName, string ContentType, byte[] ByteCode, int ProjectId, out string message)
        {
            try
            {

                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("USP_SaveUploadFiles");
                scom.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Structured;
                scom.Parameters.AddWithValue("@FileName", FileName);
                scom.Parameters.AddWithValue("@FileExtension", ContentType);
                scom.Parameters.AddWithValue("@Data", ByteCode);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);

                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUserId());
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteCommandWithRef(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetFileData(int ProjectId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("USP_GET_Filedata");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DownloadProjectFile(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("USP_GET_FileDownload_Project");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DownloadFile(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("USP_GET_FileDownload");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFile(int Id, int ProjectId, out string message)
        {
            try
            {
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("USP_Delete_Filedata");
                scom.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Structured;
                scom.Parameters.AddWithValue("@ID", Id);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUserId());
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteCommandWithRef(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteProjectFile(int Id, int ProjectId, out string message)
        {
            try
            {
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("USP_Delete_ProjectFiledata");
                scom.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Structured;
                scom.Parameters.AddWithValue("@ID", Id);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUserId());
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteCommandWithRef(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetFileData_EPCC3(int ProjectId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("USP_GET_Filedata_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetProjectFileData(int ProjectId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("USP_GET_ProjectFiledata");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DownloadFile_EPCC3(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand scom = new SqlCommand("USP_GET_FileDownload_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@Id", Id);
                return dc.GetDataSetUsingCommand(scom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFile_EPCC3(int Id, int ProjectId, out string message)
        {
            try
            {
                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("USP_Delete_Filedata_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Structured;
                scom.Parameters.AddWithValue("@ID", Id);
                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUserId());
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteCommandWithRef(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UploadEPCCFile_EPCC3(string FileName, string ContentType, byte[] ByteCode, int ProjectId, out string message)
        {
            try
            {

                DBConnect db = new DBConnect();
                SqlCommand scom = new SqlCommand("USP_SaveUploadFiles_EPCC3");
                scom.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Structured;
                scom.Parameters.AddWithValue("@FileName", FileName);
                scom.Parameters.AddWithValue("@FileExtension", ContentType);
                scom.Parameters.AddWithValue("@Data", ByteCode);
                scom.Parameters.AddWithValue("@ProjectId", ProjectId);

                scom.Parameters.AddWithValue("@userID", CommonFunctions.GetUserId());
                scom.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteCommandWithRef(ref scom);

                message = scom.Parameters["@Message"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}