using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult InHoSo(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            Session["HoSoDayNe"] = null;
            if (ddlLoaiXetTuyen == "THPT")
            {
                var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == CMND);
                if (Hoso != null)
                {
                    Session["HoSoDayNe"] = Hoso;
                    return View("DetailTHPT", Hoso);
                }
                else
                {
                    var sdt = model.tblHoSoTHPTs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                    if (sdt != null)
                    {
                        Session["HoSoDayNe"] = sdt;
                        return View("DetailTHPT", sdt);
                    }
                    Session["notfound"] = true;
                }
            }
            if (ddlLoaiXetTuyen == "DGNL")
            {
                var HosoDGNL = model.tblHoSoDGNLs.FirstOrDefault(u => u.CMND.Equals(CMND.Trim()));
                if (HosoDGNL != null)
                {
                    Session["HoSoDayNe"] = HosoDGNL;
                    return View("DetailDGNL", HosoDGNL);
                }
                else
                {
                    var sdt = model.tblHoSoDGNLs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                    if (sdt != null)
                    {
                        Session["HoSoDayNe"] = sdt;
                        return View("DetailDGNL", sdt);
                    }
                    Session["notfound"] = true;
                }
            }
            return View();
        }


        public ActionResult DetailTHPT(tblHoSoTHPT tHPT)
        {
            var form = model.tblFormTuyenSinhs.Find(0);

            if (form.NgayBatDauEdit <= DateTime.Today && DateTime.Today <= form.NgayKetThucEdit)
            {
                Session["Form-button"] = true;
                if (form.Edit_Open == true)
                {
                    Session["Form-button"] = false;
                }
                else
                {
                    Session["Form-button"] = true;
                }
            }
            else
            {
                Session["Form-button"] = false;
            }

            return View(tHPT);
        }
        [HttpPost]
        public ActionResult DetailTHPT(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            var form = model.tblFormTuyenSinhs.Find(0);

            if (form.NgayBatDauEdit <= DateTime.Today && DateTime.Today <= form.NgayKetThucEdit)
            {
                Session["Form-button"] = true;
                if (form.Edit_Open == true)
                {
                    Session["Form-button"] = false;
                }
                else
                {
                    Session["Form-button"] = true;
                }
            }
            else
            {
                Session["Form-button"] = false;
            }
            if (ddlLoaiXetTuyen == "THPT")
            {
                var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == CMND);
                if (Hoso != null)
                {
                    Session["HoSoDayNe"] = Hoso;
                    return View("DetailTHPT", Hoso);
                }
                else
                {
                    var sdt = model.tblHoSoTHPTs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                    if (sdt != null)
                    {
                        Session["HoSoDayNe"] = sdt;
                        return View("DetailTHPT", sdt);
                    }
                    Session["notfound"] = true;
                }
            }
            if (ddlLoaiXetTuyen == "DGNL")
            {
                var HosoDGNL = model.tblHoSoDGNLs.FirstOrDefault(u => u.CMND.Equals(CMND.Trim()));
                if (HosoDGNL != null)
                {
                    Session["HoSoDayNe"] = HosoDGNL;
                    return View("DetailDGNL", HosoDGNL);
                }
                else
                {
                    var sdt = model.tblHoSoDGNLs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                    if (sdt != null)
                    {
                        Session["HoSoDayNe"] = sdt;
                        return View("DetailDGNL", sdt);
                    }
                    Session["notfound"] = true;
                }

            }
            tblHoSoTHPT models = Session["HoSoDayNe"] as tblHoSoTHPT;
            if(models != null)
                return View("DetailTHPT", models);

            return View();
        }
        public ActionResult PrintTHPT(tblHoSoTHPT tHPT)
        {
            return View(tHPT);
        }

        public ActionResult DetailDGNL(tblHoSoDGNL dGNL)
        {
            var form = model.tblFormTuyenSinhs.Find(1);

            if (form.NgayBatDauEdit <= DateTime.Today && DateTime.Today <= form.NgayKetThucEdit)
            {
                Session["Form-button"] = true;
                if (form.Edit_Open == true)
                {
                    Session["Form-button"] = false;
                }
                else
                {
                    Session["Form-button"] = true;
                }
            }
            else
            {
                Session["Form-button"] = false;
            }
            var data = dGNL;
            Session["test"] = data;
            return View(data);
        }
        [HttpPost]
        public ActionResult DetailDGNL(string DropDownList1, string ddlLoaiXetTuyen, string CMND)
        {
            var form = model.tblFormTuyenSinhs.Find(1);

            if (form.NgayBatDauEdit <= DateTime.Today && DateTime.Today <= form.NgayKetThucEdit)
            {
                Session["Form-button"] = true;
                if (form.Edit_Open == true)
                {
                    Session["Form-button"] = false;
                }
                else
                {
                    Session["Form-button"] = true;
                }
            }
            else
            {
                Session["Form-button"] = false;
            }
            if (ddlLoaiXetTuyen == "THPT")
            {
                var Hoso = model.tblHoSoTHPTs.FirstOrDefault(u => u.CMND == CMND);
                if (Hoso != null)
                {
                    Session["HoSoDayNe"] = Hoso;
                    return View("DetailTHPT", Hoso);
                }
                else
                {
                    var sdt = model.tblHoSoTHPTs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                    if (sdt != null)
                    {
                        Session["HoSoDayNe"] = sdt;
                        return View("DetailTHPT", sdt);
                    }
                    Session["notfound"] = true;
                }
            }
            if (ddlLoaiXetTuyen == "DGNL")
            {
                var HosoDGNL = model.tblHoSoDGNLs.FirstOrDefault(u => u.CMND.Equals(CMND.Trim()));
                if (HosoDGNL != null)
                {
                    Session["HoSoDayNe"] = HosoDGNL;
                    return View("DetailDGNL", HosoDGNL);
                }
                else
                {
                    var sdt = model.tblHoSoDGNLs.FirstOrDefault(u => u.DienThoaiDD.Equals(CMND.Trim()));
                    if (sdt != null)
                    {
                        Session["HoSoDayNe"] = sdt;
                        return View("DetailDGNL", sdt);
                    }
                    Session["notfound"] = true;
                }

            }
            tblHoSoDGNL models = Session["HoSoDayNe"] as tblHoSoDGNL;
            if (models != null)
                return View("DetailDGNL", models);

            return View();
        }
        public ActionResult PrintDGNL(tblHoSoDGNL dgnl)
        {
            return View(dgnl);
        }
        public ActionResult EditTHPT(tblHoSoTHPT thpt)
        {
            tblHoSoTHPT tblHoSoTHPT = new tblHoSoTHPT();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.MaDanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.MaTonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");

            var CCNN = model.tblChungChiNNs.ToList();
            List<string> ChungChi = new List<string>();

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

            // Form tuyển sinh
            var form = model.tblFormTuyenSinhs.Find(0);
            Session["Form-Title"] = form.Tieu_De;
            Session["Form-Content"] = form.Noi_Dung;
            Session["Form-bodyTHPT"] = form.Open_Close;

            var formDGNL = model.tblFormTuyenSinhs.Find(1);
            Session["Form-TitleDGNl"] = formDGNL.Tieu_De;
            Session["Form-ContentDGNL"] = formDGNL.Noi_Dung;
            Session["Form-bodyDGNL"] = formDGNL.Open_Close;

            return View(thpt);
        }
        public ActionResult EditDGNL(tblHoSoDGNL dGNL)
        {
            tblHoSoDGNL tblHoSoDGNL = new tblHoSoDGNL();
            ViewBag.QuocTich = new SelectList(model.tblQuocTiches, "ID", "TenQT");
            ViewBag.MaDanToc = new SelectList(model.tblDanTocs, "MA_DANTOC", "TEN_DANTOC");
            ViewBag.MaTonGiao = new SelectList(model.tblTonGiaos, "MA_TONGIAO", "TEN_TONGIAO");

            var CCNN = model.tblChungChiNNs.ToList();
            List<string> ChungChi = new List<string>();

            foreach (var cc in CCNN)
            {
                ChungChi.Add(cc.MaNN + "-" + cc.ChungChi + "-" + cc.DiemQuiDoi);
            }
            ViewBag.CCNN = new SelectList(ChungChi);
            //

            var TP_QH_PX = model.tblTP_QH_PX.Select(e => new { e.MaTinhTP, e.TenTinhTP }).Distinct().ToList();
            TP_QH_PX.Insert(0, new { MaTinhTP = "-1", TenTinhTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.TP_QH_PX = new SelectList(TP_QH_PX, "MaTinhTP", "TenTinhTP");

            var TP_QH = model.tblTP_QH_PX.Where(u => u.MaTinhTP == dGNL.HoKhau_MaTinhTP).Select(e => new { e.MaQH, e.TenQH }).Distinct().ToList();
            TP_QH.Insert(0, new { MaQH = "-1", TenQH = "-- Chọn Quận Huyện--" });
            ViewBag.TP_QH = new SelectList(TP_QH, "MaQH", "TenQH");

            var PX = model.tblTP_QH_PX.Where(u => u.MaQH == dGNL.HoKhau_MaQH).Select(e => new { e.MaPX, e.TenPX }).Distinct().ToList();
            PX.Insert(0, new { MaPX = "-1", TenPX = "-- Chọn Phường Xã --" });
            ViewBag.PX = new SelectList(PX, "MaPX", "TenPX");
            //
            var LienHe_QH = model.tblTP_QH_PX.Where(u => u.MaTinhTP == dGNL.LienLac_MaTP).Select(e => new { e.MaQH, e.TenQH }).Distinct().ToList();
            LienHe_QH.Insert(0, new { MaQH = "-1", TenQH = "-- Chọn Quận Huyện--" });
            ViewBag.LienHe_QH = new SelectList(LienHe_QH, "MaQH", "TenQH");

            var LienHe_PX = model.tblTP_QH_PX.Where(u => u.MaQH == dGNL.LienLac_MaQH).Select(e => new { e.MaPX, e.TenPX }).Distinct().ToList();
            LienHe_PX.Insert(0, new { MaPX = "-1", TenPX = "-- Chọn Phường Xã --" });
            ViewBag.LienHe_PX = new SelectList(LienHe_PX, "MaPX", "TenPX");
            //
            var THPT = model.tblTruongTHPTs.Select(e => new { e.MA_TINHTP, e.TEN_TINHTP }).Distinct().ToList();
            THPT.Insert(0, new { MA_TINHTP = "-1", TEN_TINHTP = "-- Chọn tỉnh thành phố --" });
            ViewBag.THPT = new SelectList(THPT, "MA_TINHTP", "TEN_TINHTP");


            var Majors = model.tblNganhs.Select(e => new { e.MA_NGANH, e.TEN_NGANH }).Distinct().ToList();
            Majors.Insert(0, new { MA_NGANH = "-1", TEN_NGANH = "--------------Chọn-------------- " });
            ViewBag.NGANH = new SelectList(Majors, "MA_NGANH", "TEN_NGANH");

            // Form tuyển sinh
            var form = model.tblFormTuyenSinhs.Find(0);
            Session["Form-Title"] = form.Tieu_De;
            Session["Form-Content"] = form.Noi_Dung;
            Session["Form-bodyTHPT"] = form.Open_Close;

            var formDGNL = model.tblFormTuyenSinhs.Find(1);
            Session["Form-TitleDGNl"] = formDGNL.Tieu_De;
            Session["Form-ContentDGNL"] = formDGNL.Noi_Dung;
            Session["Form-bodyDGNL"] = formDGNL.Open_Close;
            var data = dGNL;

            return View(data);
        }

        [HttpPost]
        public ActionResult SaveDGNL(tblHoSoDGNL dGNL, string TP_QH_PX, string ddlHoKhauQuanHuyen, string ddlHoKhau_PhuongXa, string THPT, string ddlQuanHuyenTHPT, string ddlTenTruongTHPT, string ddlKhuVuc
                                     , string ddlDoiTuongUT, string LienHeTP, string ddlQuanHuyen, string ddlPhuongXa, string Majors1, string Majors2, string Majors3, string CTDT1, string CTDT2, string CTDT3)
        {
            var dbNoiSinh = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(dGNL.MaNoiSinh));
            string NameNoisinh = dbNoiSinh.TenTinhTP;


            dGNL.TenNoiSinh = NameNoisinh;
            //Dân tộc
            var dbDanToc = model.tblDanTocs.Find(dGNL.MaDanToc);
            dGNL.TenDanToc = dbDanToc.TEN_DANTOC;
            //Quốc tịch
            var dbQuocTich = model.tblQuocTiches.Find(int.Parse(dGNL.QuocTich));
            string quoctich = dbQuocTich.MaQT + "|" + dbQuocTich.TenQT;
            dGNL.QuocTich = quoctich;
            //Tôn giáo
            var dbTonGiao = model.tblTonGiaos.Find(dGNL.MaTonGiao);
            string Nametongiao = dbTonGiao.TEN_TONGIAO;
            dGNL.TenTonGiao = Nametongiao;
            // Hộ khẩu địa chỉ
            var dbHoKhauTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(dGNL.HoKhau_MaTinhTP));
            string Nametp = dbHoKhauTP.TenTinhTP;

            dGNL.HoKhau_TenTinhTP = Nametp;
            var dbQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(dGNL.HoKhau_MaQH));
            string NameQuanHuyen = dbQuanHuyen.TenQH;

            dGNL.HoKhau_TenQH = NameQuanHuyen;
            var dbPhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(dGNL.HoKhau_MaPhuong));
            string NamePhuongXa = dbPhuongXa.TenPX;

            dGNL.HoKhau_TenPhuong = NamePhuongXa;
            //Địa chỉ trường THPT
            var dbDiaChiTrgtp = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TINHTP.Equals(THPT));
            string Namediachitrgtp = dbDiaChiTrgtp.TEN_TINHTP;
            dGNL.TruongTHPT_MaTinhTP = THPT;
            dGNL.TruongTHPT_TenTinhTP = Namediachitrgtp;
            var dbTrgQuanHuyen = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_QH.Equals(ddlQuanHuyenTHPT) && u.MA_TINHTP.Equals(THPT));
            string NameTrgQuanHuyen = dbTrgQuanHuyen.TEN_QH;
            dGNL.TruongTHPT_MaQH = ddlQuanHuyenTHPT;
            dGNL.TruongTHPT_TenQH = NameTrgQuanHuyen;
            var dbTrg = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TRUONG.Equals(ddlTenTruongTHPT) && u.TEN_QH.Equals(NameTrgQuanHuyen) && u.MA_TINHTP.Equals(THPT));
            string NameTrg = dbTrg.TEN_TRUONG;
            dGNL.MaTruongTHPT = THPT + ddlTenTruongTHPT;
            dGNL.TenTruongTHPT = ddlTenTruongTHPT + " - " + NameTrg;
            //Thông tin liên hệ
            var dbLienHeTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(dGNL.LienLac_MaTP));
            string Namelienhetp = dbLienHeTP.TenTinhTP;

            dGNL.LienLac_TenTP = Namelienhetp;
            var dbLienHeQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(dGNL.LienLac_MaQH));
            string NameLienHeQuanHuyen = dbLienHeQuanHuyen.TenQH;

            dGNL.LienLac_TenQH = NameLienHeQuanHuyen;
            var dbLienHePhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(dGNL.LienLac_MaPhuongXa));
            string NameLienHePhuongXa = dbLienHePhuongXa.TenPX;

            dGNL.LienLac_TenPhuongXa = NameLienHePhuongXa;
            //Khu vực và đối tượng 
            var dbKhuVuc = model.tblTruongTHPTs.FirstOrDefault(u => u.KHU_VUC.Equals(ddlKhuVuc));
            dGNL.KhuVuc = dbKhuVuc.KHU_VUC;
            if (ddlDoiTuongUT.Equals("Không có"))
            {
                dGNL.DoiTuongUuTien = "0";
            }
            else
            {
                dGNL.DoiTuongUuTien = ddlDoiTuongUT.ToString();
            }

            // Nganh va To hop 1/2/3
            var dbNganhTohop1 = model.tblNganhs.FirstOrDefault(u => u.MA_NGANH.Equals(dGNL.MaNganh_ToHop1));
            string NameNganh1 = dbNganhTohop1.TEN_NGANH;
            dGNL.TenNganh_TenToHop1 = NameNganh1;
            dGNL.CTDT1 = CTDT1;
            if (dGNL.MaNganh_ToHop2 != "-1")
            {
                var dbNganhTohop2 = model.tblNganhs.FirstOrDefault(u => u.MA_NGANH.Equals(dGNL.MaNganh_ToHop2));
                string NameNganh2 = dbNganhTohop2.TEN_NGANH;
                dGNL.TenNganh_TenToHop2 = NameNganh2;
                dGNL.CTDT2 = CTDT2;
            }
            if (dGNL.MaNganh_ToHop3 != "-1")
            {
                var dbNganhTohop3 = model.tblNganhs.FirstOrDefault(u => u.MA_NGANH.Equals(dGNL.MaNganh_ToHop3));
                string NameNganh3 = dbNganhTohop3.TEN_NGANH;
                dGNL.TenNganh_TenToHop3 = NameNganh3;
                dGNL.CTDT3 = CTDT3;
            }

            model.Entry(dGNL).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("DetailDGNL", dGNL);
        }

        [HttpPost]
        public ActionResult SaveTHPT(string id, tblHoSoTHPT hoSo, string TP_QH_PX, string ddlHoKhauQuanHuyen, string ddlHoKhau_PhuongXa, string THPT, string ddlQuanHuyenTHPT, string ddlTenTruongTHPT, string ddlKhuVuc
                                     , string ddlDoiTuongUT, string LienHeTP, string ddlQuanHuyen, string ddlPhuongXa, string Majors1, string Majors2, string Majors3, string ddlToHopMon1, string ddlToHopMon2, string ddlToHopMon3, string CTDT1, string CTDT2, string CTDT3)
        {
            //model.tblHoSoTHPTs.Add(thpt);
            //model.SaveChanges();

            //return View(thpt);
            var dbNoiSinh = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(hoSo.MaNoiSinh));
            string NameNoisinh = dbNoiSinh.TenTinhTP;


            hoSo.TenNoiSinh = NameNoisinh;
            //Dân tộc
            var dbDanToc = model.tblDanTocs.Find(hoSo.MaDanToc);
            hoSo.TenDanToc = dbDanToc.TEN_DANTOC;
            //Quốc tịch
            var dbQuocTich = model.tblQuocTiches.Find(int.Parse(hoSo.QuocTich));
            string quoctich = dbQuocTich.MaQT + "|" + dbQuocTich.TenQT;
            hoSo.QuocTich = quoctich;
            //Tôn giáo
            var dbTonGiao = model.tblTonGiaos.Find(hoSo.MaTonGiao);
            string Nametongiao = dbTonGiao.TEN_TONGIAO;
            hoSo.TenTonGiao = Nametongiao;
            // Hộ khẩu địa chỉ
            var dbHoKhauTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(TP_QH_PX));
            string Nametp = dbHoKhauTP.TenTinhTP;
            hoSo.HoKhau_MaTinhTP = TP_QH_PX;
            hoSo.HoKhau_TenTinhTP = Nametp;
            var dbQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(ddlHoKhauQuanHuyen));
            string NameQuanHuyen = dbQuanHuyen.TenQH;
            hoSo.HoKhau_MaQH = ddlHoKhauQuanHuyen;
            hoSo.HoKhau_TenQH = NameQuanHuyen;
            var dbPhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(ddlHoKhau_PhuongXa));
            string NamePhuongXa = dbPhuongXa.TenPX;
            hoSo.HoKhau_MaPhuong = ddlHoKhau_PhuongXa;
            hoSo.HoKhau_TenPhuong = NamePhuongXa;
            //Địa chỉ trường THPT
            var dbDiaChiTrgtp = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TINHTP.Equals(THPT));
            string Namediachitrgtp = dbDiaChiTrgtp.TEN_TINHTP;
            hoSo.TruongTHPT_MaTinhTP = THPT;
            hoSo.TruongTHPT_TenTinhTP = Namediachitrgtp;
            var dbTrgQuanHuyen = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_QH.Equals(ddlQuanHuyenTHPT) && u.MA_TINHTP.Equals(THPT));
            string NameTrgQuanHuyen = dbTrgQuanHuyen.TEN_QH;
            hoSo.TruongTHPT_MaQH = ddlQuanHuyenTHPT;
            hoSo.TruongTHPT_TenQH = NameTrgQuanHuyen;
            var dbTrg = model.tblTruongTHPTs.FirstOrDefault(u => u.MA_TRUONG.Equals(ddlTenTruongTHPT) && u.TEN_QH.Equals(NameTrgQuanHuyen) && u.MA_TINHTP.Equals(THPT));
            string NameTrg = dbTrg.TEN_TRUONG;
            hoSo.MaTruongTHPT = THPT + ddlTenTruongTHPT;
            hoSo.TenTruongTHPT = ddlTenTruongTHPT + " - " + NameTrg;
            //Thông tin liên hệ
            var dbLienHeTP = model.tblTP_QH_PX.FirstOrDefault(u => u.MaTinhTP.Equals(LienHeTP));
            string Namelienhetp = dbLienHeTP.TenTinhTP;
            hoSo.LienLac_MaTP = LienHeTP;
            hoSo.LienLac_TenTP = Namelienhetp;
            var dbLienHeQuanHuyen = model.tblTP_QH_PX.FirstOrDefault(u => u.MaQH.Equals(ddlQuanHuyen));
            string NameLienHeQuanHuyen = dbLienHeQuanHuyen.TenQH;
            hoSo.LienLac_MaQH = ddlQuanHuyen;
            hoSo.LienLac_TenQH = NameLienHeQuanHuyen;
            var dbLienHePhuongXa = model.tblTP_QH_PX.FirstOrDefault(u => u.MaPX.Equals(ddlPhuongXa));
            string NameLienHePhuongXa = dbLienHePhuongXa.TenPX;
            hoSo.LienLac_MaPhuongXa = ddlPhuongXa;
            hoSo.LienLac_TenPhuongXa = NameLienHePhuongXa;
            //Khu vực và đối tượng 
            var dbKhuVuc = model.tblTruongTHPTs.FirstOrDefault(u => u.KHU_VUC.Equals(ddlKhuVuc));
            hoSo.KhuVuc = dbKhuVuc.KHU_VUC;
            if (ddlDoiTuongUT.Equals("Không có"))
            {
                hoSo.DoiTuongUuTien = "0";
            }
            else
            {
                hoSo.DoiTuongUuTien = ddlDoiTuongUT.ToString();
            }
            if (hoSo.CCNN.Equals("-- Chọn chứng chỉ ngoại ngữ --"))
            {
                hoSo.CCNN = "";
            }
            // Nganh va To hop 1/2/3
            if (Majors1 != "-1" && !string.IsNullOrWhiteSpace(ddlToHopMon1))
            {
                var dbNganhTohop1 = model.tblNganhs.FirstOrDefault(u => u.MANGANH_TOHOP.Equals(Majors1 + ddlToHopMon1));
                string NameNganhTohop1 = dbNganhTohop1.TEN_NGANH + "#" + dbNganhTohop1.MA_TOHOP + " - " + dbNganhTohop1.TEN_TOHOP;
                hoSo.MaNganh_ToHop1 = Majors1 + "#" + ddlToHopMon1; hoSo.TenNganh_TenToHop1 = NameNganhTohop1;
                hoSo.CTDT1 = CTDT1;
            }
            if (Majors2 != "-1" && !string.IsNullOrWhiteSpace(ddlToHopMon2))
            {
                var dbNganhTohop2 = model.tblNganhs.FirstOrDefault(u => u.MANGANH_TOHOP.Equals(Majors2 + ddlToHopMon2));
                string NameNganhTohop2 = dbNganhTohop2.TEN_NGANH + "#" + dbNganhTohop2.MA_TOHOP + " - " + dbNganhTohop2.TEN_TOHOP;
                hoSo.MaNganh_ToHop2 = Majors2 + "#" + ddlToHopMon2; hoSo.TenNganh_TenToHop2 = NameNganhTohop2;
                hoSo.CTDT2 = CTDT2;
            }
            if (Majors3 != "-1" && !string.IsNullOrWhiteSpace(ddlToHopMon3))
            {
                var dbNganhTohop3 = model.tblNganhs.FirstOrDefault(u => u.MANGANH_TOHOP.Equals(Majors3 + ddlToHopMon3));
                string NameNganhTohop3 = dbNganhTohop3.TEN_NGANH + "#" + dbNganhTohop3.MA_TOHOP + " - " + dbNganhTohop3.TEN_TOHOP;
                hoSo.MaNganh_ToHop3 = Majors3 + "#" + ddlToHopMon3; hoSo.TenNganh_TenToHop3 = NameNganhTohop3;
                hoSo.CTDT3 = CTDT3;
            }
            model.Entry(hoSo).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("DetailTHPT", hoSo);
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
        public JsonResult GetMajors(string id)// tim to hop theo nganh
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