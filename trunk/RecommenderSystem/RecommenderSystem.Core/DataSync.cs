using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core
{
    public class DataSync
    {
        public bool ImportExcelData(string path)
        {
            bool result = true;

            List<string> budgetSheet = new List<string>() { "Budget" };
            List<string> companionSheet = new List<string>() { "Companion" };
            //List<string> familiaritySheet = new List<string>() { "Familiarity" };
            //List<string> moodSheet = new List<string>() { "Mood" };
            List<string> placeCatSheet = new List<string>() { "Place_Categories" };
            List<string> placesSheet = new List<string>() { "Places" };
            List<string> realRatingSheet = new List<string>() { "Real_Ratings" };
            //List<string> temperatureSheet = new List<string>() { "Temperature" };
            //List<string> travelLengthSheet = new List<string>() { "Travel_Length" };
            List<string> usersSheet = new List<string>() { "Users" };
            List<string> weatherSheet = new List<string>() { "Weather" };

            foreach (string sheetName in ExcelHelper.findSheets(path, budgetSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "budget", "pr_insertNewBudget");
            }

            foreach (string sheetName in ExcelHelper.findSheets(path, companionSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "companion", "pr_insertNewCompanion");
            }

            /*foreach (string sheetName in ExcelHelper.findSheets(path, familiaritySheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "familiarity", "pr_insertNewFamiliarity");
            }

            foreach (string sheetName in ExcelHelper.findSheets(path, moodSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "mood", "pr_insertNewMood");
            }*/

            foreach (string sheetName in ExcelHelper.findSheets(path, placeCatSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "place_categories", "pr_insertNewPlaceCategory");
            }

            foreach (string sheetName in ExcelHelper.findSheets(path, placesSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "places", "pr_insertNewPlace");
            }

            /*foreach (string sheetName in ExcelHelper.findSheets(path, temperatureSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "temperature", "pr_insertNewTemperature");
            }

            foreach (string sheetName in ExcelHelper.findSheets(path, travelLengthSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "travel_length", "pr_insertNewTravelLength");
            }*/

            foreach (string sheetName in ExcelHelper.findSheets(path, weatherSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "weather", "pr_insertNewWeather");
            }

            foreach (string sheetName in ExcelHelper.findSheets(path, usersSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "users", "pr_insertNewUser");
            }

            foreach (string sheetName in ExcelHelper.findSheets(path, realRatingSheet))
            {
                result = result && ImportExcelSheetData(path, sheetName, "Real_Ratings", "pr_insertNewRating");
            }

            return result;
        }

        private bool ImportExcelSheetData(string path, string sheetName, string table_name, string sp_name)
        {
            try
            {
                DataTable data = ExcelHelper.GetDataFromExcel(path, sheetName);

                ////Special case for userSheet: generate password

                //if (table_name == "users")
                //    SystemHelper.EnrichUserData(ref data);

                //Delete old data

                List<string> dataIdToDelete = new List<string>();

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (!dataIdToDelete.Contains(data.Rows[i].ItemArray[0].ToString()))
                        dataIdToDelete.Add(data.Rows[i].ItemArray[0].ToString());
                }

                foreach (string item in dataIdToDelete)
                {
                    DbHelper.RunScripts(string.Format("delete from " + table_name + " where id = {0}", item), "Connection String");
                }

                // create the headerList
                List<string> headerList = new List<string>();
                List<DbType> typeList = new List<DbType>();

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    headerList.Add("@" + data.Columns[i].ColumnName);
                    if(data.Columns[i].ColumnName == "time")
                        typeList.Add(DbType.DateTime);
                    else
                        typeList.Add(DbType.String);
                }

                //insert new

                foreach (DataRow row in data.Rows)
                {
                    DbHelper.RunStoreProcNoDataTable(sp_name, headerList.ToArray(), row.ItemArray, typeList.ToArray());
                }
            }
            catch (Exception ex)
            {
                SystemHelper.LogEntry(ex.Message);
                return false;
            }
            return true;
        }
    }
}
