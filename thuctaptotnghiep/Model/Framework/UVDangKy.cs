namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UVDangKy")]
    public partial class UVDangKy
    {
        public UVDangKy()
        {
            MessageBoxes = new HashSet<MessageBox>();
        }

        public int Id { get; set; }

        public int? MACV { get; set; }

        public int? MAUV { get; set; }

        public int? MACT { get; set; }

        public int? Tinhtrang { get; set; }

        public DateTime? Ngaydangky { get; set; }

        public int? Trangthai { get; set; }

        public DateTime? Ngayphanhoi { get; set; }

        public string Tinnhan { get; set; }

        public virtual CongTy CongTy { get; set; }

        public virtual DangTinViecLam DangTinViecLam { get; set; }

        public virtual ICollection<MessageBox> MessageBoxes { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
