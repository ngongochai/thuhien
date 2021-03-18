namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KyNang")]
    public partial class KyNang
    {
        public KyNang()
        {
            KyNangUVs = new HashSet<KyNangUV>();
        }

        [Key]
        public int MAKN { get; set; }

        [StringLength(200)]
        public string TenKN { get; set; }

        public int? Manghanh { get; set; }

        public int? tinhtrang { get; set; }

        public virtual ICollection<KyNangUV> KyNangUVs { get; set; }
    }
}
