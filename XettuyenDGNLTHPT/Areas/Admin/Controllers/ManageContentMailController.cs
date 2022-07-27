using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class ManageContentMailController : Controller
    {
        // GET: Admin/ManageContentMail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditMailTHPT()
        {
            return View();
        }
        public ActionResult EditMailDGNL()
        {
            return View();
        }

    }
}