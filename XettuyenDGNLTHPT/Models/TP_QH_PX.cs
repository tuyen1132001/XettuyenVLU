using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XettuyenDGNLTHPT.Models
{
    [Table("tblTP_QH_PX")]
    public class TP_QH_PX:Entity
    {
        [Key]
        public string TenTinhTP { get; set; } 
        public string MA_TINHTP { get; set; }
        public string TenQH { get; set; }
        public string MaQH { get; set; }
        public string TenPX { get; set; }
        public string MaPX { get; set; }
        public string Cap { get; set; }
        public string EnglishName { get; set; }
    }
}