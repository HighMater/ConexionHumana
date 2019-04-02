using System;
using System.IO;
using System.Web;

namespace LibreriaHelper
{
    public static class FileHelper
    {
        public static bool downloadFile(string fileName, byte[] fileArray, ref string err) {
            try {
                HttpContext.Current.Response.ContentType = GetContentType(fileName.ToLower());
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                HttpContext.Current.Response.BinaryWrite(fileArray);
                HttpContext.Current.Response.Flush();
                return true;
            } catch (Exception ex) {
                err = ex.Message;
                return false;
            }
        }

        public static bool downloadFile(string fullFileName, ref string err) {
            if (!File.Exists(fullFileName)) {
                err = "No existe el archivo " + fullFileName + " debe especificar la ruta completa del archivo.";
                return false;
            }

            try {
                byte[] fileArray = File.ReadAllBytes(fullFileName);
                string fileName = Path.GetFileName(fullFileName);
                HttpContext.Current.Response.ContentType = GetContentType(fileName);
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                HttpContext.Current.Response.BinaryWrite(fileArray);
                HttpContext.Current.Response.Flush();
                err = "";
                return true;
            } catch (Exception ex) {
                err = ex.Message;
                return false;
            }
        }

        private static string GetContentType(string fileExtension) {
            string ext = Path.GetExtension(fileExtension).ToLower();
            switch (ext) {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
