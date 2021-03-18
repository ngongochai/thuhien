namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NgonNgu")]
    public partial class NgonNgu
    {
        public NgonNgu()
        {
            NgonNguUVs = new HashSet<NgonNguUV>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        public int? Tinhtrang { get; set; }

        public virtual ICollection<NgonNguUV> NgonNguUVs { get; set; }
    }
}
