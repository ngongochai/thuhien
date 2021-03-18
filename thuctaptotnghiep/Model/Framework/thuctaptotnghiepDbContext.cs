namespace Model.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class thuctaptotnghiepDbContext : DbContext
    {
        public thuctaptotnghiepDbContext()
            : base("name=thuctaptotnghiepDbContext")
        {
        }

        public virtual DbSet<Advert> Adverts { get; set; }
        public virtual DbSet<AnhCongViec> AnhCongViecs { get; set; }
        public virtual DbSet<CapBac> CapBacs { get; set; }
        public virtual DbSet<categorynew> categorynews { get; set; }
        public virtual DbSet<CBmongmuon> CBmongmuons { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<ConfigMail> ConfigMails { get; set; }
        public virtual DbSet<CongTy> CongTies { get; set; }
        public virtual DbSet<CVDaluu> CVDaluus { get; set; }
        public virtual DbSet<CVDaNop> CVDaNops { get; set; }
        public virtual DbSet<CVDuocPV> CVDuocPVs { get; set; }
        public virtual DbSet<CVmongmuon> CVmongmuons { get; set; }
        public virtual DbSet<DangTinViecLam> DangTinViecLams { get; set; }
        public virtual DbSet<FooterLogo> FooterLogoes { get; set; }
        public virtual DbSet<GopY> Gopies { get; set; }
        public virtual DbSet<HinhAnhCT> HinhAnhCTs { get; set; }
        public virtual DbSet<KyNang> KyNangs { get; set; }
        public virtual DbSet<KyNangUV> KyNangUVs { get; set; }
        public virtual DbSet<LuotTruyCap> LuotTruyCaps { get; set; }
        public virtual DbSet<LuuHoso> LuuHosoes { get; set; }
        public virtual DbSet<MessageBox> MessageBoxes { get; set; }
        public virtual DbSet<MoiPhongVan> MoiPhongVans { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NghanhNghe> NghanhNghes { get; set; }
        public virtual DbSet<NgonNgu> NgonNgus { get; set; }
        public virtual DbSet<NgonNguUV> NgonNguUVs { get; set; }
        public virtual DbSet<Quanhuyen> Quanhuyens { get; set; }
        public virtual DbSet<QuyMoCT> QuyMoCTs { get; set; }
        public virtual DbSet<SkillWork> SkillWorks { get; set; }
        public virtual DbSet<Slideshow> Slideshows { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TepDinhkem> TepDinhkems { get; set; }
        public virtual DbSet<ThongTinDaoTao> ThongTinDaoTaos { get; set; }
        public virtual DbSet<ThongTinNgheNghiep> ThongTinNgheNghieps { get; set; }
        public virtual DbSet<UngVien> UngViens { get; set; }
        public virtual DbSet<UVDangKy> UVDangKies { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapBac>()
                .HasMany(e => e.ThongTinDaoTaos)
                .WithOptional(e => e.CapBac)
                .HasForeignKey(e => e.MaHV);

            modelBuilder.Entity<CBmongmuon>()
                .HasMany(e => e.CVmongmuons)
                .WithOptional(e => e.CBmongmuon)
                .HasForeignKey(e => e.MaCB);

            modelBuilder.Entity<city>()
                .HasMany(e => e.CongTies)
                .WithOptional(e => e.city)
                .HasForeignKey(e => e.MaTP1);

            modelBuilder.Entity<city>()
                .HasMany(e => e.CongTies1)
                .WithOptional(e => e.city1)
                .HasForeignKey(e => e.MaTP2);

            modelBuilder.Entity<city>()
                .HasMany(e => e.CongTies2)
                .WithOptional(e => e.city2)
                .HasForeignKey(e => e.MaTP3);

            modelBuilder.Entity<city>()
                .HasMany(e => e.CVmongmuons)
                .WithOptional(e => e.city)
                .HasForeignKey(e => e.Thanhpho1);

            modelBuilder.Entity<city>()
                .HasMany(e => e.CVmongmuons1)
                .WithOptional(e => e.city1)
                .HasForeignKey(e => e.Thanhpho3);

            modelBuilder.Entity<city>()
                .HasMany(e => e.CVmongmuons2)
                .WithOptional(e => e.city2)
                .HasForeignKey(e => e.Thanhpho2);

            modelBuilder.Entity<city>()
                .HasMany(e => e.DangTinViecLams)
                .WithOptional(e => e.city)
                .HasForeignKey(e => e.MaTP1);

            modelBuilder.Entity<city>()
                .HasMany(e => e.DangTinViecLams1)
                .WithOptional(e => e.city1)
                .HasForeignKey(e => e.MaTP2);

            modelBuilder.Entity<city>()
                .HasMany(e => e.DangTinViecLams2)
                .WithOptional(e => e.city2)
                .HasForeignKey(e => e.MaTP3);

            modelBuilder.Entity<city>()
                .HasMany(e => e.Quanhuyens)
                .WithOptional(e => e.city)
                .HasForeignKey(e => e.Matp);

            modelBuilder.Entity<city>()
                .HasMany(e => e.UngViens)
                .WithOptional(e => e.city)
                .HasForeignKey(e => e.MaTP1);

            modelBuilder.Entity<city>()
                .HasMany(e => e.UngViens1)
                .WithOptional(e => e.city1)
                .HasForeignKey(e => e.MaTP2);

            modelBuilder.Entity<city>()
                .HasMany(e => e.UngViens2)
                .WithOptional(e => e.city2)
                .HasForeignKey(e => e.MaTP3);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Dienthoaiban)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Dienthoaididong)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Emailtuyendung)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Emaildangnhap)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.Masothue)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.VideoCT)
                .IsUnicode(false);

            modelBuilder.Entity<CongTy>()
                .HasMany(e => e.DangTinViecLams)
                .WithOptional(e => e.CongTy)
                .HasForeignKey(e => e.MACT);

            modelBuilder.Entity<CongTy>()
                .HasMany(e => e.DangTinViecLams1)
                .WithOptional(e => e.CongTy1)
                .HasForeignKey(e => e.MACT);

            modelBuilder.Entity<CongTy>()
                .HasMany(e => e.HinhAnhCTs)
                .WithRequired(e => e.CongTy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DangTinViecLam>()
                .Property(e => e.AdminDienthoai)
                .IsUnicode(false);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.CVmongmuons)
                .WithOptional(e => e.NghanhNghe)
                .HasForeignKey(e => e.Nghanhnghe1);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.CVmongmuons1)
                .WithOptional(e => e.NghanhNghe4)
                .HasForeignKey(e => e.Nghanhnghe3);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.CVmongmuons2)
                .WithOptional(e => e.NghanhNghe5)
                .HasForeignKey(e => e.Nghanhnghe2);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.DangTinViecLams)
                .WithOptional(e => e.NghanhNghe)
                .HasForeignKey(e => e.Manghanh1);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.DangTinViecLams1)
                .WithOptional(e => e.NghanhNghe1)
                .HasForeignKey(e => e.Manghanh2);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.DangTinViecLams2)
                .WithOptional(e => e.NghanhNghe2)
                .HasForeignKey(e => e.Manghanh3);

            modelBuilder.Entity<NghanhNghe>()
                .HasMany(e => e.ThongTinDaoTaos)
                .WithOptional(e => e.NghanhNghe)
                .HasForeignKey(e => e.Nghanhhoc);

            modelBuilder.Entity<NgonNgu>()
                .HasMany(e => e.NgonNguUVs)
                .WithOptional(e => e.NgonNgu)
                .HasForeignKey(e => e.MaNN);

            modelBuilder.Entity<UngVien>()
                .Property(e => e.Dienthoai1)
                .IsUnicode(false);

            modelBuilder.Entity<UngVien>()
                .Property(e => e.Dienthoai2)
                .IsUnicode(false);

            modelBuilder.Entity<UngVien>()
                .Property(e => e.EmailDN)
                .IsUnicode(false);

            modelBuilder.Entity<UngVien>()
                .Property(e => e.EmailLH)
                .IsUnicode(false);

            modelBuilder.Entity<UngVien>()
                .Property(e => e.Matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<UngVien>()
                .Property(e => e.SoCMND)
                .IsUnicode(false);

            modelBuilder.Entity<UngVien>()
                .HasMany(e => e.CVDaNops)
                .WithRequired(e => e.UngVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UngVien>()
                .HasMany(e => e.CVmongmuons)
                .WithRequired(e => e.UngVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UngVien>()
                .HasMany(e => e.TepDinhkems)
                .WithRequired(e => e.UngVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UngVien>()
                .HasMany(e => e.ThongTinDaoTaos)
                .WithRequired(e => e.UngVien)
                .HasForeignKey(e => e.MaUV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UngVien>()
                .HasMany(e => e.ThongTinDaoTaos1)
                .WithRequired(e => e.UngVien1)
                .HasForeignKey(e => e.MaUV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UngVien>()
                .HasMany(e => e.ThongTinNgheNghieps)
                .WithRequired(e => e.UngVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UVDangKy>()
                .HasMany(e => e.MessageBoxes)
                .WithOptional(e => e.UVDangKy)
                .HasForeignKey(e => e.MADK);
        }
    }
}
