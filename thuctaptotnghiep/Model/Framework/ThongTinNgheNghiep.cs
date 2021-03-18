namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinNgheNghiep")]
    public partial class ThongTinNgheNghiep
    {
        public int Id { get; set; }

        public int MaUV { get; set; }

        public string Mucdoquantam { get; set; }

        public DateTime? Fromday { get; set; }

        public DateTime? Today { get; set; }

        public int? Namkinhnghiem { get; set; }

        [StringLength(500)]
        public string Congtylamviecganday { get; set; }

        [StringLength(100)]
        public string Capbachientai { get; set; }

        public int? resumid { get; set; }

        public int? Hiennay { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
