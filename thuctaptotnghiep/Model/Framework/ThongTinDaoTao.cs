namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinDaoTao")]
    public partial class ThongTinDaoTao
    {
        public int Id { get; set; }

        public int MaUV { get; set; }

        [StringLength(200)]
        public string Tentruong { get; set; }

        public int? Nghanhhoc { get; set; }

        [StringLength(200)]
        public string Chuyennghanh { get; set; }

        public DateTime? Tuthang { get; set; }

        public DateTime? Denthang { get; set; }

        public int? MaHV { get; set; }

        [StringLength(100)]
        public string Hocvan { get; set; }

        public string Thanhtuu { get; set; }

        public int? resumid { get; set; }

        public virtual CapBac CapBac { get; set; }

        public virtual NghanhNghe NghanhNghe { get; set; }

        public virtual UngVien UngVien { get; set; }

        public virtual UngVien UngVien1 { get; set; }
    }
}
