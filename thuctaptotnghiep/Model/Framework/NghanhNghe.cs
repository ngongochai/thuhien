namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NghanhNghe")]
    public partial class NghanhNghe
    {
        public NghanhNghe()
        {
            CVmongmuons = new HashSet<CVmongmuon>();
            CVmongmuons1 = new HashSet<CVmongmuon>();
            CVmongmuons2 = new HashSet<CVmongmuon>();
            DangTinViecLams = new HashSet<DangTinViecLam>();
            DangTinViecLams1 = new HashSet<DangTinViecLam>();
            DangTinViecLams2 = new HashSet<DangTinViecLam>();
            ThongTinDaoTaos = new HashSet<ThongTinDaoTao>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Tennghanh { get; set; }

        public int? Idnghanhcha { get; set; }

        public int? Tinhtrang { get; set; }

        public int? lv { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons1 { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons2 { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams1 { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams2 { get; set; }

        public virtual ICollection<ThongTinDaoTao> ThongTinDaoTaos { get; set; }
    }
}
