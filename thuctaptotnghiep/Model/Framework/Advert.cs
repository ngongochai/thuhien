namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advert")]
    public partial class Advert
    {
        public int id { get; set; }

        public string advertlayoutleft { get; set; }

        public string advertlayoutright { get; set; }

        public string advertindexleft { get; set; }

        public string advertindexright { get; set; }

        public string advertindextop { get; set; }

        public string advertsearchjobleft { get; set; }

        public string advertsearchjobbottom { get; set; }

        public string advertlistnews { get; set; }

        public string advertdetailnews { get; set; }

        public string advertdetailjobright { get; set; }

        public string advertdetailjobleft { get; set; }
    }
}
