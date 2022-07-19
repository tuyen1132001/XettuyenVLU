using System;
using System.Collections.Generic;
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
