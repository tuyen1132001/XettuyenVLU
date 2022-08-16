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
    public class HomeAdminController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            var dsthpt = model.tblHoSoTHPTs.ToList();
            var dsdgnl = model.tblHoSoDGNLs.ToList();
            Session["slhs"] = dsthpt.Count + dsdgnl.Count;
            return View();
        }
        public ActionResult ManageAcount()
        {
            var list = model.Accounts.ToList();
            return View(list);
        }
        public ActionResult IndexData()
        {
           
            return View();
        }
    }
}