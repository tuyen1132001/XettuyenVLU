using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class FormContentController : Controller
    {
        // GET: Admin/FormContent
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string year, string dot, string TGStart, string TGEnd, string flexSwitchCheckDefault)
        {
            Session["Form-year"] = year;
            Session["Form-dot"] = dot;
            

            return View(); 
        }
    }
}