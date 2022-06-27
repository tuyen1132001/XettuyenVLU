using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class ManageHoSoController : Controller
    {
        XettuyenVLUEntities model = new XettuyenVLUEntities();
        // GET: ManageHoSo
        public ActionResult InHoSo()
        {
            
            return View();
        }


        [HttpPost]
       
        public ActionResult InHoSo(string txtCMND)
        {
            var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == txtCMND);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Detail");
            }
            return View("Detail");
        }
     
        public ActionResult Detail()
        {
            return View("Detail");
        }
    }
}