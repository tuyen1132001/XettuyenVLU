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
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: ManageHoSo
        
        public ActionResult InHoSo()
        {
            
            return View();
        }
      
        [HttpPost]
        [AllowAnonymous]
       
        public ActionResult DetailTHPT( string CMND)
        {
            var form = model.tblFormTuyenSinhs.Find(0);
            if(form.Edit_Open == true)
            {
                Session["Form-button"] = true;
            }
            else
            {
                Session["Form-button"] = false;
            }
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
        public ActionResult PrintTHPT(tblHoSoTHPT tHPT)
        {
            return View(tHPT);
        }
        [HttpPost]
        public ActionResult DetailDGNL(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            var form = model.tblFormTuyenSinhs.Find(1);
            if (form.Edit_Open == true)
            {
                Session["Form-button"] = true;
            }
            else
            {
                Session["Form-button"] = false;
            }
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
        public ActionResult PrintDGNL(tblHoSoDGNL dgnl)
        {
            return View(dgnl);
        }
    }
}