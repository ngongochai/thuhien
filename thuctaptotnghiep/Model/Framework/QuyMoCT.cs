namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuyMoCT")]
    public partial class QuyMoCT
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }
    }
}
