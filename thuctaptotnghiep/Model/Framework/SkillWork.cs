namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SkillWork")]
    public partial class SkillWork
    {
        public int Id { get; set; }

        public int? MAN { get; set; }

        public int? MAKN { get; set; }
    }
}
