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
            return View();
        }
        public ActionResult IndexTHPT()
        {
            var Form = model.tblFormTuyenSinhs.Find(0);

            
            return View(Form);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexTHPT(tblFormTuyenSinh form, string flexSwitchCheckDefault) // form xét tuyển THPT Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            var save = db.tblFormTuyenSinhs.Find(form.ID);
            string demo = flexSwitchCheckDefault;
            if (ModelState.IsValid)
            {
                if (form.NgayBatDau <= form.NgayKetThuc)
                {
                    if (demo == null)
                    {

                        form.Open_Close = false;
                        form.Edit_Open = save.Edit_Open;
                        
                        form.Loai = "THPT";
                        model.Entry(form).State = EntityState.Modified;
                        model.SaveChanges();
                        return RedirectToAction("Index", "FormContent");
                    }
                    else
                    {
                        form.Open_Close = true;
                        form.Edit_Open = save.Edit_Open;
                        
                        form.Loai = "THPT";
                        model.Entry(form).State = EntityState.Modified;
                        model.SaveChanges();
                        return RedirectToAction("Index", "FormContent");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage =  "Nhập ngày không hợp lệ";
                }
            }
            return View();
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
                Form.NgayBatDauEdit = form.NgayBatDauEdit;
                Form.NgayKetThucEdit = form.NgayKetThucEdit;
                model.Entry(Form).State = EntityState.Modified;
                model.SaveChanges();
            }
            else
            {

                Form.Edit_Open = true;
                Form.NgayBatDauEdit = form.NgayBatDauEdit;
                Form.NgayKetThucEdit = form.NgayKetThucEdit;
                model.Entry(Form).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View("Index");
        }
        public ActionResult IndexDGNL()
        {
            var Form = model.tblFormTuyenSinhs.Find(1);
            var Dot = model.tblDotTuyenSinhDGNLs.ToList();
            List<string> CacDot = new List<string>();
            CacDot.Add("---- Chọn Đợt----");
            foreach (var dot in Dot)
            {
                CacDot.Add("Đợt " + dot.Dot);
            }
            ViewBag.Dot = new SelectList(CacDot);
            return View(Form);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexDGNL(tblFormTuyenSinh form, string flexSwitchCheckDefault, string Dot) // form xét tuyển DGNL Quốc gia
        {
            SEP25Team08Entities db = new SEP25Team08Entities();
            
            var save = db.tblFormTuyenSinhs.Find(1);
            string demo = flexSwitchCheckDefault;
            if (form.NgayBatDau <= form.NgayKetThuc)
            {
                if (demo == null)
                {
                    if(Dot.Equals("---- Chọn Đợt----"))
                    {
                        ViewBag.ErrorMessageDot = "Chọn đợt";
                        var Dot1 = model.tblDotTuyenSinhDGNLs.ToList();
                        List<string> CacDot = new List<string>();
                        CacDot.Add("---- Chọn Đợt----");
                        foreach (var dot in Dot1)
                        {
                            CacDot.Add("Đợt " + dot.Dot);
                        }
                        ViewBag.Dot = new SelectList(CacDot);
                        return View();
                    }
                    else
                    {
                        save.Dot = Dot;
                        save.Tieu_De = form.Tieu_De;
                        save.Noi_Dung = form.Noi_Dung;
                        save.Open_Close = false;
                        save.NgayBatDau = form.NgayBatDau;
                        save.NgayKetThuc = form.NgayKetThuc;
                        save.thongbao = form.thongbao;
                        model.Entry(save).State = EntityState.Modified;
                        model.SaveChanges();
                        return RedirectToAction("Index", "FormContent");
                    }
                   
                }
                else
                {
                    if (Dot.Equals("---- Chọn Đợt----"))
                    {
                        ViewBag.ErrorMessageDot = "Chọn đợt";
                        var Dot1 = model.tblDotTuyenSinhDGNLs.ToList();
                        List<string> CacDot = new List<string>();
                        CacDot.Add("---- Chọn Đợt----");
                        foreach (var dot in Dot1)
                        {
                            CacDot.Add("Đợt " + dot.Dot);
                        }
                        ViewBag.Dot = new SelectList(CacDot);
                        return View();
                    }
                    else
                    {
                        save.Dot = Dot;
                        save.Tieu_De = form.Tieu_De;
                        save.Noi_Dung = form.Noi_Dung;
                        save.Open_Close = false;
                        save.NgayBatDau = form.NgayBatDau;
                        save.NgayKetThuc = form.NgayKetThuc;
                        save.thongbao = form.thongbao;
                        model.Entry(save).State = EntityState.Modified;
                        model.SaveChanges();
                        return RedirectToAction("Index", "FormContent");
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Nhập ngày không hợp lệ";
            }
            ViewBag.Dot = new SelectList(model.tblFormTuyenSinhs,"TenDot");
            return View();
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
                Form.NgayBatDauEdit = form.NgayBatDauEdit;
                Form.NgayKetThucEdit = form.NgayKetThucEdit;
                model.Entry(Form).State = EntityState.Modified;
                model.SaveChanges();
            }
            else
            {

                Form.Edit_Open = true;
                Form.NgayBatDauEdit = form.NgayBatDauEdit;
                Form.NgayKetThucEdit = form.NgayKetThucEdit;
                model.Entry(Form).State = EntityState.Modified;
                model.SaveChanges();
            }
            return View("Index");
        }

    }
}