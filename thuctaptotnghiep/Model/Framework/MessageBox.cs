namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageBox")]
    public partial class MessageBox
    {
        public int Id { get; set; }

        public int? MADK { get; set; }

        public int? MAUV { get; set; }

        public int? MACT { get; set; }

        public string Mess { get; set; }

        public int? Status { get; set; }

        public virtual UVDangKy UVDangKy { get; set; }
    }
}
