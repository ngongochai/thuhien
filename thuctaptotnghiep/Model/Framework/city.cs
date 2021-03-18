namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("city")]
    public partial class city
    {
        public city()
        {
            CongTies = new HashSet<CongTy>();
            CongTies1 = new HashSet<CongTy>();
            CongTies2 = new HashSet<CongTy>();
            CVmongmuons = new HashSet<CVmongmuon>();
            CVmongmuons1 = new HashSet<CVmongmuon>();
            CVmongmuons2 = new HashSet<CVmongmuon>();
            DangTinViecLams = new HashSet<DangTinViecLam>();
            DangTinViecLams1 = new HashSet<DangTinViecLam>();
            DangTinViecLams2 = new HashSet<DangTinViecLam>();
            Quanhuyens = new HashSet<Quanhuyen>();
            UngViens = new HashSet<UngVien>();
            UngViens1 = new HashSet<UngVien>();
            UngViens2 = new HashSet<UngVien>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("City")]
        [Required]
        [StringLength(200)]
        public string City1 { get; set; }

        public int? Mavung { get; set; }

        public virtual ICollection<CongTy> CongTies { get; set; }

        public virtual ICollection<CongTy> CongTies1 { get; set; }

        public virtual ICollection<CongTy> CongTies2 { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons1 { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons2 { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams1 { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams2 { get; set; }

        public virtual ICollection<Quanhuyen> Quanhuyens { get; set; }

        public virtual ICollection<UngVien> UngViens { get; set; }

        public virtual ICollection<UngVien> UngViens1 { get; set; }

        public virtual ICollection<UngVien> UngViens2 { get; set; }
    }
}
