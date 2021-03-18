namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CVDaluu")]
    public partial class CVDaluu
    {
        public int Id { get; set; }

        public int? MAUV { get; set; }

        public int? MACV { get; set; }

        public DateTime? Ngayluu { get; set; }

        public virtual DangTinViecLam DangTinViecLam { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
