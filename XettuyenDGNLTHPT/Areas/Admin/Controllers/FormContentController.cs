using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class FormContentController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/FormContent
        public ActionResult Index()
        {
            var Form = model.tblFormTuyenSinhs.Find(0);
            return View(Form) ;
        }
        [HttpPost]
        public ActionResult Index(tblFormTuyenSinh form, string flexSwitchCheckDefault) // form xét tuyển THPT Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(form.ID);
            string demo = flexSwitchCheckDefault;
            if(demo == null)
            {
                
                form.Open_Close =false;
                form.Edit_Open = save.Edit_Open;
                form.Loai = "THPT";
                model.Entry(form).State = EntityState.Modified;
                model.SaveChanges();
            }
            else {
                form.Open_Close = true;
                form.Edit_Open = save.Edit_Open;
                form.Loai = "THPT";
                model.Entry(form).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View(); 
        }
     
        public ActionResult EditformTHPT(tblFormTuyenSinh form, string editopen) // form xét tuyển THPT Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(0);
            var Form = model.tblFormTuyenSinhs.Find(0);
            string demo = editopen;
            if (demo == null)
            {


                Form.Edit_Open = false;
                Form.NgayBatDau = form.NgayBatDau;
                Form.NgayKetThuc = form.NgayKetThuc;
                model.Entry(Form).State = EntityState.Modified;
                model.SaveChanges();
            }
            else
            {
       
                Form.Edit_Open = true;
                Form.NgayBatDau = form.NgayBatDau;
                Form.NgayKetThuc = form.NgayKetThuc;
                model.Entry(Form).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult IndexDGNL(tblFormTuyenSinh form, string flexSwitchCheckDefault) // form xét tuyển THPT Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(form.ID);
            string demo = flexSwitchCheckDefault;
            if (demo == null)
            {

                form.Open_Close = false;
                form.Edit_Open = save.Edit_Open;
                form.Loai = "THPT";
                model.Entry(form).State = EntityState.Modified;
                model.SaveChanges();
            }
            else
            {
                form.Open_Close = true;
                form.Edit_Open = save.Edit_Open;
                form.Loai = "THPT";
                model.Entry(form).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View();
        }

    }
}