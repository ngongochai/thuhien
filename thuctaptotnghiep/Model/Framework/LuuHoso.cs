namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuuHoso")]
    public partial class LuuHoso
    {
        public int Id { get; set; }

        public int? MAUV { get; set; }

        public int? MACT { get; set; }

        public DateTime? Ngayluu { get; set; }

        public int? Tinhtrang { get; set; }

        public int? Trangthai { get; set; }

        public string Ghichu { get; set; }

        public DateTime? Ngayphanhoi { get; set; }

        public virtual CongTy CongTy { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
