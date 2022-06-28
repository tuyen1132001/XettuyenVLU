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
      

       
        public ActionResult Detail(string cmnd)
        {
            cmnd = 4668839 + "";
            var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == cmnd);
            if (Hoso != null)
            {

                return View(Hoso);
            }
            return View("InHoSo");
        }
    }
}