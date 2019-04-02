using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls;
//using Excel = Microsoft.Office.Interop.Excel;

namespace LibreriaHelper
{
    public static class ExcelHelper
    {
    //    [DllImport("user32")]
    //    private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

    //    public static bool uploadFile(FileUpload controlUpload, string targetPath, ref string err) {
    //        if (controlUpload.HasFile) {
    //            if (Path.GetExtension(controlUpload.FileName) == ".xls" || Path.GetExtension(controlUpload.FileName) == ".xlsx") {
    //                string filePath = Path.Combine(targetPath, controlUpload.FileName);
    //                // Verifica si existe el directorio
    //                if (!Directory.Exists(Path.GetDirectoryName(filePath))) {
    //                    err = "No existe el directorio " + Path.GetDirectoryName(filePath);
    //                    return false;
    //                }

    //                try {
    //                    controlUpload.SaveAs(filePath);
    //                    err = "";
    //                    return true;
    //                } catch(Exception uploadErr) {
    //                    err = "Ocurrio un error al subir el archivo. " + uploadErr.Message;
    //                    return false;
    //                }
    //            } else {
    //                //Util.showMessage(Page, "Debe seleccionar un archivo de excel valido.", "warn");
    //                err = "Debe seleccionar un archivo de excel valido.";
    //                return false;
    //            }
    //        } else {
    //            //Util.showMessage(Page, "Debe seleccionar un archivo", "warn");
    //            err = "Debe seleccionar un archivo";
    //            return false;
    //        }
    //    }

    //    public static DataTable excelFileToDataTable(FileUpload controlUploader, string targetPath, ref string err) {
    //        DataTable dt = new System.Data.DataTable();

    //        // Sube el archivo al servidor
    //        if (uploadFile(controlUploader, targetPath, ref err)) {
    //            string fullFilePath = Path.Combine(targetPath, controlUploader.FileName);

    //            // Verifica si el archivo existe
    //            if (!File.Exists(fullFilePath)) {
    //                //Util.showMessage(Page, "No existe el archivo " + fullFilePath, "warn");
    //                err = "";
    //                return dt;
    //            }

    //            string cnnStr = Path.GetExtension(controlUploader.FileName) == ".xls" ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullFilePath + ";Extended Properties=Excel 8.0" : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullFilePath + ";Extended Properties=Excel 12.0;";
    //            OleDbConnection cnn = new OleDbConnection(cnnStr);

    //            try {
    //                cnn.Open();

    //                DataTable dtSheets = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

    //                if (dtSheets.Rows.Count > 0) {
    //                    OleDbCommand cmd = new OleDbCommand("select * from [" + Lib.getText(dtSheets.Rows[0][2]) + "]", cnn);
    //                    OleDbDataAdapter da = new OleDbDataAdapter();
    //                    DataSet ds = new DataSet();

    //                    da.SelectCommand = cmd;
    //                    da.Fill(ds);

    //                    if (ds.Tables.Count > 0) {
    //                        cnn.Close();
    //                        //deleteFile(xlsPath.FileName);
    //                        return ds.Tables[0];
    //                    } else {
    //                        return dt;
    //                    }
    //                } else {
    //                    //Util.showMessage(Page, "No se reconoce el archivo", "warn");
    //                    err = "No se reconoce el archivo";
    //                    return dt;
    //                }
    //            } catch(Exception errCnn) {
    //                //Util.showMessage(Page, "ERROR: " + errCnn.Message.Replace('\'', '"'), "err");
    //                err = "ERROR: " + errCnn.Message;
    //                return dt;
    //            }
    //        } else {
    //            return dt;
    //        }
    //    }

    //    public static byte[] datatableToExcel(DataTable dt) {
    //        byte[] excelFile = new byte[0];

    //        try
    //        {
    //            // Init vars
    //            Excel.Application xlsApp;
    //            Excel.Workbook wb;
    //            Excel.Worksheet wSheet;
    //            Excel.Range rng;
    //            String tmpPath = HttpContext.Current.Server.MapPath(((String)ConfigurationManager.AppSettings["RUTACARGADEARCHIVOS"]));
    //            tmpPath = Path.Combine(tmpPath, DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xls");
    //            // Init excel app
    //            xlsApp = new Excel.Application();
    //            xlsApp.Visible = false;
    //            xlsApp.DisplayAlerts = false;

    //            // Creating new WB
    //            wb = xlsApp.Workbooks.Add(Type.Missing);
    //            // Work sheet  
    //            wSheet = (Excel.Worksheet)wb.ActiveSheet;
    //            wSheet.Name = "Sheet1";

    //            //Create Array object
    //            var data = new object[dt.Rows.Count, dt.Columns.Count];

    //            // Set headers
    //            for (int col = 0; col < dt.Columns.Count; col++) {
    //                data[0, col] = dt.Columns[col].ColumnName;
    //            }

    //            // Set whole info
    //            for (int row = 1; row < dt.Rows.Count; row++) {
    //                for (int col = 1; col < dt.Columns.Count; col++) {
    //                    data[row, col] = dt.Rows[row][col];
    //                }
    //            }

    //            // Create range
    //            Excel.Range init = (Excel.Range)wSheet.Cells[1, 1];
    //            Excel.Range final = (Excel.Range)wSheet.Cells[dt.Rows.Count, dt.Columns.Count];
    //            Excel.Range writeRng = wSheet.Range[init, final];

    //            // Write entire sheet at one
    //            writeRng.Value2 = data;

    //            // Style cell data
    //            rng = wSheet.Range[wSheet.Cells[1, 1], wSheet.Cells[dt.Rows.Count, dt.Columns.Count]];
    //            rng.EntireColumn.AutoFit();
    //            // Set cell border
    //            Excel.Borders border = rng.Borders;
    //            border.LineStyle = Excel.XlLineStyle.xlContinuous;
    //            border.Weight = 2d;

    //            // Cleaning vars
    //            wSheet = null;
    //            rng = null;
    //            wb.SaveAs(
    //                tmpPath
    //                , Excel.XlFileFormat.xlWorkbookNormal
    //                , Missing.Value
    //                , Missing.Value
    //                , Missing.Value
    //                , Missing.Value
    //                , Excel.XlSaveAsAccessMode.xlExclusive
    //                , Missing.Value
    //                , Missing.Value
    //                , Missing.Value
    //                , Missing.Value
    //                , Missing.Value
    //            );
    //            wb.Close();
    //            wb = null;
    //            xlsApp.Quit();

    //            // Get byte array
    //            excelFile = File.ReadAllBytes(tmpPath);
                
    //            // This process may be fail, we must have an error handler
    //            finishHim(xlsApp.Hwnd);
    //        }
    //        catch (Exception err)
    //        {
    //            Console.WriteLine(err.Message);
    //            return new byte[0];
    //        }
    //        finally {
    //            GC.WaitForPendingFinalizers();
    //            GC.Collect();
    //        }

    //        return excelFile;
    //    }

        
    //    private static bool finishHim(int hwnd) {
    //        int pid = 0;
    //        // Get PID
    //        GetWindowThreadProcessId(hwnd, out pid);

    //        // Get process collection by name
    //        Process[] pCollection = Process.GetProcessesByName("EXCEL");

    //        // Seek and destroy
    //        foreach (Process p in pCollection) {
    //            if (p.Id == pid) {
    //                p.Kill();
    //                return true;
    //            }
    //        }

    //        return false;
    //    }
    }
}
