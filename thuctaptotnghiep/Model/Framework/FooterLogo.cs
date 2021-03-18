namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FooterLogo")]
    public partial class FooterLogo
    {
        public int id { get; set; }

        public string footermember { get; set; }

        public string footeremployer { get; set; }

        public string footeradmin { get; set; }

        [StringLength(200)]
        public string logoimg { get; set; }
    }
}
