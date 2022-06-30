using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class HomeController : Controller
    {
        XettuyenVLUEntities model = new XettuyenVLUEntities();
        public ActionResult Index() //Form Dang ky THTP QG
        {
            var HoSoTHPT = new tblHoSoTHPT();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.DanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.TonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");
            ViewBag.TinhTP = new SelectList(model.tblTinhTPs, "MA_TINHTP", "TEN_TINHTP"); ;
            
            
            //ViewBag.CCNN = new SelectList(model.tblChungChiNNs, "ID", "MaNN"+"ChungChi"+"DiemQuiDoi");
            var CCNN = model.tblChungChiNNs.ToList();
            List<string> ChungChi = new List<string>();
            ChungChi.Add("-- Chọn chứng chỉ ngoại ngữ --");
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
            THPT.Insert(0, new  { MA_TINHTP = "-1", TEN_TINHTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.THPT = new SelectList(THPT, "MA_TINHTP", "TEN_TINHTP");

            var Majors = model.tblNganhs.Select(e => new { e.MA_NGANH, e.TEN_NGANH }).Distinct().ToList();
            Majors.Insert(0, new { MA_NGANH = "-1", TEN_NGANH = "--------------Chọn-------------- " });
            ViewBag.NGANH = new SelectList(Majors, "MA_NGANH", "TEN_NGANH");



            return View(HoSoTHPT);
        }

        [HttpPost]
        public ActionResult Index(tblHoSoTHPT tblHoSoTHPT , string TP_QH_PX) //Form Dang ky THTP QG
        {
            if (ModelState.IsValid)
            {
                //string id = form["TP_QH_PX"].ToString();
                 
               

                RedirectToAction("DEtail");
            }
            return View();
        }

        public ActionResult Detail()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
                .Where(e=> e.MA_QH == idQH && e.MA_TINHTP == idTP)
                .Select(e => new { Id = e.MA_TRUONG, Name = e.TEN_TRUONG })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetArea(string id)
        {
            var data = model.tblTruongTHPTs
                .Where(e => e.MA_TRUONG == id)
                .Select(e => new {  Name = e.KHU_VUC })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMajors(string id)// tim to hop theo nganh
        {
            var data = model.tblNganhs
                .Where(e => e.MA_NGANH == id)
                .Select(e => new { Id= e.MA_TOHOP,Name = e.MA_TOHOP+"-"+e.TEN_TOHOP })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}