//------------------------------------------------------------------------------
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

    public partial class tblFormTuyenSinh
    {
        public int ID { get; set; }
        public string Tieu_De { get; set; }
        public string Noi_Dung { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        public Nullable<bool> Open_Close { get; set; }
        public string Loai { get; set; }
        public Nullable<bool> Edit_Open { get; set; }
    }
}
