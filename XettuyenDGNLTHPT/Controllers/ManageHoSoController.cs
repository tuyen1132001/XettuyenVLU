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
        demo2Entities1 model = new demo2Entities1();
        // GET: ManageHoSo
        
        public ActionResult InHoSo()
        {
            
            return View();
        }
      
        [HttpPost]
        [AllowAnonymous]
       
        public ActionResult DetailTHPT( string CMND)
        {
   
            var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == CMND);
            if (Hoso != null)
            {

                return View("DetailTHPT",Hoso);
            }
            else
            {
                var sdt = model.tblHoSoTHPTs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                if (sdt != null)
                {
                    return View("DetailTHPT", sdt);
                }
                Session["notfound"] = true;
            }
            return View("InHoSo");
        }
        [HttpPost]
        public ActionResult DetailDGNL(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            string dot = DropDownList1;
            string loai = ddlLoaiXetTuyen;
            var HosoDGNL = model.tblHoSoDGNLs.FirstOrDefault(u => u.CMND.Equals(CMND.Trim()));
            if (HosoDGNL != null)
            {
                return View(HosoDGNL);
            }
            else
            {
                var sdt = model.tblHoSoDGNLs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                if(sdt != null)
                {
                    return View(sdt);
                }
                return DetailTHPT(CMND);
            }
        }
    }
}