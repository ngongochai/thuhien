namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuotTruyCap")]
    public partial class LuotTruyCap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? today { get; set; }

        public int? total { get; set; }

        public DateTime? datetoday { get; set; }
    }
}
