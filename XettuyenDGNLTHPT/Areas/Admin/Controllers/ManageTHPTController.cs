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
    public class ManageTHPTController : Controller
    {
        
        // GET: Admin/ManageTHPT
        SEP25Team08Entities model = new SEP25Team08Entities();
        public ActionResult Index()
        {
            var list = model.tblHoSoTHPTs.ToList();
            return View(list);
        }

        // GET: Admin/ManageTHPT/Details/5
        public ActionResult DetailsHoSoTHPT(int id)
        {
            var dthsTHPT = model.tblHoSoTHPTs.Find(id);
            return View(dthsTHPT);
        }

        public ActionResult NhanHoSo(int id)
        {
            var nhanhosoTHPT = model.tblHoSoTHPTs.Where(h => h.ID == id).FirstOrDefault();
            model.Entry(nhanhosoTHPT).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("DetailsHoSoTHPT", new { id = id });
        }

        // GET: Admin/ManageTHPT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageTHPT/Create
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

        // GET: Admin/ManageTHPT/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/ManageTHPT/Edit/5
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

        // GET: Admin/ManageTHPT/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ManageTHPT/Delete/5
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
