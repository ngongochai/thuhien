namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangTinViecLam")]
    public partial class DangTinViecLam
    {
        public DangTinViecLam()
        {
            AnhCongViecs = new HashSet<AnhCongViec>();
            CVDaluus = new HashSet<CVDaluu>();
            CVDaNops = new HashSet<CVDaNop>();
            CVDuocPVs = new HashSet<CVDuocPV>();
            UVDangKies = new HashSet<UVDangKy>();
        }

        [Key]
        public int MACV { get; set; }

        public int? MACT { get; set; }

        [StringLength(200)]
        public string TenCV { get; set; }

        public string MotaCV { get; set; }

        public string YeucauCV { get; set; }

        public int? Manghanh1 { get; set; }

        public int? Manghanh2 { get; set; }

        public int? Manghanh3 { get; set; }

        [StringLength(200)]
        public string Thanhpho1 { get; set; }

        [StringLength(200)]
        public string Thanhpho2 { get; set; }

        [StringLength(200)]
        public string Thanhpho3 { get; set; }

        public int? MaTP1 { get; set; }

        public int? MaTP2 { get; set; }

        public int? MaTP3 { get; set; }

        [StringLength(200)]
        public string Loaitien { get; set; }

        public float? Minluong { get; set; }

        public float? Maxluong { get; set; }

        public int? Luongthoathuan { get; set; }

        public bool? Hienthiluong { get; set; }

        [StringLength(200)]
        public string Hinhthuc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Hannhanhoso { get; set; }

        public int? Gioitinh { get; set; }

        public int? Mintuoi { get; set; }

        public int? Maxtuoi { get; set; }

        public int? Kinhnghiem { get; set; }

        public int? Minkinhnghiem { get; set; }

        public int? Maxkinhnghiem { get; set; }

        [StringLength(100)]
        public string Capbac { get; set; }

        [StringLength(100)]
        public string Bangcap { get; set; }

        [StringLength(100)]
        public string Tags2 { get; set; }

        [StringLength(100)]
        public string Tags3 { get; set; }

        [StringLength(100)]
        public string Tags1 { get; set; }

        public int? Trangthai { get; set; }

        public DateTime? Ngaydang { get; set; }

        public int? Tinhtrang { get; set; }

        [StringLength(100)]
        public string NgonnguHS { get; set; }

        [StringLength(300)]
        public string NguoiLH { get; set; }

        [StringLength(20)]
        public string DienthoaiLH { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public int? MACB { get; set; }

        [StringLength(500)]
        public string DiachiHS { get; set; }

        public int? Vieclamtotnhat { get; set; }

        public int? Vieclamgoiy { get; set; }

        public int? hot { get; set; }

        public DateTime? NgayhethangTop { get; set; }

        public DateTime? NgayhethangRe { get; set; }

        public DateTime? Ngayhethangjob { get; set; }

        public int? Luotxem { get; set; }

        [StringLength(200)]
        public string AdminTencongty { get; set; }

        [StringLength(200)]
        public string AdminNguoiLH { get; set; }

        [StringLength(200)]
        public string AdminEmail { get; set; }

        [StringLength(20)]
        public string AdminDienthoai { get; set; }

        [StringLength(200)]
        public string AdminDiachi { get; set; }

        public int? Type { get; set; }

        [StringLength(200)]
        public string Nguoikichhoat { get; set; }

        [StringLength(200)]
        public string Nguoixoa { get; set; }

        [StringLength(200)]
        public string Nguoian { get; set; }

        [StringLength(200)]
        public string Nguoitao { get; set; }

        [StringLength(200)]
        public string Nguoichinhsua { get; set; }

        public virtual ICollection<AnhCongViec> AnhCongViecs { get; set; }

        public virtual city city { get; set; }

        public virtual city city1 { get; set; }

        public virtual city city2 { get; set; }

        public virtual CongTy CongTy { get; set; }

        public virtual CongTy CongTy1 { get; set; }

        public virtual ICollection<CVDaluu> CVDaluus { get; set; }

        public virtual ICollection<CVDaNop> CVDaNops { get; set; }

        public virtual ICollection<CVDuocPV> CVDuocPVs { get; set; }

        public virtual NghanhNghe NghanhNghe { get; set; }

        public virtual NghanhNghe NghanhNghe1 { get; set; }

        public virtual NghanhNghe NghanhNghe2 { get; set; }

        public virtual ICollection<UVDangKy> UVDangKies { get; set; }
    }
}
