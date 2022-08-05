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
using System.Windows.Forms;

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
        public ActionResult AddTHPT()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddTHPT(tblTruongTHPT THPT)
        {
            int lastId = model.tblTruongTHPTs.Max(item => item.ID);
            THPT.MA_TPTRUONG = THPT.MA_TINHTP + THPT.MA_TRUONG;
            THPT.ID = lastId + 1;
            model.tblTruongTHPTs.Add(THPT);
            model.SaveChanges();
            return RedirectToAction("IndexTHPT", "ManageImporData");
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
                        using (SqlCommand command = new SqlCommand("", con))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {

                                sqlBulkCopy.DestinationTableName = "[dbo].[tblTruongTHPT]";
                                sqlBulkCopy.ColumnMappings.Add("STT", "ID");
                                sqlBulkCopy.ColumnMappings.Add("MãTpTruong", "MA_TPTRUONG");
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
                                bool check1 = false;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    string userName = dtExcel.Rows[i]["STT"].ToString();
                                    int id = int.Parse(userName);
                                    var check = model.tblTruongTHPTs.FirstOrDefault(t => t.ID==id);
                                    if (check != null)
                                    {
                                        check1 = true;
                                        break;
                                    }
                                    else
                                    {
                                        check1 = false;
                                        break;
                                    }
                                }
                                if (check1 == false)
                                {
                                    sqlBulkCopy.WriteToServer(dtExcel);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("Có dữ liệu trùng lặp bạn có muốn lưu không", "Dữ liệu đã tồn tại", MessageBoxButtons.YesNo);
                                    if(dialogResult == DialogResult.Yes)
                                    {
                                        command.CommandText = "CREATE TABLE #TmpTable([ID] [int] NOT NULL," +
                                            "[MA_TPTRUONG] [nvarchar](20) NULL," +
                                            "[MA_TINHTP] [nvarchar](5) NULL," +
                                            "[TEN_TINHTP] [nvarchar](255) NULL," +
                                            "[MA_QH] [nvarchar](255) NULL," +
                                            "[TEN_QH][nvarchar](255) NULL," +
                                            "[MA_TRUONG] [nvarchar](20) NULL," +
                                            "[TEN_TRUONG] [nvarchar](255) NULL," +
                                            "[DIA_CHI] [nvarchar](255) NULL," +
                                            "[KHU_VUC] [nvarchar](255) NULL," +
                                            "[LOAI_TRUONG] [nvarchar](255) NULL)";
                                        command.ExecuteNonQuery();

                                        //Bulk insert into temp table

                                        //sqlBulkCopy.BulkCopyTimeout = 660;
                                        sqlBulkCopy.DestinationTableName = "#TmpTable";
                                        sqlBulkCopy.WriteToServer(dtExcel);
                                        sqlBulkCopy.Close();
                                        command.CommandText = "UPDATE tblTruongTHPT SET tblTruongTHPT.ID = Temp.ID, " +
                                                                "tblTruongTHPT.MA_TRUONG=Temp.MA_TRUONG," +
                                                                "tblTruongTHPT.MA_TPTRUONG=Temp.MA_TPTRUONG," +
                                                                "tblTruongTHPT.MA_TINHTP=Temp.MA_TINHTP," +
                                                                "tblTruongTHPT.TEN_TINHTP=Temp.TEN_TINHTP," +
                                                                "tblTruongTHPT.MA_QH=Temp.MA_QH," +
                                                                "tblTruongTHPT.TEN_QH=Temp.TEN_QH," +
                                                                "tblTruongTHPT.TEN_TRUONG=Temp.TEN_TRUONG," +
                                                                "tblTruongTHPT.DIA_CHI=Temp.DIA_CHI," +
                                                                "tblTruongTHPT.KHU_VUC=Temp.KHU_VUC," +
                                                                "tblTruongTHPT.LOAI_TRUONG=Temp.LOAI_TRUONG" +
                                                             " FROM tblTruongTHPT " +
                                                             "INNER JOIN #TmpTable Temp ON ( tblTruongTHPT.ID = Temp.ID )" +
                                                             "DROP TABLE #TmpTable";
                                        command.ExecuteNonQuery();
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        return RedirectToAction("IndexTHPT");
                                    }
                                }
                                con.Close();
                            }
                        }
                    }
                }
                return RedirectToAction("IndexTHPT");
            }
            catch (Exception ex)
            {
                return RedirectToAction("IndexTHPT");
            }
        }
        public ActionResult DeleteTHPT(int id)
        {
            var Data = model.tblTruongTHPTs.Find(id);
            model.tblTruongTHPTs.Remove(Data);
            model.SaveChanges();
            return RedirectToAction("IndexTHPT");
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

                var newdata = data;
                newdata.MA_TPTRUONG = data.MA_TINHTP + data.MA_TRUONG;
                model.Entry(newdata).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("IndexTHPT", "ManageImporData");
            }
            return View();
        }
        public ActionResult IndexCNN()
        {
            var CNN = model.tblChungChiNNs.ToList();
            return View(CNN);
        }
        public ActionResult AddCNN()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddCNN(tblChungChiNN CNN)
        {
            model.tblChungChiNNs.Add(CNN);
            model.SaveChanges();
            return RedirectToAction("IndexCNN", "ManageImporData");
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
                        using (SqlCommand command = new SqlCommand("", con))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                sqlBulkCopy.DestinationTableName = "[dbo].[tblChungChiNN]";
                                sqlBulkCopy.ColumnMappings.Add("Mã Ngoại Ngữ", "MaNN");
                                sqlBulkCopy.ColumnMappings.Add("Chứng Chỉ", "ChungChi");
                                sqlBulkCopy.ColumnMappings.Add("Điểm Quy Đổi", "DiemQuiDoi");
                                con.Open();
                                bool check1 = false;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    string iDNN = dtExcel.Rows[i]["Mã Ngoại Ngữ"].ToString();
                                    string StringCC = dtExcel.Rows[i]["Chứng Chỉ"].ToString();
                                    var check = model.tblChungChiNNs.FirstOrDefault(t => t.MaNN.Equals(iDNN)&&t.ChungChi.Equals(StringCC));
                                    if (check != null)
                                    {
                                        check1 = true;
                                        break;
                                    }
                                    else
                                    {
                                        check1 = false;
                                        break;
                                    }
                                }
                                if (check1 == false)
                                {
                                    sqlBulkCopy.WriteToServer(dtExcel);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("Có dữ liệu trùng lặp bạn có muốn lưu không", "Dữ liệu đã tồn tại", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        command.CommandText = "CREATE TABLE #TmpTable([ID] [int] IDENTITY(1,1) NOT NULL," +
                                            "[MaNN] [varchar](10)  NULL," +
                                            "[ChungChi] [nvarchar](200) NULL," +
                                            "[DiemQuiDoi] [float] NULL)";
                                        //command.CommandTimeout = 300;
                                        command.ExecuteNonQuery();

                                        //Bulk insert into temp table

                                        /*sqlBulkCopy.BulkCopyTimeout = 660;*/
                                        sqlBulkCopy.DestinationTableName = "#TmpTable";
                                        sqlBulkCopy.WriteToServer(dtExcel);
                                        sqlBulkCopy.Close();
                                        command.CommandText = "UPDATE tblChungChiNN SET " +
                                                                "tblChungChiNN.DiemQuiDoi=Temp.DiemQuiDoi" +
                                                             " FROM tblChungChiNN " +
                                                             "INNER JOIN #TmpTable Temp ON ( tblChungChiNN.MaNN = Temp.MaNN AND tblChungChiNN.ChungChi=Temp.ChungChi)" +
                                                             "DROP TABLE #TmpTable";
                                        command.ExecuteNonQuery();
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        return RedirectToAction("IndexCNN");
                                    }
                                }
                                con.Close();

                            }
                        }
                    }
                }
                return RedirectToAction("IndexCNN");
            }
            catch (Exception ex)
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
        public ActionResult AddTP_QH_PX()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddTP_QH_PX(tblTP_QH_PX tP_QH_PX)
        {
            model.tblTP_QH_PX.Add(tP_QH_PX);
            model.SaveChanges();
            return RedirectToAction("IndexTP_QH_PX", "ManageImporData");
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
                        using (SqlCommand command = new SqlCommand("", con))
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
                                bool check1 = false;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    string IDTP = dtExcel.Rows[i]["Mã TP"].ToString();
                                    string IDQH = dtExcel.Rows[i]["Mã QH"].ToString();
                                    string IDPX = dtExcel.Rows[i]["Mã PX"].ToString();
                                    var check = model.tblTP_QH_PX.FirstOrDefault(t => t.MaTinhTP.Equals(IDTP) && t.MaQH.Equals(IDQH) && t.MaPX.Equals(IDPX));
                                    if (check != null)
                                    {
                                        check1 = true;
                                        break;
                                    }
                                    else
                                    {
                                        check1 = false;
                                        break;
                                    }
                                }
                                if (check1 == false)
                                {
                                    sqlBulkCopy.WriteToServer(dtExcel);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("Có dữ liệu trùng lặp bạn có muốn lưu không", "Dữ liệu đã tồn tại", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        command.CommandText = "CREATE TABLE #TmpTable([ID] [int] IDENTITY(1,1) NOT NULL," +
                                            "[TenTinhTP][nvarchar](255) NULL," +
                                            "[MaTinhTP] [nvarchar](255) NULL," +
                                            "[TenQH] [nvarchar](255) NULL," +
                                            "[MaQH] [nvarchar](255) NULL," +
                                            "[TenPX] [nvarchar](255) NULL," +
                                            "[MaPX] [nvarchar](255) NULL," +
                                            "[Cap] [nvarchar](255) NULL," +
                                            "[EnglishName] [nvarchar](255) NULL)";
                                        //command.CommandTimeout = 300;
                                        command.ExecuteNonQuery();

                                        //Bulk insert into temp table

                                        /*sqlBulkCopy.BulkCopyTimeout = 660;*/
                                        sqlBulkCopy.DestinationTableName = "#TmpTable";
                                        sqlBulkCopy.WriteToServer(dtExcel);
                                        sqlBulkCopy.Close();
                                        command.CommandText = "UPDATE tblTP_QH_PX SET " +
                                                                "tblTP_QH_PX.TenQH=Temp.TenQH," +
                                                                "tblTP_QH_PX.TenPX=Temp.TenPX" +
                                                             " FROM tblTP_QH_PX " +
                                                             "INNER JOIN #TmpTable Temp ON ( tblTP_QH_PX.MaTinhTP = Temp.MaTinhTP AND tblTP_QH_PX.MaQH=Temp.MaQH AND tblTP_QH_PX.MaPX=Temp.MaPX)" +
                                                             "DROP TABLE #TmpTable";
                                        command.ExecuteNonQuery();
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        return RedirectToAction("IndexTP_QH_PX");
                                    }
                                }
                                con.Close();

                            }
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