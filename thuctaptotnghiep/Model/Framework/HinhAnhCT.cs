namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhCT")]
    public partial class HinhAnhCT
    {
        public int Id { get; set; }

        public int MACT { get; set; }

        public string AnhCT { get; set; }

        public virtual CongTy CongTy { get; set; }
    }
}
