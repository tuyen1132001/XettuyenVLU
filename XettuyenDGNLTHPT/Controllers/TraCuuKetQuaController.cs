using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class TraCuuKetQuaController : Controller
    {
        // GET: TraCuuKetQua
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: ManageHoSo

        public ActionResult index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult index(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == CMND && !string.IsNullOrEmpty(u.DaNhanHoSo));
            if (Hoso != null)
            {

                return RedirectToAction("ketquaTHPT", Hoso);
            }
            else
            {
                var sdt = model.tblHoSoTHPTs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()) && !string.IsNullOrEmpty(u.DaNhanHoSo));
                if (sdt != null)
                {
                    return RedirectToAction("ketquaTHPT", sdt);
                }
                Session["notfound"] = true;
            }
            var HosoDGNL = model.tblHoSoDGNLs.FirstOrDefault(u => u.CMND.Equals(CMND.Trim()) && !string.IsNullOrEmpty(u.DaNhanHoSo));
            if (HosoDGNL != null)
            {

                return RedirectToAction("ketquaDGNL", HosoDGNL);
            }
            else
            {
                var sdt = model.tblHoSoDGNLs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()) && !string.IsNullOrEmpty(u.DaNhanHoSo));
                if (sdt != null)
                {

                    return RedirectToAction("ketquaDGNL", sdt);
                }
                Session["notfound"] = true;
            }
            return View();
        }


        public ActionResult ketquaTHPT(tblHoSoTHPT tHPT)
        {
            var form = model.tblFormTuyenSinhs.Find(0);
            if (form.Edit_Open == true)
            {
                Session["Form-button"] = true;
            }
            else
            {
                Session["Form-button"] = false;
            }

            return View(tHPT);
        }
      

        public ActionResult ketquaDGNL(tblHoSoDGNL dGNL)
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
            var data = dGNL;
            return View(data);
        }
      
    }
}