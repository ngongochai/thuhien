namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slideshow")]
    public partial class Slideshow
    {
        public int id { get; set; }

        [StringLength(500)]
        public string imgurl { get; set; }

        public int? status { get; set; }

        public int? displayorder { get; set; }

        [StringLength(500)]
        public string url { get; set; }

        [StringLength(500)]
        public string videoembed { get; set; }
    }
}
