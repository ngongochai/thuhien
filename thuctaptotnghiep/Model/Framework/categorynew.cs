namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class categorynew
    {
        public categorynew()
        {
            News = new HashSet<News>();
        }

        [Key]
        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public bool? showonhomepage { get; set; }

        public int? displayorder { get; set; }

        public int? position { get; set; }

        public bool? active { get; set; }

        public bool? trash { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
