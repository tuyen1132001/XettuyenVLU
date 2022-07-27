using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class ManageContentMailController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/ManageContentMail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditMailTHPT()
        {
            var data = model.tblContentMails.Find(1);
            data.NOIDUNG = data.NOIDUNG.Replace("<br>", "\r\n");
            return View(data);
        }
        [HttpPost]
        public ActionResult EditMailTHPT(string noidung)
        {

            var data = new tblContentMail();
            data.NOIDUNG = noidung.Replace("\r\n", "<br>");
            data.LOAI = "THPT";
            data.ID = 1;
            model.Entry(data).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index", "ManageContentMail");
        }
        public ActionResult EditMailDGNL()
        {
            var data = model.tblContentMails.Find(2);
            data.NOIDUNG = data.NOIDUNG.Replace("<br>", "\r\n");
            return View(data);
        }
        [HttpPost]
        public ActionResult EditMailDGNL(string noidung)
        {
           
            var data = new tblContentMail();
            data.NOIDUNG = noidung.Replace("\r\n", "<br>");
            data.LOAI = "DGNL";
            data.ID = 2;
            model.Entry(data).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index", "ManageContentMail");
        }

    }
}