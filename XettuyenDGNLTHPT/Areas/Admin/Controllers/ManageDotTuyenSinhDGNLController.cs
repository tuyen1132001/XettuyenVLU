using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class ManageDotTuyenSinhDGNLController : Controller
    {
        // GET: Admin/ManageDotTuyenSinhDGNL
        SEP25Team08Entities model = new SEP25Team08Entities();
        public ActionResult Index()
        {
            var data = model.tblDotTuyenSinhDGNLs.ToList();
            return View(data);
        }
        public ActionResult AddDotDGNL()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddDotDGNL(tblDotTuyenSinhDGNL dGNL)
        {
            dGNL.Loai = "DGNL";
            model.tblDotTuyenSinhDGNLs.Add(dGNL);
            model.SaveChanges();
            return RedirectToAction("Index", "ManageDotTuyenSinhDGNL");
            
        }
        public ActionResult EditDotDGNL(int id)
        {
            var thongtin = model.tblDotTuyenSinhDGNLs.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(thongtin);
        }
        [HttpPost]
        public ActionResult EditDotDGNL(tblDotTuyenSinhDGNL dGNL)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
        public ActionResult DeleteĐotGNL(int id)
        {
            var xoa = model.tblDotTuyenSinhDGNLs.Find(id);
            model.tblDotTuyenSinhDGNLs.Remove(xoa);
            model.SaveChanges();
            return RedirectToAction("Index", "ManageDotTuyenSinhDGNL");
        }
    }
}