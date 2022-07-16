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
using XettuyenDGNLTHPT.Areas.Admin.Middleware;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    [LoginVerification]
    public class ManageImporDataController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/ManageImporData
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexTHPT()
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
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "[dbo].[tblTruongTHPT]";
                            sqlBulkCopy.ColumnMappings.Add("STT", "ID");
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
                return RedirectToAction("IndexTHPT");
            }
            catch
            {
                return RedirectToAction("IndexTHPT");
            }
        }
        public ActionResult DeleteTHPT(int id)
        {
            var Data = model.tblTruongTHPTs.Find(id);
            model.tblTruongTHPTs.Remove(Data);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditImportDataTHPT(int id)
        {
            var Data = model.tblTruongTHPTs.Find(id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult EditImportDataTHPT(tblTruongTHPT data)
        {
            if (ModelState.IsValid)
            {
                model.Entry(data).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("Index", "ManageImporData");
            }
            return View();
        }
        public ActionResult IndexCNN()
        {
            var CNN = model.tblChungChiNNs.ToList();
            return View(CNN);
        }
        public ActionResult ImportCNN(HttpPostedFileBase ExcelData)
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
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "[dbo].[tblChungChiNN]";
                            sqlBulkCopy.ColumnMappings.Add("Mã Ngoại Ngữ", "MaNN");
                            sqlBulkCopy.ColumnMappings.Add("Chứng Chỉ", "ChungChi");
                            sqlBulkCopy.ColumnMappings.Add("Điểm Quy Đổi", "DiemQuiDoi");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcel);
                            con.Close();

                        }
                    }
                }
                return RedirectToAction("IndexCNN");
            }
            catch
            {
                return RedirectToAction("IndexCNN");
            }
        }
        public ActionResult EditImportDataCNN(int id)
        {
            var Data = model.tblChungChiNNs.Find(id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult EditImportDataCNN(tblChungChiNN data)
        {
            if (ModelState.IsValid)
            {
                model.Entry(data).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("IndexCNN", "ManageImporData");
            }
            return View();
        }
        public ActionResult DeleteCNN(int id)
        {
            var Data = model.tblChungChiNNs.Find(id);
            model.tblChungChiNNs.Remove(Data);
            model.SaveChanges();
            return RedirectToAction("IndexCNN");
        }
        //Import TP_QH_PX
        public ActionResult IndexTP_QH_PX()
        {
            var TP_QH_PX = model.tblTP_QH_PX.ToList();
            return View(TP_QH_PX);
        }
        public ActionResult EditImportDataTP_QH_PX(int id)
        {
            var Data = model.tblTP_QH_PX.Find(id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult EditImportDataTP_QH_PX(tblTP_QH_PX data)
        {
            if (ModelState.IsValid)
            {
                model.Entry(data).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("IndexTP_QH_PX", "ManageImporData");
            }
            return View();
        }
        public ActionResult DeleteTP_QH_PX(int id)
        {
            var Data = model.tblTP_QH_PX.Find(id);
            model.tblTP_QH_PX.Remove(Data);
            model.SaveChanges();
            return RedirectToAction("IndexTP_QH_PX");
        }
        public ActionResult ImportTP_QH_PX(HttpPostedFileBase ExcelData)
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
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "[dbo].[tblTP_QH_PX]";
                            sqlBulkCopy.ColumnMappings.Add("Tỉnh Thành Phố", "TenTinhTP");
                            sqlBulkCopy.ColumnMappings.Add("Mã TP", "MaTinhTP");
                            sqlBulkCopy.ColumnMappings.Add("Quận Huyện", "TenQH");
                            sqlBulkCopy.ColumnMappings.Add("Mã QH", "MaQH");
                            sqlBulkCopy.ColumnMappings.Add("Phường Xã", "TenPX");
                            sqlBulkCopy.ColumnMappings.Add("Mã PX", "MaPX");
                            sqlBulkCopy.ColumnMappings.Add("Cấp", "Cap");
                            sqlBulkCopy.ColumnMappings.Add("Tên Tiếng Anh", "EnglishName");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcel);
                            con.Close();

                        }
                    }
                }
                return RedirectToAction("IndexTP_QH_PX");
            }
            catch
            {
                return RedirectToAction("IndexTP_QH_PX");
            }
        }
    }
}