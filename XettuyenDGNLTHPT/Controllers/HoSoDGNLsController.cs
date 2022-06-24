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
        private XettuyenVLUEntities db = new XettuyenVLUEntities();
        XettuyenVLUEntities model = new XettuyenVLUEntities(); 

        // GET: HoSoDGNLs
        public ActionResult Index()
        {
            
            return View(db.tblHoSoDGNLs.ToList());
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTen,Email,GioiTinh,NgaySinh,MaNoiSinh,TenNoiSinh,MaDanToc,TenDanToc,MaTonGiao,TenTonGiao,CMND,QuocTich,HoKhau,HoKhau_MaPhuong,HoKhau_TenPhuong,HoKhau_MaTinhTP,HoKhau_TenTinhTP,HoKhau_MaQH,HoKhau_TenQH,NamTotNghiep,SoBaoDanh,HocLucLop12,HanhKiemLop12,LoaiHinhTN,TruongTHPT_MaTinhTP,TruongTHPT_TenTinhTP,TruongTHPT_MaQH,TruongTHPT_TenQH,TenTruongTHPT,MaTruongTHPT,TenLop12,KhuVuc,DiemDGNL,TGThiDGNL,DoiTuongUuTien,CCNN,MaNganh_ToHop1,TenNganh_TenToHop1,MaNganh_ToHop2,TenNganh_TenToHop2,MaNganh_ToHop3,TenNganh_TenToHop3,CTCT,CTDB,LienLac_DiaChi,LienLac_MaPhuongXa,LienLac_TenPhuongXa,LienLac_MaTP,LienLac_TenTP,LienLac_MaQH,LienLac_TenQH,DienThoaiDD,DienThoaiPhuHuynh,DateInserted,DateEdited,DaNhanHoSo,DiemVeMyThuat,DiemVeNangKhieu")] tblHoSoDGNL tblHoSoDGNL)
        {
            var HoSoDGNL = new tblHoSoDGNL();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.DanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.TonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");
            ViewBag.TinhTP = new SelectList(model.tblTinhTPs, "MA_TINHTP", "TEN_TINHTP");
            return View(HoSoDGNL);
            if (ModelState.IsValid)
            {
                db.tblHoSoDGNLs.Add(tblHoSoDGNL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblHoSoDGNL);
        }

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
    }
}
