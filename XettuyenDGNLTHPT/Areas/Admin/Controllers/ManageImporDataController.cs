using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Data.Entity;
using OfficeOpenXml;
using XettuyenDGNLTHPT.Models;
using System.Data.SqlClient;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class ManageImporDataController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/ManageImporData
        public ActionResult Index()
        {
            var truong = model.tblTruongTHPTs.ToList();
            return View(truong);
        }
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase ExcelData)
        {
            try
            {
                string filepath = string.Empty;
                if (ExcelData != null)
                {
                    string path = Server.MapPath("~/Upload");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filepath = path + Path.GetFileName(ExcelData.FileName);
                    string extension = Path.GetExtension(ExcelData.FileName);
                    ExcelData.SaveAs(filepath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls":
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx":
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                      
                    }

                    DataTable dtExcel = new DataTable();
                    conString = string.Format(conString, filepath);
                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;
                                connExcel.Open();
                                DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0].Field<string>("TABLE_NAME");
                                connExcel.Close();
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * from [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dtExcel);
                                connExcel.Close();
                            }
                        }
                    }
                    conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using(SqlConnection con = new SqlConnection(conString))
                    {
                        using(SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "[dbo].[tblTruongTHPT]";
                            sqlBulkCopy.ColumnMappings.Add("STT","ID");
                            sqlBulkCopy.ColumnMappings.Add("Mã TpTruong", "MA_TPTRUONG");
                            sqlBulkCopy.ColumnMappings.Add("Mã Tỉnh/TP", "MA_TINHTP");
                            sqlBulkCopy.ColumnMappings.Add("Tên Tỉnh/TP", "TEN_TINHTP");
                            sqlBulkCopy.ColumnMappings.Add("Mã Quận/Huyện", "MA_QH");
                            sqlBulkCopy.ColumnMappings.Add("Tên Quận/Huyện", "TEN_QH");
                            sqlBulkCopy.ColumnMappings.Add("Mã Trường", "MA_TRUONG");
                            sqlBulkCopy.ColumnMappings.Add("Tên Trường", "TEN_TRUONG");
                            sqlBulkCopy.ColumnMappings.Add("Địa Chỉ", "DIA_CHI");
                            sqlBulkCopy.ColumnMappings.Add("Khu Vực", "KHU_VUC");
                            sqlBulkCopy.ColumnMappings.Add("Trường DTNT", "LOAI_TRUONG");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcel);
                            con.Close();

                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}