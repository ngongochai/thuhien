namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhCongViec")]
    public partial class AnhCongViec
    {
        public int Id { get; set; }

        public int? MACV { get; set; }

        public string AnhUrl { get; set; }

        public virtual DangTinViecLam DangTinViecLam { get; set; }
    }
}
