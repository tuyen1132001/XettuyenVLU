﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XettuyenDGNLTHPT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblHoSoTHPT
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [RegularExpression(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ ]*$",
        ErrorMessage = "Vui lòng nhập đúng Họ và Tên")]
        public string HoVaTen { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Phải điền đúng định dạng Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public Nullable<bool> GioiTinh { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public Nullable<System.DateTime> NgaySinh { get; set; }
       
        public string MaNoiSinh { get; set; }
        public string TenNoiSinh { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string MaDanToc { get; set; }
        public string TenDanToc { get; set; }
        public string MaTonGiao { get; set; }
        public string TenTonGiao { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [MaxLength(12, ErrorMessage = "CMND tối đa 12 số")]
        [MinLength(9, ErrorMessage = "CMND tối thiểu 9 số ")]
        public string CMND { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]

        public string QuocTich { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [RegularExpression(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9. ]*$",
        ErrorMessage = "Vui lòng nhập đúng địa chỉ")]
        public string HoKhau { get; set; }
        
        public string HoKhau_MaPhuong { get; set; }
        
        public string HoKhau_TenPhuong { get; set; }
        
        public string HoKhau_MaTinhTP { get; set; }
        
        public string HoKhau_TenTinhTP { get; set; }
        public string HoKhau_MaQH { get; set; }
        public string HoKhau_TenQH { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string NamTotNghiep { get; set; }
        public string SoBaoDanh { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string HocLucLop12 { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string HanhKiemLop12 { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string LoaiHinhTN { get; set; }
        public string TruongTHPT_MaTinhTP { get; set; }
        public string TruongTHPT_TenTinhTP { get; set; }
        public string TruongTHPT_MaQH { get; set; }
        public string TruongTHPT_TenQH { get; set; }
        public string TenTruongTHPT { get; set; }
        public string MaTruongTHPT { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string TenLop12 { get; set; }
        public string KhuVuc { get; set; }
        public string DoiTuongUuTien { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Toan { get; set; }

        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Van { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Anh { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Phap { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Ly { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Hoa { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Sinh { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Su { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> Dia { get; set; }
        [Range(1, 10, ErrorMessage = "Vui lòng nhập điểm từ 1 tới 10")]
        public Nullable<double> GDCD { get; set; }
        public string CCNN { get; set; }
        
        public string MaNganh_ToHop1 { get; set; }
        public string TenNganh_TenToHop1 { get; set; }
        public string MaNganh_ToHop2 { get; set; }
        public string TenNganh_TenToHop2 { get; set; }
        public string MaNganh_ToHop3 { get; set; }
        public string TenNganh_TenToHop3 { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        [RegularExpression(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9. ]*$",
        ErrorMessage = "Vui lòng nhập đúng địa chỉ")]
        public string LienLac_DiaChi { get; set; }
        public string LienLac_MaPhuongXa { get; set; }
        public string LienLac_TenPhuongXa { get; set; }
        public string LienLac_MaTP { get; set; }
        public string LienLac_TenTP { get; set; }
        public string LienLac_MaQH { get; set; }
        public string LienLac_TenQH { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string DienThoaiDD { get; set; }
        [Required(ErrorMessage = "Cần nhập vào dữ liệu này")]
        public string DienThoaiPhuHuynh { get; set; }
        public Nullable<System.DateTime> DateInserted { get; set; }
        public Nullable<System.DateTime> DateEdited { get; set; }
        public string DaNhanHoSo { get; set; }
        public string DiemVeMyThuat { get; set; }
        public string DiemVeNangKhieu { get; set; }
        public string CTDT1 { get; set; }
        public string CTDT2 { get; set; }
        public string CTDT3 { get; set; }
    }
}
