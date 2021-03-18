namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KyNangUV")]
    public partial class KyNangUV
    {
        public int id { get; set; }

        public int? MAUV { get; set; }

        [StringLength(100)]
        public string TenKN { get; set; }

        public int? MAKN { get; set; }

        public virtual KyNang KyNang { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
