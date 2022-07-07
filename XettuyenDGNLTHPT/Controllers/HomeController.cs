using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Controllers
{
    public class HomeController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        public ActionResult Index() //Form Dang ky THTP QG
        {
            var HoSoTHPT = new tblHoSoTHPT();
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



            var THPT = model.tblTruongTHPTs.Select(e => new { e.MA_TINHTP, e.TEN_TINHTP }).Distinct().ToList();
            THPT.Insert(0, new { MA_TINHTP = "-1", TEN_TINHTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.THPT = new SelectList(THPT, "MA_TINHTP", "TEN_TINHTP");

            var Majors = model.tblNganhs.Select(e => new { e.MA_NGANH, e.TEN_NGANH }).Distinct().ToList();
            Majors.Insert(0, new { MA_NGANH = "-1", TEN_NGANH = "--------------Chọn-------------- " });
            ViewBag.NGANH = new SelectList(Majors, "MA_NGANH", "TEN_NGANH");

            return View();
        }

        [HttpPost]
        public ActionResult Index(tblHoSoTHPT tblHoSoTHPT, string TP_QH_PX, string ddlHoKhauQuanHuyen, string ddlHoKhau_PhuongXa, string THPT, string ddlQuanHuyenTHPT, string ddlTenTruongTHPT, string ddlKhuVuc
                                    , string ddlDoiTuongUT, string LienHeTP, string ddlQuanHuyen, string ddlPhuongXa, string Majors1, string Majors2, string Majors3, string ddlToHopMon1, string ddlToHopMon2, string ddlToHopMon3) //Form Dang ky THTP QG
        {
            if (ModelState.IsValid)
            {
                var dbNoiSinh = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(tblHoSoTHPT.MaNoiSinh));
                string NameNoisinh = dbNoiSinh.TenTinhTP;


                tblHoSoTHPT.TenNoiSinh = NameNoisinh;
                //Dân tộc
                var dbDanToc = model.tblDanTocs.Find(tblHoSoTHPT.MaDanToc);
                tblHoSoTHPT.TenDanToc = dbDanToc.TEN_DANTOC + "";
                //Quốc tịch
                var dbQuocTich = model.tblQuocTiches.Find(int.Parse(tblHoSoTHPT.QuocTich));
                string quoctich = dbQuocTich.MaQT + "|" + dbQuocTich.TenQT;
                tblHoSoTHPT.QuocTich = quoctich;
                //Tôn giáo
                var dbTonGiao = model.tblTonGiaos.Find(tblHoSoTHPT.MaTonGiao);
                string Nametongiao = dbTonGiao.TEN_TONGIAO;
                tblHoSoTHPT.TenTonGiao = Nametongiao;
                // Hộ khẩu địa chỉ
                var dbHoKhauTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(TP_QH_PX));
                string Nametp = dbHoKhauTP.TenTinhTP;
                tblHoSoTHPT.HoKhau_MaTinhTP = TP_QH_PX;
                tblHoSoTHPT.HoKhau_TenTinhTP = Nametp;
                var dbQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(ddlHoKhauQuanHuyen));
                string NameQuanHuyen = dbQuanHuyen.TenQH;
                tblHoSoTHPT.HoKhau_MaQH = ddlHoKhauQuanHuyen;
                tblHoSoTHPT.HoKhau_TenQH = NameQuanHuyen;
                var dbPhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(ddlHoKhau_PhuongXa));
                string NamePhuongXa = dbPhuongXa.TenPX;
                tblHoSoTHPT.HoKhau_MaPhuong = ddlHoKhau_PhuongXa;
                tblHoSoTHPT.HoKhau_TenPhuong = NamePhuongXa;
                //Địa chỉ trường THPT
                var dbDiaChiTrgtp = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TINHTP.Equals(THPT));
                string Namediachitrgtp = dbDiaChiTrgtp.TEN_TINHTP;
                tblHoSoTHPT.TruongTHPT_MaTinhTP = THPT;
                tblHoSoTHPT.TruongTHPT_TenTinhTP = Namediachitrgtp;
                var dbTrgQuanHuyen = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_QH.Equals(ddlQuanHuyenTHPT) && u.MA_TINHTP.Equals(THPT));
                string NameTrgQuanHuyen = dbTrgQuanHuyen.TEN_QH;
                tblHoSoTHPT.TruongTHPT_MaQH = ddlQuanHuyenTHPT;
                tblHoSoTHPT.TruongTHPT_TenQH = NameTrgQuanHuyen;
                var dbTrg = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TRUONG.Equals(ddlTenTruongTHPT) && u.TEN_QH.Equals(NameTrgQuanHuyen) && u.MA_TINHTP.Equals(THPT));
                string NameTrg = dbTrg.TEN_TRUONG;
                tblHoSoTHPT.MaTruongTHPT = THPT + ddlTenTruongTHPT;
                tblHoSoTHPT.TenTruongTHPT = ddlTenTruongTHPT + " - " + NameTrg;
                //Thông tin liên hệ
                var dbLienHeTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(LienHeTP));
                string Namelienhetp = dbLienHeTP.TenTinhTP;
                tblHoSoTHPT.LienLac_MaTP = LienHeTP;
                tblHoSoTHPT.LienLac_TenTP = Namelienhetp;
                var dbLienHeQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(ddlQuanHuyen));
                string NameLienHeQuanHuyen = dbLienHeQuanHuyen.TenQH;
                tblHoSoTHPT.LienLac_MaQH = ddlQuanHuyen;
                tblHoSoTHPT.LienLac_TenQH = NameLienHeQuanHuyen;
                var dbLienHePhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(ddlPhuongXa));
                string NameLienHePhuongXa = dbLienHePhuongXa.TenPX;
                tblHoSoTHPT.LienLac_MaPhuongXa = ddlPhuongXa;
                tblHoSoTHPT.LienLac_TenPhuongXa = NameLienHePhuongXa;
                //Khu vực và đối tượng 
                var dbKhuVuc = model.tblTruongTHPTs.FirstOrDefault(u => u.KHU_VUC.Equals(ddlKhuVuc));
                tblHoSoTHPT.KhuVuc = dbKhuVuc.KHU_VUC;
                if (ddlDoiTuongUT.Equals("Không có"))
                {
                    tblHoSoTHPT.DoiTuongUuTien = "0";
                }
                else
                {
                    tblHoSoTHPT.DoiTuongUuTien = ddlDoiTuongUT.ToString();
                }
                if (tblHoSoTHPT.CCNN.Equals("-- Chọn chứng chỉ ngoại ngữ --"))
                {
                    tblHoSoTHPT.CCNN = "";
                }
                // Nganh va To hop 1/2/3
                var dbNganhTohop1 = model.tblNganhs.FirstOrDefault(u => u.MANGANH_TOHOP.Equals(Majors1 + ddlToHopMon1));
                string NameNganhTohop1 = dbNganhTohop1.TEN_NGANH + "#" + dbNganhTohop1.MA_TOHOP + " - " + dbNganhTohop1.TEN_TOHOP;
                var dbNganhTohop2 = model.tblNganhs.FirstOrDefault(u => u.MANGANH_TOHOP.Equals(Majors2 + ddlToHopMon2));
                string NameNganhTohop2 = dbNganhTohop2.TEN_NGANH + "#" + dbNganhTohop2.MA_TOHOP + " - " + dbNganhTohop2.TEN_TOHOP;
                var dbNganhTohop3 = model.tblNganhs.FirstOrDefault(u => u.MANGANH_TOHOP.Equals(Majors3 + ddlToHopMon3));
                string NameNganhTohop3 = dbNganhTohop3.TEN_NGANH + "#" + dbNganhTohop3.MA_TOHOP + " - " + dbNganhTohop3.TEN_TOHOP;
                tblHoSoTHPT.MaNganh_ToHop1 = Majors1 + "#" + ddlToHopMon1; tblHoSoTHPT.TenNganh_TenToHop1 = NameNganhTohop1;
                tblHoSoTHPT.MaNganh_ToHop2 = Majors2 + "#" + ddlToHopMon2; tblHoSoTHPT.TenNganh_TenToHop2 = NameNganhTohop2;
                tblHoSoTHPT.MaNganh_ToHop3 = Majors3 + "#" + ddlToHopMon3; tblHoSoTHPT.TenNganh_TenToHop3 = NameNganhTohop3;
                return RedirectToAction("Detail", tblHoSoTHPT);
            }
            return View();
        }

        public ActionResult Detail(tblHoSoTHPT thpt)
        {
            var hoso = thpt;
            
            return View(hoso);

        }

       public ActionResult Save(tblHoSoTHPT thpt)
        {
            model.tblHoSoTHPTs.Add(thpt);
            model.SaveChanges();
            var hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND.Equals(thpt.CMND));

            MailMessage mail = new MailMessage();
            mail.To.Add(hoso.Email);
            mail.From = new MailAddress("xettuyenvanlang@gmail.com");
            mail.Subject = "Đã tiếp nhận hồ sơ";
            mail.Body = "Chào bạn" + hoso.HoVaTen + "<br>Cảm ơn bạn đã đăng ký xét tuyển vào Trường Đại học Văn Lang trong năm</br> " + "Mã hồ sơ của bạn là NL_" + hoso.ID +
                         "<br>Kết quả tuyển sinh sẽ được Trường Đại học Văn Lang công bố và thông báo cho bạn sau khi kết thúc đợt nhận hồ sơ (dự kiến trong tháng 5/2021).</br>";


            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Port = 25;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("xettuyenvanlang@gmail.com", "rywijjglguaugjex"); //Email, mật khẩu ứng dụng
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View(hoso);
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
                .Select(e => new { Id = e.KHU_VUC, Name = e.KHU_VUC })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMajors1(string id)// tim to hop theo nganh
        {
            var data = model.tblNganhs
                .Where(e => e.MA_NGANH == id)
                .Select(e => new { Id = e.MA_TOHOP, Name = e.MA_TOHOP + "-" + e.TEN_TOHOP })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMajors2(string id)// tim to hop theo nganh
        {
            var data = model.tblNganhs
                .Where(e => e.MA_NGANH == id)
                .Select(e => new { Id = e.MA_TOHOP, Name = e.MA_TOHOP + "-" + e.TEN_TOHOP })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMajors3(string id)// tim to hop theo nganh
        {
            var data = model.tblNganhs
                .Where(e => e.MA_NGANH == id)
                .Select(e => new { Id = e.MA_TOHOP, Name = e.MA_TOHOP + "-" + e.TEN_TOHOP })
                .Distinct()
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}