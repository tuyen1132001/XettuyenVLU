using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class ManageDanhMucController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/ManageDanhMuc
        public ActionResult Index()
        {
            var listDanhMuc = model.DanhMucs.ToList();
            return View(listDanhMuc);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DanhMuc danhMuc)
        {
            DanhMuc dm1 = model.DanhMucs.Find(danhMuc.ID);
            if (dm1 != null)
            {
                return Content("Error");
            }
            else
            {
                model.DanhMucs.Add(danhMuc);
                model.SaveChanges();
                return RedirectToAction("index");
            }
          
        }
        public ActionResult Update(int ID)
        {
            DanhMuc dm1 = model.DanhMucs.Find(ID);
            return View(dm1);
           

        }
        [HttpPost]
        public ActionResult Update(DanhMuc danhMuc)
        {
            model.Entry(danhMuc).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("index");

        }
        public ActionResult DeleteDanhMuc(int ID)
        {
            var delete = model.DanhMucs.Find(ID);
            model.DanhMucs.Remove(delete);
            model.SaveChanges();
            return RedirectToAction("index", "ManageDanhMuc");
        }
    }
}