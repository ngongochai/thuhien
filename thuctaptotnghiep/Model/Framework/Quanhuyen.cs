namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quanhuyen")]
    public partial class Quanhuyen
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Tenquanhuyen { get; set; }

        public int? Matp { get; set; }

        public virtual city city { get; set; }
    }
}
