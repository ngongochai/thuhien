namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TepDinhkem")]
    public partial class TepDinhkem
    {
        public int Id { get; set; }

        public int MaUV { get; set; }

        public string Url { get; set; }

        public DateTime? Ngaytao { get; set; }

        public virtual UngVien UngVien { get; set; }
    }
}
