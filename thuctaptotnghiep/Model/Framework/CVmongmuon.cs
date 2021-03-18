namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CVmongmuon")]
    public partial class CVmongmuon
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

        public virtual CBmongmuon CBmongmuon { get; set; }

        public virtual city city { get; set; }

        public virtual city city1 { get; set; }

        public virtual city city2 { get; set; }

        public virtual NghanhNghe NghanhNghe { get; set; }

        public virtual NghanhNghe NghanhNghe4 { get; set; }

        public virtual NghanhNghe NghanhNghe5 { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
