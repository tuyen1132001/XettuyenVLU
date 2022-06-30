using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XettuyenDGNLTHPT.Models
{
    [Table("tblTinhTP")]
    public class TinhTP:Entity
    {
        [Key]
        public string MA_TINHTP { get; set; }
        public string TEN_TINHTP { get; set; }
        
    }
}