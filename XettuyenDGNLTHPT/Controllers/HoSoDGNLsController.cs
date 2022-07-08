using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;
using System.Net.Mail;

namespace XettuyenDGNLTHPT.Controllers
{
    public class HoSoDGNLsController : Controller
    {
        private SEP25Team08Entities db = new SEP25Team08Entities();
        SEP25Team08Entities model = new SEP25Team08Entities(); 

        // GET: HoSoDGNLs
        public ActionResult Index()
        {
            var DGNL = new tblHoSoDGNL();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.MaDanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.MaTonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");
            ViewBag.MaNoiSinh = new SelectList(model.tblTinhTPs, "MA_TINHTP", "TEN_TINHTP"); ;

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
        public ActionResult Index(tblHoSoDGNL tblHoSoDGNL, string TP_QH_PX, string ddlHoKhauQuanHuyen, string ddlHoKhau_PhuongXa, string THPT, string ddlQuanHuyenTHPT, string ddlTenTruongTHPT, string ddlKhuVuc
                                    , string ddlDoiTuongUT, string LienHeTP, string ddlQuanHuyen, string ddlPhuongXa, string Majors1, string Majors2, string Majors3) //Form Dang ky THTP QG
        {
            if (ModelState.IsValid)
            {
                
                var dbNoiSinh = model.tblTP_QH_PX.FirstOrDefault(u=>u.MaTinhTP.Equals(tblHoSoDGNL.MaNoiSinh));
                string  NameNoisinh = dbNoiSinh.TenTinhTP;
                tblHoSoDGNL.TenNoiSinh = NameNoisinh;
                //Dân tộc
                var dbDanToc = model.tblDanTocs.Find(tblHoSoDGNL.MaDanToc);
                tblHoSoDGNL.TenDanToc = dbDanToc.TEN_DANTOC + "";
                //Quốc tịch
                var dbQuocTich = model.tblQuocTiches.Find(int.Parse(tblHoSoDGNL.QuocTich));
                string quoctich = dbQuocTich.MaQT + "|" + dbQuocTich.TenQT;
                tblHoSoDGNL.QuocTich = quoctich;
                //Tôn giáo
                var dbTonGiao = model.tblTonGiaos.Find(tblHoSoDGNL.MaTonGiao);
                string Nametongiao = dbTonGiao.TEN_TONGIAO;
                tblHoSoDGNL.TenTonGiao = Nametongiao;
                // Hộ khẩu địa chỉ
                var dbHoKhauTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(TP_QH_PX));
                string Nametp = dbHoKhauTP.TenTinhTP;
                tblHoSoDGNL.HoKhau_MaTinhTP = TP_QH_PX;
                tblHoSoDGNL.HoKhau_TenTinhTP = Nametp;
                var dbQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(ddlHoKhauQuanHuyen));
                string NameQuanHuyen = dbQuanHuyen.TenQH;
                tblHoSoDGNL.HoKhau_MaQH = ddlHoKhauQuanHuyen;
                tblHoSoDGNL.HoKhau_TenQH = NameQuanHuyen;
                var dbPhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(ddlHoKhau_PhuongXa));
                string NamePhuongXa = dbPhuongXa.TenPX;
                tblHoSoDGNL.HoKhau_MaPhuong = ddlHoKhau_PhuongXa;
                tblHoSoDGNL.HoKhau_TenPhuong = NamePhuongXa;
                //Địa chỉ trường THPT
                var dbDiaChiTrgtp = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TINHTP.Equals(THPT));
                string Namediachitrgtp = dbDiaChiTrgtp.TEN_TINHTP;
                tblHoSoDGNL.TruongTHPT_MaTinhTP = THPT;
                tblHoSoDGNL.TruongTHPT_TenTinhTP = Namediachitrgtp;
                var dbTrgQuanHuyen = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_QH.Equals(ddlQuanHuyenTHPT) && u.MA_TINHTP.Equals(THPT));
                string NameTrgQuanHuyen = dbTrgQuanHuyen.TEN_QH;
                tblHoSoDGNL.TruongTHPT_MaQH = ddlQuanHuyenTHPT;
                tblHoSoDGNL.TruongTHPT_TenQH = NameTrgQuanHuyen;
                var dbTrg = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TRUONG.Equals(ddlTenTruongTHPT) && u.TEN_QH.Equals(NameTrgQuanHuyen) && u.MA_TINHTP.Equals(THPT));
                string NameTrg = dbTrg.TEN_TRUONG;
                tblHoSoDGNL.MaTruongTHPT = THPT + ddlTenTruongTHPT;
                tblHoSoDGNL.TenTruongTHPT = ddlTenTruongTHPT + " - " + NameTrg;
                //Thông tin liên hệ
                var dbLienHeTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(LienHeTP));
                string Namelienhetp = dbLienHeTP.TenTinhTP;
                tblHoSoDGNL.LienLac_MaTP = LienHeTP;
                tblHoSoDGNL.LienLac_TenTP = Namelienhetp;
                var dbLienHeQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(ddlQuanHuyen));
                string NameLienHeQuanHuyen = dbLienHeQuanHuyen.TenQH;
                tblHoSoDGNL.LienLac_MaQH = ddlQuanHuyen;
                tblHoSoDGNL.LienLac_TenQH = NameLienHeQuanHuyen;
                var dbLienHePhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(ddlPhuongXa));
                string NameLienHePhuongXa = dbLienHePhuongXa.TenPX;
                tblHoSoDGNL.LienLac_MaPhuongXa = ddlPhuongXa;
                tblHoSoDGNL.LienLac_TenPhuongXa = NameLienHePhuongXa;
                //Khu vực và đối tượng 
                tblHoSoDGNL.KhuVuc = ddlKhuVuc;
                if (ddlDoiTuongUT.Equals("Không có"))
                {
                    tblHoSoDGNL.DoiTuongUuTien = "0";
                }
                else
                {
                    tblHoSoDGNL.DoiTuongUuTien = ddlDoiTuongUT.ToString();
                }

                // Nganh va To hop 1/2/3
                var dbNganhTohop1 = model.tblNganhs.FirstOrDefault(u => u.MA_NGANH.Equals(Majors1));
                string NameNganh1 = dbNganhTohop1.TEN_NGANH;
                var dbNganhTohop2 = model.tblNganhs.FirstOrDefault(u => u.MA_NGANH.Equals(Majors2));
                string NameNganh2 = dbNganhTohop2.TEN_NGANH;
                var dbNganhTohop3 = model.tblNganhs.FirstOrDefault(u => u.MA_NGANH.Equals(Majors3));
                string NameNganh3 = dbNganhTohop3.TEN_NGANH;
                tblHoSoDGNL.MaNganh_ToHop1 = Majors1; tblHoSoDGNL.TenNganh_TenToHop1 = NameNganh1;
                tblHoSoDGNL.MaNganh_ToHop2 = Majors2; tblHoSoDGNL.TenNganh_TenToHop2 = NameNganh2;
                tblHoSoDGNL.MaNganh_ToHop3 = Majors3; tblHoSoDGNL.TenNganh_TenToHop3 = NameNganh3;
                return RedirectToAction("Details", tblHoSoDGNL);
            }
            
            return View();
        }
        // GET: HoSoDGNLs/Details/5
   
     
        public ActionResult Details(tblHoSoDGNL tblHoSoDGNL)
        {
            var hoso = tblHoSoDGNL;
 

            return View(hoso);
        }
        
        public ActionResult Save(tblHoSoDGNL DGNL)
        {
            
            model.tblHoSoDGNLs.Add(DGNL);
            model.SaveChanges();
            var hoso = model.tblHoSoDGNLs.FirstOrDefault(u =>u.CMND.Equals(DGNL.CMND));
            string from = "khontuyen2001@gmail.com";
            string pass = "tuyenhero123";
            //MailMessage mail = new MailMessage();
            //mail.To.Add(hoso.Email);
            //mail.From = new MailAddress("khontuyen2001@gmail.com");
           
            //mail.Subject = "Đã tiếp nhận hồ sơ";
            //mail.Body = "Chào bạn" + hoso.HoVaTen + "<br>Cảm ơn bạn đã đăng ký xét tuyển vào Trường Đại học Văn Lang trong năm</br> " + "Mã hồ sơ của bạn là NL_"+hoso.ID+
            //             "<br>Kết quả tuyển sinh sẽ được Trường Đại học Văn Lang công bố và thông báo cho bạn sau khi kết thúc đợt nhận hồ sơ (dự kiến trong tháng 5/2021).</br>";
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(from, pass)
            //};
            //using (var mess = new MailMessage(from, "khontuyen2001@gmail.com")
            //{
            //    Subject = mail.Subject,
            //    Body = mail.Body
            //})
            //{
            //    smtp.Send(mess);
            //}
            return View(hoso);
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
