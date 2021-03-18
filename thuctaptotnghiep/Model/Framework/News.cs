namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int newsid { get; set; }

        public string titlenews { get; set; }

        public string titleimages { get; set; }

        public string htmlcontent { get; set; }

        public string description { get; set; }

        public string namepost { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? datepost { get; set; }

        public int? categoryId { get; set; }

        public bool? active { get; set; }

        public int? displayorder { get; set; }

        public bool? showonhomepage { get; set; }

        public bool? important { get; set; }

        public bool? popular { get; set; }

        public bool? Featured { get; set; }

        public int? ViewBest { get; set; }

        public bool? trash { get; set; }

        [StringLength(200)]
        public string Update_by { get; set; }

        public virtual categorynew categorynew { get; set; }
    }
}
