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
            return View();
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