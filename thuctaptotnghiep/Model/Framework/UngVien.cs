namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UngVien")]
    public partial class UngVien
    {
        public UngVien()
        {
            CVDaluus = new HashSet<CVDaluu>();
            CVDaNops = new HashSet<CVDaNop>();
            CVDuocPVs = new HashSet<CVDuocPV>();
            CVmongmuons = new HashSet<CVmongmuon>();
            KyNangUVs = new HashSet<KyNangUV>();
            LuuHosoes = new HashSet<LuuHoso>();
            MoiPhongVans = new HashSet<MoiPhongVan>();
            NgonNguUVs = new HashSet<NgonNguUV>();
            TepDinhkems = new HashSet<TepDinhkem>();
            ThongTinDaoTaos = new HashSet<ThongTinDaoTao>();
            ThongTinDaoTaos1 = new HashSet<ThongTinDaoTao>();
            ThongTinNgheNghieps = new HashSet<ThongTinNgheNghiep>();
            UVDangKies = new HashSet<UVDangKy>();
        }

        [Key]
        public int MAUV { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string Hovatendem { get; set; }

        public DateTime? Ngaysinh { get; set; }

        public DateTime? Ngaydangky { get; set; }

        public int? Gioitinh { get; set; }

        [StringLength(20)]
        public string Dienthoai1 { get; set; }

        [StringLength(20)]
        public string Dienthoai2 { get; set; }

        [StringLength(500)]
        public string EmailDN { get; set; }

        [StringLength(100)]
        public string EmailLH { get; set; }

        [StringLength(200)]
        public string Matkhau { get; set; }

        public string Hinhanh { get; set; }

        [StringLength(200)]
        public string Hosoupload { get; set; }

        [StringLength(200)]
        public string Diachihientai { get; set; }

        public int? Quocgia { get; set; }

        [StringLength(200)]
        public string Thanhpho { get; set; }

        [StringLength(100)]
        public string Quanhuyen { get; set; }

        public int? MaQH { get; set; }

        [StringLength(50)]
        public string SoCMND { get; set; }

        public int? Honnhan { get; set; }

        [StringLength(500)]
        public string FacebookUrl { get; set; }

        public string Muctieu { get; set; }

        public int? Tinhtrang { get; set; }

        public int? vaitro { get; set; }

        public int? Loaitaikhoan { get; set; }

        [StringLength(100)]
        public string Chucdanh { get; set; }

        public int? Namkinhnghiem { get; set; }

        [StringLength(200)]
        public string thanhpho2 { get; set; }

        [StringLength(200)]
        public string thanhpho3 { get; set; }

        public int? MaTP1 { get; set; }

        public int? MaTP2 { get; set; }

        public int? MaTP3 { get; set; }

        public int? Luotxem { get; set; }

        public DateTime? Ngaycapnhat { get; set; }

        [StringLength(500)]
        public string matkhaureset { get; set; }

        public virtual city city { get; set; }

        public virtual city city1 { get; set; }

        public virtual city city2 { get; set; }

        public virtual ICollection<CVDaluu> CVDaluus { get; set; }

        public virtual ICollection<CVDaNop> CVDaNops { get; set; }

        public virtual ICollection<CVDuocPV> CVDuocPVs { get; set; }

        public virtual ICollection<CVmongmuon> CVmongmuons { get; set; }

        public virtual ICollection<KyNangUV> KyNangUVs { get; set; }

        public virtual ICollection<LuuHoso> LuuHosoes { get; set; }

        public virtual ICollection<MoiPhongVan> MoiPhongVans { get; set; }

        public virtual ICollection<NgonNguUV> NgonNguUVs { get; set; }

        public virtual ICollection<TepDinhkem> TepDinhkems { get; set; }

        public virtual ICollection<ThongTinDaoTao> ThongTinDaoTaos { get; set; }

        public virtual ICollection<ThongTinDaoTao> ThongTinDaoTaos1 { get; set; }

        public virtual ICollection<ThongTinNgheNghiep> ThongTinNgheNghieps { get; set; }

        public virtual ICollection<UVDangKy> UVDangKies { get; set; }
    }
}
