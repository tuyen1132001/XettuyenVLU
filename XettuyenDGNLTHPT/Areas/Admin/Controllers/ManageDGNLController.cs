using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Areas.Admin.Middleware;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    [LoginVerification]
    public class ManageDGNLController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/ManageDGNL
        public ActionResult Index()
        {
            var list = model.tblHoSoDGNLs.ToList();
            return View(list);
        }

        // GET: Admin/ManageDGNL/Details/5
        public ActionResult DetailsHoSoDGNL(int id)
        {
            var dthsDGNL = model.tblHoSoDGNLs.Find(id);
            return View(dthsDGNL);
        }
        public JsonResult NhanHoSo(string id, string vlue)
        {
            {
                int sid = Int32.Parse(id.Trim());
                var nhanhosoDGNL = model.tblHoSoDGNLs.Where(h => h.ID == sid).FirstOrDefault();
                if (string.IsNullOrEmpty(vlue))
                {
                    nhanhosoDGNL.DaNhanHoSo = null;
                    model.Entry(nhanhosoDGNL).State = EntityState.Modified;
                    model.SaveChanges();
                }
                else
                {
                    nhanhosoDGNL.DaNhanHoSo = "N";
                    model.Entry(nhanhosoDGNL).State = EntityState.Modified;
                    model.SaveChanges();
                }
                return Json("Success");
            }
        }
        public ActionResult ExportData()
        {
            var list = model.tblHoSoDGNLs.ToList();
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Mã hồ sơ";
            Sheet.Cells["B1"].Value = "Họ và tên";
            Sheet.Cells["C1"].Value = "Email";
            Sheet.Cells["D1"].Value = "Giới tính";
            Sheet.Cells["E1"].Value = "Ngày sinh";
            Sheet.Cells["F1"].Value = "Mã nơi sinh";
            Sheet.Cells["G1"].Value = "Tên nơi sinh";
            Sheet.Cells["H1"].Value = "Mã dân tôc";
            Sheet.Cells["I1"].Value = "Tên dân tộc";
            Sheet.Cells["J1"].Value = "Mã tôn giáo";
            Sheet.Cells["K1"].Value = "Tên tôn giáo";
            Sheet.Cells["L1"].Value = "CMND";
            Sheet.Cells["M1"].Value = "Quốc tịch";
            Sheet.Cells["N1"].Value = "Hộ Khẩu";
            Sheet.Cells["O1"].Value = "Hộ khẩu Mã phường";
            Sheet.Cells["P1"].Value = "Hộ khẩu Tên phường";
            Sheet.Cells["Q1"].Value = "Hộ khẩu Mã tỉnh TP";
            Sheet.Cells["R1"].Value = "Hộ khẩu Tên tỉnh TP";
            Sheet.Cells["S1"].Value = "Hộ khẩu Mã quận huyện";
            Sheet.Cells["T1"].Value = "Hộ khẩu Tên quận huyện";
            Sheet.Cells["U1"].Value = "Năm tốt nghiệp";
            Sheet.Cells["V1"].Value = "Số báo danh";
            Sheet.Cells["W1"].Value = "Hạnh kiểm lớp 12";
            Sheet.Cells["X1"].Value = "Học lực lớp 12";
            Sheet.Cells["Y1"].Value = "Loại hình TN";
            Sheet.Cells["Z1"].Value = "Trường THPT Mã Tỉnh";
            Sheet.Cells["AA1"].Value = "Trường THPT Tên Tỉnh";
            Sheet.Cells["AB1"].Value = "Trường THPT Mã Quận/Huyện";
            Sheet.Cells["AC1"].Value = "Trường THPT Tên Quận/Huyện";
            Sheet.Cells["AD1"].Value = "Tên Trường THPT";
            Sheet.Cells["AE1"].Value = "Mã Trường THPT";
            Sheet.Cells["AF1"].Value = "Tên lớp 12";
            Sheet.Cells["AG1"].Value = "Khu vực";
            Sheet.Cells["AH1"].Value = "Điểm ĐGNL";
            Sheet.Cells["AI1"].Value = "Thời gian thi DGNL";
            Sheet.Cells["AJ1"].Value = "Đối tượng ưu tiên";
            Sheet.Cells["AK1"].Value = "CCNN";
            Sheet.Cells["AL1"].Value = "Mã ngành_tổ hợp 1";
            Sheet.Cells["AM1"].Value = "Tên ngành_tổ hợp 1";
            Sheet.Cells["AN1"].Value = "CTDT 1";
            Sheet.Cells["AO1"].Value = "Mã ngành_tổ hợp 2";
            Sheet.Cells["AP1"].Value = "Tên ngành_tổ hợp 2";
            Sheet.Cells["AQ1"].Value = "CTDT 2 ";
            Sheet.Cells["AR1"].Value = "Mã ngành_tổ hợp 3";
            Sheet.Cells["AS1"].Value = "Tên ngành_tổ hợp 3";
            Sheet.Cells["AT1"].Value = "CTDT 3";
            Sheet.Cells["AU1"].Value = "Liên lạc địa chỉ";
            Sheet.Cells["AV1"].Value = "Liên lạc mã phường";
            Sheet.Cells["AW1"].Value = "Liên lạc tên phường";
            Sheet.Cells["AX1"].Value = "Liên lạc mã tỉnh tp";
            Sheet.Cells["AY1"].Value = "Liên lạc tên tỉnh tp";
            Sheet.Cells["AZ1"].Value = "liên lạc mã quận/huyện";
            Sheet.Cells["BA1"].Value = "Liên lạc tên quận/huyện";
            Sheet.Cells["BB1"].Value = "Điện thoại di động";
            Sheet.Cells["BC1"].Value = "Điện thoại phụ huynh";
            Sheet.Cells["BD1"].Value = "Data Inserted";
            Sheet.Cells["BE1"].Value = "Data Edited";
            Sheet.Cells["BF1"].Value = "Đã Nhận Hồ sơ";
            int row = 2;// dòng bắt đầu ghi dữ liệu
            foreach (var item in list)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.ID;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.HoVaTen;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Email;
                if (item.GioiTinh == true)
                {
                    Sheet.Cells[string.Format("D{0}", row)].Value = "Nam";
                } else if (item.GioiTinh == false)
                {
                    Sheet.Cells[string.Format("D{0}", row)].Value = "Nữ";
                }
                else
                {
                    Sheet.Cells[string.Format("D{0}", row)].Value = "";
                }
                Sheet.Cells[string.Format("E{0}", row)].Value = Convert.ToDateTime(item.NgaySinh).ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("F{0}", row)].Value = item.MaNoiSinh;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.TenNoiSinh;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.MaDanToc;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.TenDanToc;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.MaTonGiao;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.TenTonGiao;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.CMND;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.QuocTich;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.HoKhau;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.HoKhau_MaPhuong;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.HoKhau_TenPhuong;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.HoKhau_MaTinhTP;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.HoKhau_TenTinhTP;
                Sheet.Cells[string.Format("S{0}", row)].Value = item.HoKhau_MaQH;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.HoKhau_TenQH;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.NamTotNghiep;
                Sheet.Cells[string.Format("V{0}", row)].Value = item.SoBaoDanh;
                Sheet.Cells[string.Format("W{0}", row)].Value = item.HocLucLop12;
                Sheet.Cells[string.Format("X{0}", row)].Value = item.HanhKiemLop12;
                Sheet.Cells[string.Format("Y{0}", row)].Value = item.LoaiHinhTN;
                Sheet.Cells[string.Format("Z{0}", row)].Value = item.TruongTHPT_MaTinhTP;
                Sheet.Cells[string.Format("AA{0}", row)].Value = item.TruongTHPT_TenTinhTP;
                Sheet.Cells[string.Format("AB{0}", row)].Value = item.TruongTHPT_MaQH;
                Sheet.Cells[string.Format("AC{0}", row)].Value = item.TruongTHPT_TenQH;
                Sheet.Cells[string.Format("AD{0}", row)].Value = item.TenTruongTHPT;
                Sheet.Cells[string.Format("AE{0}", row)].Value = item.MaTruongTHPT;
                Sheet.Cells[string.Format("AF{0}", row)].Value = item.TenLop12;
                Sheet.Cells[string.Format("AG{0}", row)].Value = item.KhuVuc;
                Sheet.Cells[string.Format("AH{0}", row)].Value = item.DiemDGNL;
                Sheet.Cells[string.Format("AI{0}", row)].Value = Convert.ToDateTime(item.TGThiDGNL).ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("AJ{0}", row)].Value = item.DoiTuongUuTien;
                Sheet.Cells[string.Format("AK{0}", row)].Value = item.CCNN;
                Sheet.Cells[string.Format("AL{0}", row)].Value = item.MaNganh_ToHop1;
                Sheet.Cells[string.Format("AM{0}", row)].Value = item.TenNganh_TenToHop1;
                Sheet.Cells[string.Format("AN{0}", row)].Value = item.CTDT1;
                Sheet.Cells[string.Format("AO{0}", row)].Value = item.MaNganh_ToHop2;
                Sheet.Cells[string.Format("AP{0}", row)].Value = item.TenNganh_TenToHop2;
                Sheet.Cells[string.Format("AQ{0}", row)].Value = item.CTDT2;
                Sheet.Cells[string.Format("AR{0}", row)].Value = item.MaNganh_ToHop3;
                Sheet.Cells[string.Format("AS{0}", row)].Value = item.TenNganh_TenToHop3;
                Sheet.Cells[string.Format("AT{0}", row)].Value = item.CTDT3;
                Sheet.Cells[string.Format("AU{0}", row)].Value = item.LienLac_DiaChi;
                Sheet.Cells[string.Format("AV{0}", row)].Value = item.LienLac_MaPhuongXa;
                Sheet.Cells[string.Format("AW{0}", row)].Value = item.LienLac_TenPhuongXa;
                Sheet.Cells[string.Format("AX{0}", row)].Value = item.LienLac_MaTP;
                Sheet.Cells[string.Format("AY{0}", row)].Value = item.LienLac_TenTP;
                Sheet.Cells[string.Format("AZ{0}", row)].Value = item.LienLac_MaQH;
                Sheet.Cells[string.Format("BA{0}", row)].Value = item.LienLac_TenQH;
                Sheet.Cells[string.Format("BB{0}", row)].Value = item.DienThoaiDD;
                Sheet.Cells[string.Format("BC{0}", row)].Value = item.DienThoaiPhuHuynh;
                Sheet.Cells[string.Format("BD{0}", row)].Value = Convert.ToDateTime(item.DateInserted).ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("BE{0}", row)].Value = Convert.ToDateTime(item.DateEdited).ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("BF{0}", row)].Value = item.DaNhanHoSo;
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "Report.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
            return View();
        }
        // GET: Admin/ManageDGNL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageDGNL/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ManageDGNL/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/ManageDGNL/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ManageDGNL/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ManageDGNL/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
