namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapBac")]
    public partial class CapBac
    {
        public CapBac()
        {
            ThongTinDaoTaos = new HashSet<ThongTinDaoTao>();
        }

        [Key]
        public int MACB { get; set; }

        [StringLength(200)]
        public string Tencapbac { get; set; }

        public DateTime? Ngaytao { get; set; }

        public int? Tinhtrang { get; set; }

        public virtual ICollection<ThongTinDaoTao> ThongTinDaoTaos { get; set; }
    }
}
