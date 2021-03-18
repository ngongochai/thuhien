namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CVDaNop")]
    public partial class CVDaNop
    {
        public int Id { get; set; }

        public int MAUV { get; set; }

        public int? MACV { get; set; }

        public DateTime? Ngaynop { get; set; }

        public virtual DangTinViecLam DangTinViecLam { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
