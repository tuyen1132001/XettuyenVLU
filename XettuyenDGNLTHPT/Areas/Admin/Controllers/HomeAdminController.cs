using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Areas.Admin.Middleware;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    [LoginVerification]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}