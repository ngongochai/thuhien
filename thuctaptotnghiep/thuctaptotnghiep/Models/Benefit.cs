using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace thuctaptotnghiep.Models
{
    public class Benefit
    {
        public int Id { get; set; }

        public int MaUV { get; set; }

        [StringLength(200)]
        public string Vitri { get; set; }

        [StringLength(100)]
        public string Loailuong { get; set; }

        public float? Minmucluong { get; set; }

        public float? Maxmucluong { get; set; }

        public int? MaCB { get; set; }

        [StringLength(200)]
        public string Capbac { get; set; }

        public int? Nghanhnghe1 { get; set; }

        public int? Nghanhnghe3 { get; set; }

        public int? Nghanhnghe2 { get; set; }

        public int? Thanhpho1 { get; set; }
        public int? Thanhpho3 { get; set; }
        public int? Thanhpho2 { get; set; }

        public double? USD { get; set; }
    }
}