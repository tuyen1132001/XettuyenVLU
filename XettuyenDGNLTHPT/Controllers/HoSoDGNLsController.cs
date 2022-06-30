using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class HoSoDGNLsController : Controller
    {
        private demo2Entities db = new demo2Entities();
        demo2Entities model = new demo2Entities(); 

        // GET: HoSoDGNLs
        public ActionResult Index()
        {
            var DGNL = new tblHoSoDGNL();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.DanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.TonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");
            ViewBag.TinhTP = new SelectList(model.tblTinhTPs, "MA_TINHTP", "TEN_TINHTP"); ;

            //ViewBag.CCNN = new SelectList(model.tblChungChiNNs, "ID", "MaNN"+"ChungChi"+"DiemQuiDoi");
            var CCNN = model.tblChungChiNNs.ToList();
            List<string> ChungChi = new List<string>();
            ChungChi.Add("-- Chọn chứng chỉ ngoại ngữ --");
            foreach (var cc in CCNN)
            {
                ChungChi.Add(cc.MaNN + "-" + cc.ChungChi + "-" + cc.DiemQuiDoi);
            }
            ViewBag.CCNN = new SelectList(ChungChi);
            //
            var TP_QH_PX = model.tblTP_QH_PX.Select(e => new { e.MaTinhTP, e.TenTinhTP }).Distinct().ToList();
            TP_QH_PX.Insert(0, new { MaTinhTP = "-1", TenTinhTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.TP_QH_PX = new SelectList(TP_QH_PX, "MaTinhTP", "TenTinhTP");
            //
            var THPT = model.tblTruongTHPTs.Select(e => new { e.MA_TINHTP, e.TEN_TINHTP }).Distinct().ToList();
            THPT.Insert(0, new { MA_TINHTP = "-1", TEN_TINHTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.THPT = new SelectList(THPT, "MA_TINHTP", "TEN_TINHTP");
            //
            var Majors = model.tblNganhs.Select(e => new { e.MA_NGANH, e.TEN_NGANH }).Distinct().ToList();
            Majors.Insert(0, new { MA_NGANH = "-1", TEN_NGANH = "--------------Chọn-------------- " });
            ViewBag.NGANH = new SelectList(Majors, "MA_NGANH", "TEN_NGANH");

            return View(DGNL);
        }
        [HttpPost]
        public ActionResult Index(tblHoSoDGNL tblHoSoDGNL) //Form Dang ky THTP QG
        {
            if (ModelState.IsValid)
            {
                model.tblHoSoDGNLs.Add(tblHoSoDGNL);
                model.SaveChanges();
            }
            return View();
        }
        // GET: HoSoDGNLs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHoSoDGNL tblHoSoDGNL = db.tblHoSoDGNLs.Find(id);
            if (tblHoSoDGNL == null)
            {
                return HttpNotFound();
            }
            return View(tblHoSoDGNL);
        }

        // GET: HoSoDGNLs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoSoDGNLs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,HoVaTen,Email,GioiTinh,NgaySinh,MaNoiSinh,TenNoiSinh,MaDanToc,TenDanToc,MaTonGiao,TenTonGiao,CMND,QuocTich,HoKhau,HoKhau_MaPhuong,HoKhau_TenPhuong,HoKhau_MaTinhTP,HoKhau_TenTinhTP,HoKhau_MaQH,HoKhau_TenQH,NamTotNghiep,SoBaoDanh,HocLucLop12,HanhKiemLop12,LoaiHinhTN,TruongTHPT_MaTinhTP,TruongTHPT_TenTinhTP,TruongTHPT_MaQH,TruongTHPT_TenQH,TenTruongTHPT,MaTruongTHPT,TenLop12,KhuVuc,DiemDGNL,TGThiDGNL,DoiTuongUuTien,CCNN,MaNganh_ToHop1,TenNganh_TenToHop1,MaNganh_ToHop2,TenNganh_TenToHop2,MaNganh_ToHop3,TenNganh_TenToHop3,CTCT,CTDB,LienLac_DiaChi,LienLac_MaPhuongXa,LienLac_TenPhuongXa,LienLac_MaTP,LienLac_TenTP,LienLac_MaQH,LienLac_TenQH,DienThoaiDD,DienThoaiPhuHuynh,DateInserted,DateEdited,DaNhanHoSo,DiemVeMyThuat,DiemVeNangKhieu")] tblHoSoDGNL tblHoSoDGNL)
        //{
        //    var HoSoDGNL = new tblHoSoDGNL();
        //    ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
        //    ViewBag.DanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
        //    ViewBag.TonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");
        //    ViewBag.TinhTP = new SelectList(model.tblTinhTPs, "MA_TINHTP", "TEN_TINHTP");
        //    return View(HoSoDGNL);
        //    if (ModelState.IsValid)
        //    {
        //        db.tblHoSoDGNLs.Add(tblHoSoDGNL);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tblHoSoDGNL);
        //}

        // GET: HoSoDGNLs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHoSoDGNL tblHoSoDGNL = db.tblHoSoDGNLs.Find(id);
            if (tblHoSoDGNL == null)
            {
                return HttpNotFound();
            }
            return View(tblHoSoDGNL);
        }

        // POST: HoSoDGNLs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,Email,GioiTinh,NgaySinh,MaNoiSinh,TenNoiSinh,MaDanToc,TenDanToc,MaTonGiao,TenTonGiao,CMND,QuocTich,HoKhau,HoKhau_MaPhuong,HoKhau_TenPhuong,HoKhau_MaTinhTP,HoKhau_TenTinhTP,HoKhau_MaQH,HoKhau_TenQH,NamTotNghiep,SoBaoDanh,HocLucLop12,HanhKiemLop12,LoaiHinhTN,TruongTHPT_MaTinhTP,TruongTHPT_TenTinhTP,TruongTHPT_MaQH,TruongTHPT_TenQH,TenTruongTHPT,MaTruongTHPT,TenLop12,KhuVuc,DiemDGNL,TGThiDGNL,DoiTuongUuTien,CCNN,MaNganh_ToHop1,TenNganh_TenToHop1,MaNganh_ToHop2,TenNganh_TenToHop2,MaNganh_ToHop3,TenNganh_TenToHop3,CTCT,CTDB,LienLac_DiaChi,LienLac_MaPhuongXa,LienLac_TenPhuongXa,LienLac_MaTP,LienLac_TenTP,LienLac_MaQH,LienLac_TenQH,DienThoaiDD,DienThoaiPhuHuynh,DateInserted,DateEdited,DaNhanHoSo,DiemVeMyThuat,DiemVeNangKhieu")] tblHoSoDGNL tblHoSoDGNL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblHoSoDGNL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblHoSoDGNL);
        }

        // GET: HoSoDGNLs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHoSoDGNL tblHoSoDGNL = db.tblHoSoDGNLs.Find(id);
            if (tblHoSoDGNL == null)
            {
                return HttpNotFound();
            }
            return View(tblHoSoDGNL);
        }

        // POST: HoSoDGNLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblHoSoDGNL tblHoSoDGNL = db.tblHoSoDGNLs.Find(id);
            db.tblHoSoDGNLs.Remove(tblHoSoDGNL);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult GetDistrict(string id) // lấy quận huyện
        {
            var data = model.tblTP_QH_PX
                                .Where(e => e.MaTinhTP == id)
                                .Select(e => new { Id = e.MaQH, Name = e.TenQH })
                                .Distinct()
                                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWard(string id) // lấy phường xã
        {
            var data = model.tblTP_QH_PX
                                .Where(e => e.MaQH == id)
                                .Select(e => new { Id = e.MaPX, Name = e.TenPX })
                                .Distinct()
                                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrictSchool(string id)
        {
            var data = model.tblTruongTHPTs
                                .Where(e => e.MA_TINHTP == id)
                                .Select(e => new { Id = e.MA_QH, Name = e.TEN_QH })
                                .Distinct()
                                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSchool(string id)
        {
            var idQH = id.Split('-')[0];
            var idTP = id.Split('-')[1];
            var data = model.tblTruongTHPTs
                .Where(e => e.MA_QH == idQH && e.MA_TINHTP == idTP)
                .Select(e => new { Id = e.MA_TRUONG, Name = e.TEN_TRUONG })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetArea(string id)
        {
            var data = model.tblTruongTHPTs
                .Where(e => e.MA_TRUONG == id)
                .Select(e => new { Name = e.KHU_VUC })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
