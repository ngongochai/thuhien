namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GopY")]
    public partial class GopY
    {
        public int id { get; set; }

        public string content { get; set; }

        [StringLength(200)]
        public string email { get; set; }

        [StringLength(15)]
        public string phone { get; set; }

        public DateTime? datesend { get; set; }
    }
}
