using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

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

        public static bool RunScripts(String strSQL)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("Connection String");

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

        public static DataTable RunScriptsWithTable(String strSQL)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("Connection String");

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

        public static DataSet RunScriptsWithMultipleTable(String strSQL)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase("Connection String");
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
