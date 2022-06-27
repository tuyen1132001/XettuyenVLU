using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace XettuyenDGNLTHPT.Models
{
    public class MyDbContext:DbContext
    {
        private const string sqlConnection = @"data source=LAPTOP-M1A71T3G;initial catalog=XettuyenVLU;user id=sa;password=123456";
        public MyDbContext() : base(sqlConnection)
        {

        }
        //public MyDbContext() : base("TenKhaiBaoTrongWebConfig")
        //{

        //}
        public DbSet<TinhTP> TinhTPs { get; set; }
        public DbSet<TP_QH_PX> TP_QH_PXes { get; set; }
    }
}