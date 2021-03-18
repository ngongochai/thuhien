using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class Employer
    {
        public int MACT { get; set; }

        [StringLength(400)]
        public string Tencongty { get; set; }

        [StringLength(200)]
        public string Nguoilienhe { get; set; }

        [StringLength(11)]
        public string Dienthoaiban { get; set; }

        [StringLength(11)]
        public string Dienthoaididong { get; set; }

        [StringLength(200)]
        public string Emailtuyendung { get; set; }

        [StringLength(200)]
        public string Emaildangnhap { get; set; }

        [StringLength(200)]
        public string Matkhau { get; set; }
        public string Xacnhanmatkhau { get; set; }

        [StringLength(500)]
        public string Diachi { get; set; }
        public int? Manghanh1 { get; set; }
        public int? Manghanh2 { get; set; }
        public int? Manghanh3 { get; set; }
        public string Logo { get; set; }
        [AllowHtml]
        public string Thongtin { get; set; }

        public int? Quymocongty { get; set; }

        public int? Namthanhlap { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string Masothue { get; set; }

        public int? Tinhtrang { get; set; }

        public bool? Toptuyendung { get; set; }

        [StringLength(200)]
        public string VideoCT { get; set; }

        public DateTime? Ngaytao { get; set; }
        public List<int> listnghanh { get; set; }
    }
}