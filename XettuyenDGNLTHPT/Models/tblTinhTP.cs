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
    
    public partial class tblTinhTP
    {
        public string MA_TINHTP { get; set; }
        public string TEN_TINHTP { get; set; }
    
        public virtual tblTP_QH_PX tblTP_QH_PX { get; set; }
    }
}
