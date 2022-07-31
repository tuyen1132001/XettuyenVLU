using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Areas.Admin.Middleware;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    [LoginVerification]
    public class FormContentController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/FormContent
        public ActionResult Index()
        {
            //var Form = model.tblFormTuyenSinhs.Find(0);
            return View() ;
        }
        public ActionResult IndexTHPT()
        {
            var Form = model.tblFormTuyenSinhs.Find(0);
            return View(Form);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexTHPT(tblFormTuyenSinh form, string flexSwitchCheckDefault, string noidung) // form xét tuyển THPT Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(form.ID);
            string demo = flexSwitchCheckDefault;
            if(demo == null)
            {
                
                form.Open_Close =false;
                form.Edit_Open = save.Edit_Open;
                form.thongbao = noidung;
                form.Loai = "THPT";
                model.Entry(form).State = EntityState.Modified;
                model.SaveChanges();
            }
            else {
                form.Open_Close = true;
                form.Edit_Open = save.Edit_Open;
                form.thongbao = noidung;
                form.Loai = "THPT";
                model.Entry(form).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View("Index"); 
        }
        public ActionResult EditformTHPT()
        {
            var Form = model.tblFormTuyenSinhs.Find(0);
            return View(Form);
        }
        [HttpPost]
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
        public ActionResult IndexDGNL()
        {
            var Form = model.tblFormTuyenSinhs.Find(1);
            
            return View(Form);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexDGNL(tblFormTuyenSinh form, string flexSwitchCheckDefault, string noidung) // form xét tuyển DGNL Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(1);
            string demo = flexSwitchCheckDefault;
            if (demo == null)
            {
                save.Tieu_De = form.Tieu_De;
                save.Noi_Dung = form.Noi_Dung;
                save.Open_Close = false;
                save.NgayBatDau = form.NgayBatDau;
                save.NgayKetThuc = form.NgayKetThuc;
                save.thongbao = noidung;
                model.Entry(save).State = EntityState.Modified;
                model.SaveChanges();
            }
            else
            {
                save.Open_Close = true;
                save.NgayBatDau = form.NgayBatDau;
                save.NgayKetThuc = form.NgayKetThuc;
                save.thongbao = noidung;
                model.Entry(save).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View("Index");
        }
        public ActionResult EditformDGNL()
        {
            var Form = model.tblFormTuyenSinhs.Find(1);
            return View(Form);
        }
        [HttpPost]
        public ActionResult EditformDGNL(tblFormTuyenSinh form, string editopen) // form xét tuyển THPT Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(1);
            var Form = model.tblFormTuyenSinhs.Find(1);
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

    }
}