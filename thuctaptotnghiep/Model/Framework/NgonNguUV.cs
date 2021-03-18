namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NgonNguUV")]
    public partial class NgonNguUV
    {
        public int id { get; set; }

        public int? MAUV { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(100)]
        public string Tentrinhdo { get; set; }

        public int? trinhdo { get; set; }

        public int? MaNN { get; set; }

        public int? count { get; set; }

        public virtual NgonNgu NgonNgu { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
