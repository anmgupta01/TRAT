using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TRAT
{
    public class DBConnect
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection sql = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnection"].ToString());

            return sql;
        }


        public static SqlConnection GetConnection_Report()
        {
            SqlConnection sql = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnection_Report"].ToString());

            return sql;
        }
        public DataSet GetDataSetUsingCommand(SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();
            DataSet ds = new DataSet();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            return ds;
        }


        public DataSet GetDataSetUsingCommand_Report(SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();
            DataSet ds = new DataSet();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            return ds;
        }

        public string GetStringUsingCommand(SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();
            string ds = "";

            try
            {
                sql.Open();
                cmd.Connection = sql;
                ds = cmd.ExecuteScalar().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            return ds;
        }

        public string GetStringUsingCommand_Report(SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();
            string ds = "";

            try
            {
                sql.Open();
                cmd.Connection = sql;
                ds = cmd.ExecuteScalar().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            return ds;
        }


        public string GetOutStringCommand(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();
            string ds = "";

            try
            {
                sql.Open();
                cmd.Connection = sql;
                ds = cmd.ExecuteScalar().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            
            return ds;
        }

        public string GetOutStringCommand_Report(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();
            string ds = "";

            try
            {
                sql.Open();
                cmd.Connection = sql;
                ds = cmd.ExecuteScalar().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

            return ds;
        }

        public string GetCommand(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();
            string ds = "";

            try
            {
                sql.Open();
                cmd.Connection = sql;
                ds = cmd.ExecuteNonQuery().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

            return ds;
        }

        public string GetCommand_Report(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();
            string ds = "";

            try
            {
                sql.Open();
                cmd.Connection = sql;
                ds = cmd.ExecuteNonQuery().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

            return ds;
        }


        public void ExecuteCommand(SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

        }

        public void ExecuteCommand_Report(SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

        }

        public void ExecuteCommandWithRef(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

        }

        public void ExecuteCommandWithRef_Report(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception e)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }

        }

        public DataSet GetDataSetAndOutStringUsingCommand(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection();
            DataSet ds = new DataSet();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            return ds;
        }

        public DataSet GetDataSetAndOutStringUsingCommand_Report(ref SqlCommand cmd)
        {
            SqlConnection sql = GetConnection_Report();
            DataSet ds = new DataSet();

            try
            {
                sql.Open();
                cmd.Connection = sql;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                sql.Close();
                sql.Dispose();
            }
            catch (Exception)
            {
                sql.Close();
                sql.Dispose();
                throw;
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
            return ds;
        }
    }
}