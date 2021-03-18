namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongTy")]
    public partial class CongTy
    {
        public CongTy()
        {
            DangTinViecLams = new HashSet<DangTinViecLam>();
            DangTinViecLams1 = new HashSet<DangTinViecLam>();
            HinhAnhCTs = new HashSet<HinhAnhCT>();
            LuuHosoes = new HashSet<LuuHoso>();
            MoiPhongVans = new HashSet<MoiPhongVan>();
            UVDangKies = new HashSet<UVDangKy>();
        }

        [Key]
        public int MACT { get; set; }

        [StringLength(400)]
        public string Tencongty { get; set; }

        [StringLength(200)]
        public string Nguoilienhe { get; set; }

        [StringLength(20)]
        public string Dienthoaiban { get; set; }

        [StringLength(20)]
        public string Dienthoaididong { get; set; }

        [StringLength(200)]
        public string Emailtuyendung { get; set; }

        [StringLength(200)]
        public string Emaildangnhap { get; set; }

        [StringLength(200)]
        public string Matkhau { get; set; }

        [StringLength(200)]
        public string Diachi { get; set; }

        public int? Manghanh1 { get; set; }

        public int? Manghanh2 { get; set; }

        public int? Manghanh3 { get; set; }

        public string Logo { get; set; }

        public string Thongtin { get; set; }

        public int? Quymocongty { get; set; }

        public int? Namthanhlap { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string Masothue { get; set; }

        public int? Tinhtrang { get; set; }

        public bool? Toptuyendung { get; set; }

        [StringLength(200)]
        public string VideoCT { get; set; }

        public DateTime? Ngaytao { get; set; }

        [StringLength(200)]
        public string Thanhpho1 { get; set; }

        [StringLength(200)]
        public string Thanhpho2 { get; set; }

        [StringLength(200)]
        public string Thanhpho3 { get; set; }

        public int? MaTP1 { get; set; }

        public int? MaTP2 { get; set; }

        public int? MaTP3 { get; set; }

        [StringLength(500)]
        public string matkhaureset { get; set; }

        [StringLength(200)]
        public string Nguoicapnhat { get; set; }

        public virtual city city { get; set; }

        public virtual city city1 { get; set; }

        public virtual city city2 { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams { get; set; }

        public virtual ICollection<DangTinViecLam> DangTinViecLams1 { get; set; }

        public virtual ICollection<HinhAnhCT> HinhAnhCTs { get; set; }

        public virtual ICollection<LuuHoso> LuuHosoes { get; set; }

        public virtual ICollection<MoiPhongVan> MoiPhongVans { get; set; }

        public virtual ICollection<UVDangKy> UVDangKies { get; set; }
    }
}
