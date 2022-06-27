using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public JsonResult GetAllTinhTPs()
        {
            using(var db = new MyDbContext())
            {
                var data = db.TinhTPs.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllTP_QH_PXes()
        {
            using (var db = new MyDbContext())
            {

                //var data = db.TP_QH_PXes.Where(x=>x.MA_TINHTP==MA_TINHTP).ToList();
                //var data1 = db.TP_QH_PXes.Where(x => x.MaQH == MaQH).ToList();
                //var data2 = db.TP_QH_PXes.Where(x => x.MaPX == MaPX).ToList();
                var data = db.TP_QH_PXes.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }
}