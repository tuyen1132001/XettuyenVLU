using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class ManageHoSoController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: ManageHoSo

        public ActionResult InHoSo()
        {

            return View();
        }
        [HttpPost]
        public ActionResult InHoSo(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == CMND);
            if (Hoso != null)
            {

                return RedirectToAction("DetailTHPT", Hoso);
            }
            else
            {
                var sdt = model.tblHoSoTHPTs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                if (sdt != null)
                {
                    return RedirectToAction("DetailTHPT", sdt);
                }
                Session["notfound"] = true;
            }
            var HosoDGNL = model.tblHoSoDGNLs.FirstOrDefault(u => u.CMND.Equals(CMND.Trim()));
            if (HosoDGNL != null)
            {
                
                return RedirectToAction("DetailDGNL", HosoDGNL);
            }
            else
            {
                var sdt = model.tblHoSoDGNLs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                if (sdt != null)
                {

                    return RedirectToAction("DetailDGNL", sdt);
                }
                Session["notfound"] = true;
            }
            return View();
        }
    

        public ActionResult DetailTHPT(tblHoSoTHPT tHPT)
        {
            var form = model.tblFormTuyenSinhs.Find(0);
            if (form.Edit_Open == true)
            {
                Session["Form-button"] = true;
            }
            else
            {
                Session["Form-button"] = false;
            }
       
            return View(tHPT);
        }
        public ActionResult PrintTHPT(tblHoSoTHPT tHPT)
        {
            return View(tHPT);
        }
        
        public ActionResult DetailDGNL(tblHoSoDGNL dGNL)
        {
            var form = model.tblFormTuyenSinhs.Find(1);
            if (form.Edit_Open == true)
            {
                Session["Form-button"] = true;
            }
            else
            {
                Session["Form-button"] = false;
            }
            var data = dGNL;
            return View(data);
        }
        public ActionResult PrintDGNL(tblHoSoDGNL dgnl)
        {
            return View(dgnl);
        }
        public ActionResult EditTHPT(tblHoSoTHPT thpt)
        {
            tblHoSoTHPT tblHoSoTHPT = new tblHoSoTHPT();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.MaDanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.MaTonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");

            var CCNN = model.tblChungChiNNs.ToList();
            List<string> ChungChi = new List<string>();

            foreach (var cc in CCNN)
            {
                ChungChi.Add(cc.MaNN + "-" + cc.ChungChi + "-" + cc.DiemQuiDoi);
            }
            ViewBag.CCNN = new SelectList(ChungChi);
            //
            var TP_QH_PX = model.tblTP_QH_PX.Select(e => new { e.MaTinhTP, e.TenTinhTP }).Distinct().ToList();
            TP_QH_PX.Insert(0, new { MaTinhTP = "-1", TenTinhTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.TP_QH_PX = new SelectList(TP_QH_PX, "MaTinhTP", "TenTinhTP");



            var THPT = model.tblTruongTHPTs.Select(e => new { e.MA_TINHTP, e.TEN_TINHTP }).Distinct().ToList();
            THPT.Insert(0, new { MA_TINHTP = "-1", TEN_TINHTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.THPT = new SelectList(THPT, "MA_TINHTP", "TEN_TINHTP");

            var Majors = model.tblNganhs.Select(e => new { e.MA_NGANH, e.TEN_NGANH }).Distinct().ToList();
            Majors.Insert(0, new { MA_NGANH = "-1", TEN_NGANH = "--------------Chọn-------------- " });
            ViewBag.NGANH = new SelectList(Majors, "MA_NGANH", "TEN_NGANH");

            // Form tuyển sinh
            var form = model.tblFormTuyenSinhs.Find(0);
            Session["Form-Title"] = form.Tieu_De;
            Session["Form-Content"] = form.Noi_Dung;
            Session["Form-bodyTHPT"] = form.Open_Close;

            var formDGNL = model.tblFormTuyenSinhs.Find(1);
            Session["Form-TitleDGNl"] = formDGNL.Tieu_De;
            Session["Form-ContentDGNL"] = formDGNL.Noi_Dung;
            Session["Form-bodyDGNL"] = formDGNL.Open_Close;

            return View(thpt);
        }
        public ActionResult EditDGNL(tblHoSoDGNL dGNL)
        {
            tblHoSoDGNL tblHoSoDGNL = new tblHoSoDGNL();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.MaDanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.MaTonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");

            var CCNN = model.tblChungChiNNs.ToList();
            List<string> ChungChi = new List<string>();

            foreach (var cc in CCNN)
            {
                ChungChi.Add(cc.MaNN + "-" + cc.ChungChi + "-" + cc.DiemQuiDoi);
            }
            ViewBag.CCNN = new SelectList(ChungChi);
            //
           
            var TP_QH_PX = model.tblTP_QH_PX.Select(e => new { e.MaTinhTP, e.TenTinhTP }).Distinct().ToList();
            TP_QH_PX.Insert(0, new { MaTinhTP = "-1", TenTinhTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.TP_QH_PX = new SelectList(TP_QH_PX, "MaTinhTP", "TenTinhTP");
            
            var TP_QH = model.tblTP_QH_PX.Where(u =>u.MaTinhTP== dGNL.HoKhau_MaTinhTP).Select(e => new { e.MaQH, e.TenQH }).Distinct().ToList();
            TP_QH.Insert(0, new { MaQH = "-1", TenQH = "-- Chọn Quận Huyện--" });
            ViewBag.TP_QH = new SelectList(TP_QH, "MaQH", "TenQH");

            var PX= model.tblTP_QH_PX.Where(u => u.MaQH == dGNL.HoKhau_MaQH).Select(e => new { e.MaPX, e.TenPX }).Distinct().ToList();
            PX.Insert(0, new { MaPX = "-1", TenPX = "-- Chọn Phường Xã --" });
            ViewBag.PX = new SelectList(PX, "MaPX", "TenPX");
            //
            var LienHe_QH = model.tblTP_QH_PX.Where(u => u.MaTinhTP == dGNL.LienLac_MaTP).Select(e => new { e.MaQH, e.TenQH }).Distinct().ToList();
            LienHe_QH.Insert(0, new { MaQH = "-1", TenQH = "-- Chọn Quận Huyện--" });
            ViewBag.LienHe_QH = new SelectList(LienHe_QH, "MaQH", "TenQH");

            var LienHe_PX = model.tblTP_QH_PX.Where(u => u.MaQH == dGNL.LienLac_MaQH).Select(e => new { e.MaPX, e.TenPX }).Distinct().ToList();
            LienHe_PX.Insert(0, new { MaPX = "-1", TenPX = "-- Chọn Phường Xã --" });
            ViewBag.LienHe_PX = new SelectList(LienHe_PX, "MaPX", "TenPX");
            //
            var THPT = model.tblTruongTHPTs.Select(e => new { e.MA_TINHTP, e.TEN_TINHTP }).Distinct().ToList();
            THPT.Insert(0, new { MA_TINHTP = "-1", TEN_TINHTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.THPT = new SelectList(THPT, "MA_TINHTP", "TEN_TINHTP");


            var Majors = model.tblNganhs.Select(e => new { e.MA_NGANH, e.TEN_NGANH }).Distinct().ToList();
            Majors.Insert(0, new { MA_NGANH = "-1", TEN_NGANH = "--------------Chọn-------------- " });
            ViewBag.NGANH = new SelectList(Majors, "MA_NGANH", "TEN_NGANH");

            // Form tuyển sinh
            var form = model.tblFormTuyenSinhs.Find(0);
            Session["Form-Title"] = form.Tieu_De;
            Session["Form-Content"] = form.Noi_Dung;
            Session["Form-bodyTHPT"] = form.Open_Close;

            var formDGNL = model.tblFormTuyenSinhs.Find(1);
            Session["Form-TitleDGNl"] = formDGNL.Tieu_De;
            Session["Form-ContentDGNL"] = formDGNL.Noi_Dung;
            Session["Form-bodyDGNL"] = formDGNL.Open_Close;
            var data = dGNL;

            return View(data);
        }
        
        public ActionResult SaveEditDGNL(tblHoSoDGNL dGNL, string NgaySinh, string txtTenThiSinh)
        {
            var data = model.tblHoSoDGNLs.Find(dGNL.ID);
            model.Entry(dGNL).State = EntityState.Modified;
            model.SaveChanges();
            return View("InHoSo");
        }
        public ActionResult SaveTHPT(tblHoSoTHPT thpt)
        {
            model.tblHoSoTHPTs.Add(thpt);
            model.SaveChanges();

            return View(thpt);
        }
        public ActionResult SaveDGNL(tblHoSoDGNL dGNL)
        {
            model.tblHoSoDGNLs.Add(dGNL);
            model.SaveChanges();

            return View(dGNL);
        }

        public JsonResult GetDistrict(string id) // lấy quận huyện
        {
            var data = model.tblTP_QH_PX
                                .Where(e => e.MaTinhTP == id)
                                .Select(e => new { Id = e.MaQH, Name = e.TenQH })
                                .Distinct()
                                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWard(string id) // lấy phường xã
        {
            var data = model.tblTP_QH_PX
                                .Where(e => e.MaQH == id)
                                .Select(e => new { Id = e.MaPX, Name = e.TenPX })
                                .Distinct()
                                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistrictSchool(string id)
        {
            var data = model.tblTruongTHPTs
                                .Where(e => e.MA_TINHTP == id)
                                .Select(e => new { Id = e.MA_QH, Name = e.TEN_QH })
                                .Distinct()
                                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSchool(string id)
        {
            var idQH = id.Split('-')[0];
            var idTP = id.Split('-')[1];
            var data = model.tblTruongTHPTs
                .Where(e => e.MA_QH == idQH && e.MA_TINHTP == idTP)
                .Select(e => new { Id = e.MA_TRUONG, Name = e.TEN_TRUONG })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetArea(string id)
        {
            var data = model.tblTruongTHPTs
                .Where(e => e.MA_TRUONG == id)
                .Select(e => new { Id = e.KHU_VUC, Name = e.KHU_VUC })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMajors(string id)// tim to hop theo nganh
        {
            var data = model.tblNganhs
                .Where(e => e.MA_NGANH == id)
                .Select(e => new { Id = e.MA_TOHOP, Name = e.MA_TOHOP + "-" + e.TEN_TOHOP })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}