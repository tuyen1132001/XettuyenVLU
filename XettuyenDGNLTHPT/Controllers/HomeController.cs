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
            ViewBag.TinhTP = new SelectList(model.tblTinhTPs, "MA_TINHTP", "TEN_TINHTP");
            //ViewBag.CCNN = new SelectList(model.tblChungChiNNs, "ID", "MaNN - ChungChi - DiemQuiDoi");
            
            return View(HoSoTHPT);
        }
        [HttpPost]
        public ActionResult Index(tblHoSoTHPT tblHoSoTHPT) //Form Dang ky THTP QG
        {
            if (ModelState.IsValid)
            {
                model.tblHoSoTHPTs.Add(tblHoSoTHPT);
                model.SaveChanges();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}