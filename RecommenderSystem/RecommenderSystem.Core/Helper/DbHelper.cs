using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace RecommenderSystem.Core.Helper
{
    public static class DbHelper
    {
        public static bool Test()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("Connection String");
                DbCommand command = db.GetSqlStringCommand(string.Format("Select count(*) from mood"));
                command.CommandTimeout = 0;
                db.ExecuteNonQuery(command);

            }
            catch (Exception ex)
            {
                SystemHelper.LogEntry("Fail in DbHelper.cs\\Test(): " + ex.ToString() + "\n");
                throw ex;
                //return false;
            }
            return true;
        }

        public static bool TestADOMD()
        {
            try
            {
                AdomdConnection conn = new AdomdConnection(ConfigurationManager.ConnectionStrings["Cube"].ConnectionString);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                SystemHelper.LogEntry("Fail in DbHelper.cs\\TestADOMD(): " + ex.ToString() + "\n");
                throw ex;
            }
        }

        private static DataTable GetDataTableFromCellSet(CellSet cs)
        {
            //design the datatable
            DataTable dt = new DataTable();
            DataColumn dc = null;
            DataRow dr = null;

            //add the columns
            dt.Columns.Add(new DataColumn("Description"));
            //first column
            //get the other columns from axis
            string name = null;
            foreach (Position p in cs.Axes[0].Positions)
            {
                dc = new DataColumn();
                name = "";
                foreach (Member m in p.Members)
                {
                    name = name + m.Caption + " ";
                }
                dc.ColumnName = name;
                dt.Columns.Add(dc);
            }

            //add each row, row label first, then data cells
            int y = 0;
            y = 0;
            foreach (Position py in cs.Axes[1].Positions)
            {
                dr = dt.NewRow();
                //create new row

                // Do the row label
                name = "";
                foreach (Member m in py.Members)
                {
                    name = name + m.Caption + "";

                }
                dr[0] = name;
                //first cell in the row

                // Data cells
                int x = 0;
                for (x = 0; x <= cs.Axes[0].Positions.Count - 1; x++)
                {
                    dr[x + 1] = cs[x, y].FormattedValue;
                    //other cells in the row
                }

                dt.Rows.Add(dr);
                //add the row
                y = y + 1;
            }

            return dt;
        }

        public static DataTable RunMDXWithDataTable(String mdx)
        {
            try
            {
                AdomdConnection conn = new AdomdConnection(ConfigurationManager.ConnectionStrings["Cube"].ConnectionString);
                conn.Open();
                AdomdCommand command = conn.CreateCommand();
                command.CommandText = mdx;
                command.CommandType = CommandType.Text;
                //AdomdDataReader reader = command.ExecuteReader();
                CellSet cs = command.ExecuteCellSet();
                
                conn.Close();
                return GetDataTableFromCellSet(cs);
            }
            catch (Exception ex)
            {
                SystemHelper.LogEntry("Fail in DbHelper.cs\\RunMDX(): " + ex.ToString() + "\n");
                throw ex;
            }
        }

        public static bool RunScripts(String strSQL, string connectionString)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(connectionString);

                DbCommand command = db.GetSqlStringCommand(strSQL);
                command.CommandTimeout = 0;
                db.ExecuteNonQuery(command);
                return true;
            }

            catch (Exception ex)
            {
                SystemHelper.LogEntry("Fail in DbHelper.cs\\Runscripts(): " + ex.ToString() + "\n" + strSQL);
                throw ex;
                //return false;
            }

        }

        public static DataTable RunScriptsWithTable(String strSQL, string connectionString)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(connectionString);

                DbCommand command = db.GetSqlStringCommand(strSQL);
                command.CommandTimeout = 0;

                DataSet result = db.ExecuteDataSet(command);
                return result.Tables[0];
            }

            catch (Exception ex)
            {
                SystemHelper.LogEntry("Fail in DbHelper.cs\\RunscriptsWithTable(): " + ex.ToString() + "\n" + strSQL);
                throw ex;
                //return null;
            }
        }

        public static DataSet RunScriptsWithMultipleTable(String strSQL, string connectionString)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase(connectionString);
                //DataSet result = db.ExecuteDataSet(CommandType.Text, strSQL);
                DbCommand command = db.GetSqlStringCommand(strSQL);
                command.CommandTimeout = 0;
                DataSet result = db.ExecuteDataSet(command);

                return result;
            }

            catch (Exception ex)
            {
                SystemHelper.LogEntry("Fail in DbHelper.cs\\RunScriptsWithMultipleTable(): " + ex.ToString() + "\n" + strSQL);
                throw ex;
                //return null;
            }
        }

        public static int RunStoreProcNoDataTable(string storeProcName, string[] parameterList, object[] valueList, DbType[] typeList)
        {
            string sp_Name = storeProcName;
            try
            {
                
                //Fix datetime
                for (int i = 0; i < parameterList.Count(); i++)
                {
                    if (typeList[i] == DbType.DateTime)
                        //valueList[i] = Convert.ToDateTime(Convert.ToDouble(valueList[i]));
                        valueList[i] = DateTime.FromOADate(Convert.ToDouble(valueList[i]));
                }

                Database db = DatabaseFactory.CreateDatabase("Connection String");
                DbCommand command = db.GetStoredProcCommand(sp_Name);
                command.CommandTimeout = 0;
                if (parameterList != null && (parameterList.Count() == valueList.Count()))
                {
                    for (int i = 0; i < parameterList.Count(); i++)
                    {
                        db.AddInParameter(command, parameterList[i], typeList[i]);
                    }

                    for (int i = 0; i < parameterList.Count(); i++)
                    {
                        db.SetParameterValue(command, parameterList[i], valueList[i]);
                    }
                }
                else
                    return -1;




                int result = db.ExecuteNonQuery(command);


                return result;


            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(string.Format("Error occurs on method {0}, store name: {2} - Message {1}", "RunStoreProcNoDataTable", e.ToString(), storeProcName));
                throw e;
                return -1;
            }
        }

        public static bool RunInsertStoreProcNoDataTable(DataTable data, string storeprocName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("Connection String");

                DbCommand qcommand = db.GetStoredProcCommand(storeprocName);
                qcommand.CommandTimeout = 0;
                List<string> headerList = new List<string>();
                List<DbType> dbTypeList = new List<DbType>();

                // create the headerList
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    headerList.Add("@" + data.Columns[i].ColumnName);
                    dbTypeList.Add(DbType.String);
                }

                foreach (DataRow row in data.Rows)
                {
                    int result = DbHelper.RunStoreProcNoDataTable(storeprocName, headerList.ToArray(), row.ItemArray, dbTypeList.ToArray());
                    result = result + 1;
                }

                return true;
            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(e.ToString() + "\n" + storeprocName);
                throw e;
                return false;
            }
        }
    }
}
