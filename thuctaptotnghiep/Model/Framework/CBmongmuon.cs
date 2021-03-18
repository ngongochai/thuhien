namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CBmongmuon")]
    public partial class CBmongmuon
    {
        public CBmongmuon()
        {
            CVmongmuons = new HashSet<CVmongmuon>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        public int? Tinhtrang { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons { get; set; }
    }
}
