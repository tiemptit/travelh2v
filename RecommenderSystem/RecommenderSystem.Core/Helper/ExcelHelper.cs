using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Text.RegularExpressions;

namespace RecommenderSystem.Core.Helper
{
    public class ExcelHelper
    {
        private static void SetCellValue(SharedStringTablePart shareStringTablePart, Cell cell, string value)
        {


            if (cell == null || cell.CellValue.Text.Equals(string.Empty))
            {
                return;
            }
            else
            {
                if (cell.DataType.Value == CellValues.SharedString)
                {
                    int shareStringId = int.Parse(cell.CellValue.Text);
                    SharedStringItem item = shareStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(shareStringId);

                    item.Elements<Text>().First().Text = value;

                }
                else
                {
                    cell.CellValue.Text = value;
                }
            }
        }

        public static DataTable GetDataFromExcel(string pathToExcelFile, string sheetName)
        {
            DataTable result = new DataTable(sheetName);
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(pathToExcelFile, false))
                {
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName);
                    if (sheets.Count() == 0)
                    {
                        return null;
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
                    SharedStringTablePart shareStringTablePart = document.WorkbookPart.SharedStringTablePart;
                    int rowIndex = 1;
                    int totalColumn = 0;
                    int cellIndex = 0;

                    // this is for the header row only
                    Row row = GetRow(rowIndex, worksheetPart);
                    String cellValue = "";
                    while (true)
                    {
                        Cell cell = GetCell(cellIndex, row);
                        cellValue = GetCellValue(shareStringTablePart, cell);
                        if (cellValue != null)
                        {
                            result.Columns.Add(cellValue);
                            cellIndex += 1;
                            totalColumn += 1;
                        }
                        else
                            break;
                    }

                    rowIndex += 1;

                    while (true)
                    {
                        row = GetRow(rowIndex, worksheetPart);
                        if (row == null)
                        {
                            break;
                        }

                        System.Collections.ArrayList cellValues = new System.Collections.ArrayList();

                        int lastcell = 0;
                        bool isBreak = true;
                        for (cellIndex = 0; cellIndex < totalColumn; cellIndex++)
                        {
                            Cell cell = GetCell(cellIndex, row);
                            cellValue = GetCellValue(shareStringTablePart, cell);

                            if (cellValue != null)
                            {
                                // skip to the number of missing cell
                                while ((lastcell + 1) < GetNumber(Regex.Replace(cell.CellReference, @"\d", "")))
                                {
                                    if (cellValues.Count < totalColumn)
                                        cellValues.Add("");
                                    lastcell++;
                                }
                                lastcell = GetNumber(Regex.Replace(cell.CellReference, @"\d", ""));
                                if (cellValues.Count < totalColumn)
                                    cellValues.Add(cellValue);
                                isBreak = false;
                            }
                        }

                        if (isBreak)
                            break;

                        result.Rows.Add(cellValues.ToArray());
                        rowIndex++;
                    }
                    document.Close();
                }

            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(string.Format("Error occurs on method {0} - Message {1}", "GetDataFromExcel", e.Message));
                return null;
            }
            return result;

        }


        Cell CreateTextCell(string header, string text, int index)
        {
            //Create a new inline string cell.
            Cell c = new Cell();
            c.DataType = CellValues.InlineString;
            c.CellReference = header + index;
            //Add text to the text cell.
            InlineString inlineString = new InlineString();
            Text t = new Text();
            t.Text = text;
            inlineString.AppendChild(t);
            c.AppendChild(inlineString);
            return c;
        }

        public static List<string> findSheets(string pathToExcelFile, List<string> sheetsToSearch)
        {
            List<String> result = new List<string>();
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(pathToExcelFile, false))
            {

                foreach (String sheetToSearch in sheetsToSearch)
                {
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name.Value.ToLower().Contains(sheetToSearch.ToLower()));
                    if (sheets.Count() == 0) // the sheet with that name couldn't be found
                    {
                        // do nothign here
                    }
                    else
                    {
                        foreach (Sheet sheet in sheets)
                        {
                            if (!result.Contains(sheet.Name.ToString()))
                                result.Add(sheet.Name.ToString());
                        }
                    }
                }
            }

            return result;
        }


        public static bool SetDataToExcel(string pathToExcelFile, string sheetName, DataTable data)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.ApplicationClass();

                Microsoft.Office.Interop.Excel.Workbook wb;
                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //15/03/2011 Template ?
                    System.IO.File.Copy(SystemHelper.GetConfigValue("appStartupPath") + "\\template\\newWorkbook.xlsx", pathToExcelFile);
                }


                wb = app.Workbooks.Open(pathToExcelFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //Find sheetname
                int index = 1;
                for (index = 1; index <= wb.Sheets.Count; index++)
                {
                    if (((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Name.ToLower().Trim() == sheetName.ToLower().Trim())
                        break;
                }

                if (index > wb.Sheets.Count)
                {
                    wb.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Activate();
                }



                Microsoft.Office.Interop.Excel.Worksheet activeSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                activeSheet.Name = sheetName;
                //activeSheet.Cells.FormatConditions = 

                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    wb.Save();
                }



                // exit excel, still now work so far
                wb.Close(true, Type.Missing, Type.Missing);
                app.Workbooks.Close();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app.Workbooks);
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(app) != 0)
                { };
                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Close();

                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Kill();


                using (SpreadsheetDocument document = SpreadsheetDocument.Open(pathToExcelFile, true))
                {
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName);
                    if (sheets.Count() == 0) // the sheet with that name couldn't be found
                    {
                        return false;
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);

                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    //sheetData.RemoveAllChildren<Row>();

                    //SharedStringTablePart shareStringTablePart = document.WorkbookPart.SharedStringTablePart;

                    int lastRowIndex = sheetData.ChildElements.Count;

                    for (int i = lastRowIndex; i < data.Rows.Count + 1 + lastRowIndex; i++) // +1 for the header label
                    {
                        DataRow dataRow = null;
                        if (i > lastRowIndex)
                        {
                            dataRow = data.Rows[i - 1 - lastRowIndex];
                        }
                        Row aRow = new Row();
                        aRow.RowIndex = (UInt32)i + 1;

                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.InlineString;

                            cell.CellReference = GetAlpha(j + 1) + (i + 1);

                            InlineString inlineString = new InlineString();

                            Text t = new Text();


                            if (i == lastRowIndex)
                            {
                                t.Text = data.Columns[j].ToString();
                                cell.StyleIndex = (UInt32Value)1U;
                            }
                            else
                            {
                                t.Text = dataRow[j].ToString();
                            }

                            inlineString.AppendChild(t);

                            cell.AppendChild(inlineString);
                            aRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(aRow);
                    }

                    System.Xml.XmlWriter test = System.Xml.XmlWriter.Create("Test.xml");
                    worksheetPart.Worksheet.WriteTo(test);
                    test.Close();

                    worksheetPart.Worksheet.Save();

                    document.WorkbookPart.Workbook.Save();
                    document.Close();
                }

            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(string.Format("Error occurs on method {0} - Message {1}", "GetDataFromExcel", e.Message));
                return false;
            }
            return true;

        }

        public static bool SetDataToExcel(string pathToExcelFile, string sheetName, DataTable data, string title)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.ApplicationClass();

                Microsoft.Office.Interop.Excel.Workbook wb;
                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //15/03/2011 Template ?
                    System.IO.File.Copy(SystemHelper.GetConfigValue("appStartupPath") + "\\template\\newWorkbook.xlsx", pathToExcelFile);
                }


                wb = app.Workbooks.Open(pathToExcelFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //Find sheetname
                int index = 1;
                for (index = 1; index <= wb.Sheets.Count; index++)
                {
                    if (((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Name.ToLower().Trim() == sheetName.ToLower().Trim())
                        break;
                }

                if (index > wb.Sheets.Count)
                {
                    wb.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Activate();
                }



                Microsoft.Office.Interop.Excel.Worksheet activeSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                activeSheet.Name = sheetName;
                //activeSheet.Cells.FormatConditions = 

                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    wb.Save();
                }



                // exit excel, still now work so far
                wb.Close(true, Type.Missing, Type.Missing);
                app.Workbooks.Close();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app.Workbooks);
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(app) != 0)
                { };
                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Close();

                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Kill();


                using (SpreadsheetDocument document = SpreadsheetDocument.Open(pathToExcelFile, true))
                {
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName);
                    if (sheets.Count() == 0) // the sheet with that name couldn't be found
                    {
                        return false;
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);

                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    //sheetData.RemoveAllChildren<Row>();

                    //SharedStringTablePart shareStringTablePart = document.WorkbookPart.SharedStringTablePart;

                    int lastRowIndex = sheetData.ChildElements.Count;


                    for (int i = lastRowIndex; i < data.Rows.Count + 3 + lastRowIndex; i++) // +1 for the header label, + 1 for title, + 1 for spacing row
                    {
                        DataRow dataRow = null;

                        if (i > lastRowIndex + 2)
                        {
                            dataRow = data.Rows[i - 3 - lastRowIndex];
                        }
                        Row aRow = new Row();
                        aRow.RowIndex = (UInt32)i + 1;

                        bool isRowEnd = false;//Fix the excel expand

                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            if (isRowEnd)
                            {
                                break; //Fix the excel expand
                            }
                            Cell cell = new Cell();
                            cell.DataType = CellValues.InlineString;

                            cell.CellReference = GetAlpha(j + 1) + (i + 1);

                            InlineString inlineString = new InlineString();

                            Text t = new Text();

                            if (i == lastRowIndex)//Title
                            {
                                if (j == 0)
                                {
                                    t.Text = title;
                                    cell.StyleIndex = (UInt32Value)4U;
                                    isRowEnd = true;
                                }
                            }
                            else if (i == lastRowIndex + 2)//Header
                            {
                                t.Text = data.Columns[j].ToString();
                                cell.StyleIndex = (UInt32Value)1U;
                            }
                            else
                            {
                                if (i != lastRowIndex + 1)//Null spacing row
                                    t.Text = dataRow[j].ToString();//Data row
                            }

                            inlineString.AppendChild(t);

                            cell.AppendChild(inlineString);
                            aRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(aRow);
                    }

                    System.Xml.XmlWriter test = System.Xml.XmlWriter.Create("Test.xml");
                    worksheetPart.Worksheet.WriteTo(test);
                    test.Close();

                    worksheetPart.Worksheet.Save();

                    document.WorkbookPart.Workbook.Save();
                    document.Close();
                }

            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(string.Format("Error occurs on method {0} - Message {1}", "GetDataFromExcel", e.Message));
                return false;
            }
            return true;

        }


        public static bool SetDataToExcel(string template, string pathToExcelFile, string sheetName, DataTable data, string title)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.ApplicationClass();

                Microsoft.Office.Interop.Excel.Workbook wb;
                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //15/03/2011 Template ?
                    System.IO.File.Copy(SystemHelper.GetConfigValue("appStartupPath") + "\\template\\" + template + ".xlsx", pathToExcelFile);
                }


                wb = app.Workbooks.Open(pathToExcelFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //Find sheetname
                int index = 1;
                for (index = 1; index <= wb.Sheets.Count; index++)
                {
                    if (((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Name.ToLower().Trim() == sheetName.ToLower().Trim())
                        break;
                }

                if (index > wb.Sheets.Count)
                {
                    wb.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Activate();
                }



                Microsoft.Office.Interop.Excel.Worksheet activeSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                activeSheet.Name = sheetName;
                //activeSheet.Cells.FormatConditions = 

                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    wb.Save();
                }



                // exit excel, still now work so far
                wb.Close(true, Type.Missing, Type.Missing);
                app.Workbooks.Close();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app.Workbooks);
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(app) != 0)
                { };
                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Close();

                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Kill();


                using (SpreadsheetDocument document = SpreadsheetDocument.Open(pathToExcelFile, true))
                {
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName);
                    if (sheets.Count() == 0) // the sheet with that name couldn't be found
                    {
                        return false;
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);

                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    //sheetData.RemoveAllChildren<Row>();

                    //SharedStringTablePart shareStringTablePart = document.WorkbookPart.SharedStringTablePart;

                    int lastRowIndex = sheetData.ChildElements.Count;


                    for (int i = lastRowIndex; i < data.Rows.Count + 3 + lastRowIndex; i++) // +1 for the header label, + 1 for title, + 1 for spacing row
                    {
                        DataRow dataRow = null;

                        if (i > lastRowIndex + 2)
                        {
                            dataRow = data.Rows[i - 3 - lastRowIndex];
                        }
                        Row aRow = new Row();
                        aRow.RowIndex = (UInt32)i + 1;

                        bool isRowEnd = false;//Fix the excel expand

                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            if (isRowEnd)
                            {
                                break; //Fix the excel expand
                            }
                            Cell cell = new Cell();
                            cell.DataType = CellValues.InlineString;

                            cell.CellReference = GetAlpha(j + 1) + (i + 1);

                            InlineString inlineString = new InlineString();

                            Text t = new Text();

                            if (i == lastRowIndex)//Title
                            {
                                if (j == 0)
                                {
                                    t.Text = title;
                                    cell.StyleIndex = (UInt32Value)4U;
                                    isRowEnd = true;
                                }
                            }
                            else if (i == lastRowIndex + 2)//Header
                            {
                                t.Text = data.Columns[j].ToString();
                                cell.StyleIndex = (UInt32Value)1U;
                            }
                            else
                            {
                                if (i != lastRowIndex + 1)//Null spacing row
                                    t.Text = dataRow[j].ToString();//Data row
                            }

                            inlineString.AppendChild(t);

                            cell.AppendChild(inlineString);
                            aRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(aRow);
                    }

                    System.Xml.XmlWriter test = System.Xml.XmlWriter.Create("Test.xml");
                    worksheetPart.Worksheet.WriteTo(test);
                    test.Close();

                    worksheetPart.Worksheet.Save();

                    document.WorkbookPart.Workbook.Save();
                    document.Close();
                }

            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(string.Format("Error occurs on method {0} - Message {1}", "GetDataFromExcel", e.Message));
                return false;
            }
            return true;

        }

        public static bool SetDataToExcel(string pathToExcelFile, string sheetName, DataTable data, string[] intro, string title)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.ApplicationClass();

                Microsoft.Office.Interop.Excel.Workbook wb;
                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //15/03/2011 Template ?
                    System.IO.File.Copy(SystemHelper.GetConfigValue("appStartupPath") + "\\template\\newWorkbook.xlsx", pathToExcelFile);
                }


                wb = app.Workbooks.Open(pathToExcelFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //Find sheetname
                int index = 1;
                for (index = 1; index <= wb.Sheets.Count; index++)
                {
                    if (((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Name.ToLower().Trim() == sheetName.ToLower().Trim())
                        break;
                }

                if (index > wb.Sheets.Count)
                {
                    wb.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[index]).Activate();
                }



                Microsoft.Office.Interop.Excel.Worksheet activeSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                activeSheet.Name = sheetName;
                //activeSheet.Cells.FormatConditions = 

                if (!System.IO.File.Exists(pathToExcelFile))
                {
                    //wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    wb.SaveAs(pathToExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    wb.Save();
                }



                // exit excel, still now work so far
                wb.Close(true, Type.Missing, Type.Missing);
                app.Workbooks.Close();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app.Workbooks);
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(app) != 0)
                { };
                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Close();

                //System.Diagnostics.Process.GetProcessById(app.Windows.Application.Hwnd).Kill();


                using (SpreadsheetDocument document = SpreadsheetDocument.Open(pathToExcelFile, true))
                {
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName);
                    if (sheets.Count() == 0) // the sheet with that name couldn't be found
                    {
                        return false;
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);

                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    //sheetData.RemoveAllChildren<Row>();

                    //SharedStringTablePart shareStringTablePart = document.WorkbookPart.SharedStringTablePart;

                    int lastRowIndex = sheetData.ChildElements.Count;
                    int k = 0; //step in intro
                    for (int i = lastRowIndex; i < data.Rows.Count + 4 + lastRowIndex + intro.Length; i++) // +1 for the header label, + 2 for spacing row, + 1 for title
                    {
                        DataRow dataRow = null;

                        if (i > lastRowIndex + 3 + intro.Length)
                        {
                            dataRow = data.Rows[i - 4 - lastRowIndex - intro.Length];
                        }
                        Row aRow = new Row();
                        aRow.RowIndex = (UInt32)i + 1;

                        bool isRowEnd = false;//Fix the excel expand

                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            if (isRowEnd)
                            {
                                break;//Fix the excel expand
                            }
                            Cell cell = new Cell();
                            cell.DataType = CellValues.InlineString;

                            cell.CellReference = GetAlpha(j + 1) + (i + 1);

                            InlineString inlineString = new InlineString();

                            Text t = new Text();

                            if (i <= lastRowIndex + intro.Length - 1)//Intro
                            {
                                if (j == 0)
                                {
                                    t.Text = intro[k];
                                    k++;
                                    cell.StyleIndex = (UInt32Value)3U;
                                    isRowEnd = true;
                                }
                            }
                            else if (i == lastRowIndex + intro.Length + 1)//Title
                            {
                                if (j == 0)
                                {
                                    t.Text = title;
                                    cell.StyleIndex = (UInt32Value)4U;
                                    isRowEnd = true;
                                }

                            }
                            else if (i == lastRowIndex + 3 + intro.Length)//Header
                            {
                                t.Text = data.Columns[j].ToString();
                                cell.StyleIndex = (UInt32Value)1U;
                            }
                            else
                            {
                                if (i != intro.Length + lastRowIndex && i != intro.Length + lastRowIndex + 2)//Null spacing row
                                    t.Text = dataRow[j].ToString();//Data row
                            }


                            inlineString.AppendChild(t);

                            cell.AppendChild(inlineString);
                            aRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(aRow);
                    }

                    System.Xml.XmlWriter test = System.Xml.XmlWriter.Create("Test.xml");
                    worksheetPart.Worksheet.WriteTo(test);
                    test.Close();

                    worksheetPart.Worksheet.Save();

                    document.WorkbookPart.Workbook.Save();
                    document.Close();
                }

            }
            catch (Exception e)
            {
                SystemHelper.LogEntry(string.Format("Error occurs on method {0} - Message {1}", "GetDataFromExcel", e.Message));
                return false;
            }
            return true;

        }

        //public static DataTable SetDataToExcel(string pathToExcelFile, DataTable sampleData)
        //{
        //    //DataTable result = new DataTable(sheetName);
        //    try
        //    {
        //        using (SpreadsheetDocument document = SpreadsheetDocument.Create(pathToExcelFile,SpreadsheetDocumentType.Workbook))
        //        {
        //            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
        //            if (sheets.Count() == 0)
        //            {
        //                return null;
        //            }

        //            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
        //            SharedStringTablePart shareStringTablePart = document.WorkbookPart.SharedStringTablePart;
        //            int rowIndex = 1;
        //            int totalColumn = 0;
        //            int cellIndex = 0;

        //            // this is for the header row only
        //            Row row = GetRow(rowIndex, worksheetPart);
        //            String cellValue = "";
        //            foreach (DataColumn column in sampleData.Columns)
        //            {
        //                Cell cell = GetCell(cellIndex, row);

        //                SetCellValue(shareStringTablePart, cell, column.ToString());
        //                cellIndex += 1;						
        //            }

        //            rowIndex += 1;

        //            foreach (DataRow datarow in sampleData.Rows)
        //            {

        //                row = GetRow(rowIndex, worksheetPart);


        //                if (row == null)
        //                {
        //                    break;
        //                }

        //                for (int i = 0; i < sampleData.Columns.Count; i++)
        //                {
        //                    Cell cell = GetCell(i, row);
        //                    SetCellValue(shareStringTablePart, cell, datarow[i].ToString());
        //                }
        //                rowIndex++;
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        SystemHelper.LogEntry(string.Format("Error occurs on method {0} - Message {1}", "GetDataFromExcel", e.Message));
        //        return null;
        //    }


        //}

        private static int GetNumber(string column)
        {
            if (column.Length == 1)
                return (int)column[0] - 64;
            if (column.Length == 2)
                return (int)column[1] - 64 + (column[0] - 64) * 26;
            return 0;
        }

        private static string GetAlpha(int num)
        {
            const int A = 65;    //ASCII value for capital A
            string sCol = string.Empty;
            int iRemain = 0;
            // THIS ALGORITHM ONLY WORKS UP TO ZZ. It fails on AAA
            if (num > 701)
            {
                return string.Empty;
            }
            if (num <= 26)
            {
                if (num == 0)
                {
                    sCol = Convert.ToChar((A + 26) - 1).ToString(); ;
                }
                else
                {
                    sCol = Convert.ToChar((A + num) - 1).ToString();
                }
            }
            else
            {
                iRemain = ((num / 26)) - 1;
                if ((num % 26) == 0)
                {
                    sCol = GetAlpha(iRemain) + GetAlpha(num % 26);
                }
                else
                {
                    sCol = Convert.ToChar(A + iRemain) + GetAlpha(num % 26);
                }
            }
            return sCol;

        }


        #region private static Methods
        /// <summary>
        /// Get a row with specified index, return null if row has nodata
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="worksheetPart"></param>
        /// <returns></returns>
        private static Row GetRow(int rowIndex, WorksheetPart worksheetPart)
        {
            try
            {
                return worksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First<Row>();
            }
            catch (Exception ex)
            {
                return null;
                //Row aRow = new Row();
                //aRow.RowIndex = 0;
                //worksheetPart.Worksheet.GetFirstChild<SheetData>().Append((aRow));
                //return worksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First<Row>();


            }
        }

        /// <summary>
        /// Get a cell with specified name & row. Return null if cell has no sampleData.
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private static Cell GetCell(string cellName, Row row)
        {
            try
            {
                return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, cellName, true) == 0).First<Cell>();
            }
            catch
            {
                return null;
            }
        }


        static string GetCellValue(WorksheetPart worksheetPart, SharedStringTablePart stringTablePart, string startCol, string startRow)
        {
            string reference = startCol + startRow;
            //get exact cell based on reference
            IEnumerable<Cell> cell = worksheetPart.Worksheet.Descendants<Cell>()
            .Where(c => reference.Equals(c.CellReference));
            if (cell.Count() != 0)
                return GetValue(cell.First(), stringTablePart);
            else
                return null;
        }


        static string GetValue(Cell cell, SharedStringTablePart stringTablePart)
        {
            if (cell.ChildElements.Count == 0)
                return null;
            //get cell value
            string value = cell.CellValue.InnerText;
            //Look up real value from shared string table
            if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                value = stringTablePart.SharedStringTable
                .ChildElements[Int32.Parse(value)]
                .InnerText;
            return value;
        }

        private static Cell GetCell(int cellIndex, Row row)
        {
            try
            {
                return row.Elements<Cell>().ElementAt(cellIndex);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Get cell's value depend on its style: share string or text
        /// </summary>
        /// <param name="shareStringTablePart"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static string GetCellValue(SharedStringTablePart shareStringTablePart, Cell cell)
        {
            if (cell == null || cell.CellValue == null || cell.CellValue.Text.Equals(string.Empty))
            {
                return null;
            }
            else
            {
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    int shareStringId = int.Parse(cell.CellValue.Text);
                    SharedStringItem item = shareStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(shareStringId);
                    return item.InnerText;
                }
                else
                {
                    return cell.CellValue.Text;
                }
            }
        }
        #endregion
    }
}
