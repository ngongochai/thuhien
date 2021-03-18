
SELECT MACV,Luotxem FROM DangTinViecLam ORDER BY Luotxem DESC, MACV desc

create database timviec
use daynhaph_timviec

create table GopY(
id int identity primary key,
content nvarchar(max),
email nvarchar(200),
phone nvarchar(15),
datesend datetime,
)

create table ConfigMail(
id int identity primary key,
Name nvarchar(200),
smtUserName nvarchar(200),
smtpPassword nvarchar(200),
smtpHost nvarchar(200),
smtpPort int,
smtpSubject nvarchar(max),
smtpType int
)
create table CongTy(
MACT int identity primary key,
Tencongty nvarchar(400),
Nguoilienhe nvarchar(200),
Dienthoaiban varchar(11),
Dienthoaididong varchar(11),
Emailtuyendung varchar(200),
Emaildangnhap varchar(200),
Matkhau varchar(200),
Diachi nvarchar(500),
Thongtin nvarchar(500),
Quymocongty int,
Namthanhlap int,
Website varchar(200),
Fax varchar(20),
Masothue varchar(100),
)
drop table CongTy
create table DangTinViecLam(
	MACV int identity primary key,
	MACT int,
	TenCV nvarchar(100),
	MotaCV nvarchar(max),
	YeucauCV nvarchar(max),
	Manghanh1 int,
	Manghanh2 int,
	Manghanh3 int,
	Thanhpho1 nvarchar(100),
	Thanhpho2 nvarchar(100),
	Thanhpho3 nvarchar(100),
	Loaitien nvarchar(50),
	Minluong real,
	Maxluong real,
	Hienthiluong bit,
	Hinhthuc nvarchar(200),
	Hannhanhoso date,
	Gioitinh int,
	Mintuoi int,
	Maxtuoi int,
	Kinhnghiem int,
	Minkinhnghiem int,
	Maxkinhnghiem int,
	Capbac nvarchar(100),
	Bangcap nvarchar(100),
	Tags nvarchar(100),
	Trangthai int
)
create table AnhCongViec(
	MACV int,
	AnhUrl nvarchar(max)
)

create table NghanhNghe(
	Id int identity primary key,
	Tennghanh nvarchar(100),
	Idnghanhcha int,
	Tinhtrang int
)
create table UngVien(
 MAUV int identity primary key,
 Ten nvarchar(100),
 Hovatendem nvarchar(200),
 Ngaysinh datetime,
 Gioitinh bit,
 Dienthoai1 varchar(11),
 Dienthoai2 varchar(11),
 Email varchar(100),
 Matkhau varchar(200),
 Hinhanh nvarchar(max),
 Diachithuongtru nvarchar(200),
 Diachihientai nvarchar(200),
 Quocgia nvarchar(100),
 Thanhpho nvarchar(100),
 Quanhuyen nvarchar(100),
 SoCMND varchar(50),
 Honnhan nvarchar(100),
 FacebookUrl nvarchar(500),
 Muctieu nvarchar(max)
)
create table UVDangKy(
	Id int identity primary key,
	MACV int,
	MAUV int,
	MACT int,
	Tinhtrang bit
)
create table ThongTinNgheNghiep(
 MaUV int primary key,
 Congviecganday nvarchar(max),
 Mucdoquantam nvarchar(max),
 Namkinhnghiem int,
 Congtylamviecganday nvarchar(500),
 Bangcap nvarchar(200),
 Capbachientai nvarchar(100),
)
create table CVmongmuon(
	MaUV int primary key,
	Vitri nvarchar(200),
	Loailuong nvarchar(100),
	Minmucluong real,
	Maxmucluong real,
	Capbac nvarchar(200),
	Nghanhnghe1 nvarchar(200),
	Nghanhnghe2 nvarchar(200),
	Thanhpho1 nvarchar(100),
	quanhuyen1 nvarchar(100),
	Thanhpho2 nvarchar(100),
	quanhuyen2 nvarchar(100)
)
create table ThongTinDaoTao(
	MaUV int primary key,
	Tentruong nvarchar(200),
	Nghanhhoc nvarchar(100),
	Chuyennghanh nvarchar(200),
	Thangtotnghiep int,
	Namtotnghiep int, 
	Diemtrungbinh float,
	Hocvan nvarchar(100),
)
create table TepDinhkem(
	MaUV int primary key,
	Url nvarchar(max),
	Ngaytao datetime
)
create table CVDaluu(
	MAUV int,
	MACV int
)
Create table CVDaNop(
	MAUV int,
	MACV int
)
create table CVDuocPV(
	MAUV int,
	MACV int,
	
)
ALTER TABLE CVDuocPV ADD Thoigian datetime
ALTER TABLE MyTable ADD COLUMN NewField int NULL AFTER ID;
ALTER TABLE ThongTinDaoTao ALTER COLUMN Nghanhhoc int

create table ThongBao(
	MAUV int,
	MACT int,
	Noidung nvarchar(max)
)
use timviec
create table slideshow(
 int id
)
Create table HinhAnhCT(
	MACT int,
	AnhCT nvarchar(max)
)
create table CapBac(
	MACB int identity primary key,
	Tencapbac nvarchar(200),
	Ngaytao datetime,
	Tinhtrang int
)
create table KyNang(
	MAKN int identity primary key,
	TenKN nvarchar(200)
)
create table LuotTruyCap(
id int primary key,
today int null,
total int null,
datetoday datetime null
)
create proc sp_checkloginuser(
@username varchar(100),
@password varchar(100)
)
as
begin
declare @set int;
if exists(select ten from UngVien where EmailDN=@username and matkhau=@password and tinhtrang=1 and vaitro=1)
set @set=1;
else if exists(select ten from ungvien where emaildn=@username and matkhau=@password and tinhtrang=1 and vaitro=2)
set @set=2;
else
set @set=0;
select @set
end

drop proc sp_checkloginuser

insert into nghanhnghe values(N'An toàn lao động','',1,0)
insert into nghanhnghe values(N'Bác sĩ','',1,0)
insert into nghanhnghe values(N'Bán hàng','',1,0)
insert into nghanhnghe values(N'Bán hàng kỹ thuật','',1,0)
insert into nghanhnghe values(N'Bán lẻ/Bán sỉ' ,'',1,0)
insert into nghanhnghe values(N'Bảo hiểm' ,'',1,0)
insert into nghanhnghe values(N'Bảo trì/Sửa chữa','',1,0)
insert into nghanhnghe values(N'Bất động sản' ,'',1,0)
insert into nghanhnghe values(N'Biên phiên dịch' ,'',1,0)
insert into nghanhnghe values(N'Cấp quản lý điều hành' ,'',1,0)
insert into nghanhnghe values(N'Chứng khoán','',1,0)
insert into nghanhnghe values(N'Cơ khí','',1,0)
insert into nghanhnghe values(N'Công nghệ cao','',1,0)
insert into nghanhnghe values(N'Dầu khí' ,'',1,0)
insert into nghanhnghe values(N'Dệt may/Da giày','',1,0)
insert into nghanhnghe values(N'Dịch vụ khách hàng' ,'',1,0)
insert into nghanhnghe values(N'Dược Phẩm/Công nghệ sinh học','',1,0)
insert into nghanhnghe values(N'Dược sĩ' ,'',1,0)
insert into nghanhnghe values(N'Giáo dục/Đào tạo','',1,0)
insert into nghanhnghe values(N'Hàng cao cấp','',1,0)
insert into nghanhnghe values(N'Hàng gia dụng' ,'',1,0)
insert into nghanhnghe values(N'Hàng hải','',1,0)
insert into nghanhnghe values(N'Hàng không/Du lịch','',1,0)
insert into nghanhnghe values(N'Hàng tiêu dùng','',1,0)
insert into nghanhnghe values(N'Hành chánh/Thư ký','',1,0)
insert into nghanhnghe values(N'Hóa học/Hóa sinh','',1,0)
insert into nghanhnghe values(N'Hoạch định/Dự án','',1,0)
insert into nghanhnghe values(N'In ấn/ Xuất bản','',1,0)
insert into nghanhnghe values(N'Internet/Online Media' ,'',1,0)
insert into nghanhnghe values(N'IT - Phần mềm' ,'',1,0)
insert into nghanhnghe values(N'IT-Phần cứng/Mạng' ,'',1,0)
insert into nghanhnghe values(N'Kế toán' ,'',1,0)
insert into nghanhnghe values(N'Khác' ,'',1,0)
insert into nghanhnghe values(N'Kho vận' ,'',1,0)
insert into nghanhnghe values(N'Kiểm toán' ,'',1,0)
insert into nghanhnghe values(N'Kiến trúc/Thiết kế nội thất ' ,'',1,0)
insert into nghanhnghe values(N'Marketing' ,'',1,0)
insert into nghanhnghe values(N'Mới tốt nghiệp' ,'',1,0)
insert into nghanhnghe values(N'Môi trường/Xử lý chất thải','',1,0)
insert into nghanhnghe values(N'Mỹ Thuật/Nghệ Thuật/Thiết Kế' ,'',1,0)
insert into nghanhnghe values(N'Ngân hàng','',1,0)
insert into nghanhnghe values(N'Người nước ngoài/Việt Kiều','',1,0)
insert into nghanhnghe values(N'Nhà hàng/Khách sạn','',1,0)
insert into nghanhnghe values(N'Nhân sự','',1,0)
insert into nghanhnghe values(N'Nông nghiệp/Lâm nghiệp','',1,0)
insert into nghanhnghe values(N'Overseas Jobs' ,'',1,0)
insert into nghanhnghe values(N'Pháp lý','',1,0)
insert into nghanhnghe values(N'Phi chính phủ/Phi lợi nhuận','',1,0)
insert into nghanhnghe values(N'QA/QC' ,'',1,0)
insert into nghanhnghe values(N'Quảng cáo/Khuyến mãi/Đối ngoại' ,'',1,0)
insert into nghanhnghe values(N'Sản phẩm công nghiệp','',1,0)
insert into nghanhnghe values(N'Sản Xuất','',1,0)
insert into nghanhnghe values(N'Tài chính/Đầu tư','',1,0)
insert into nghanhnghe values(N'Thời trang','',1,0)
insert into nghanhnghe values(N'Thời vụ/Hợp đồng ngắn hạn' ,'',1,0)
insert into nghanhnghe values(N'Thu Mua/Vật Tư/Cung Vận','',1,0)
insert into nghanhnghe values(N'Thực phẩm &amp; Đồ uống','',1,0)
insert into nghanhnghe values(N'Trình dược viên' ,'',1,0)
insert into nghanhnghe values(N'Truyền hình/Truyền thông/Báo chí','',1,0)
insert into nghanhnghe values(N'Tư vấn' ,'',1,0)
insert into nghanhnghe values(N'Tự động hóa/Ô tô','',1,0)
insert into nghanhnghe values(N'Vận chuyển/Giao nhận','',1,0)
insert into nghanhnghe values(N'Viễn Thông' ,'',1,0)
insert into nghanhnghe values(N'Xây dựng','',1,0)
insert into nghanhnghe values(N'Xuất nhập khẩu','',1,0)
insert into nghanhnghe values(N'Y sĩ/Hộ lý' ,'',1,0)
insert into nghanhnghe values(N'Y tế/Chăm sóc sức khỏe' ,'',1,0)
insert into nghanhnghe values(N'Địa chất/Khoáng sản','',1,0)
insert into nghanhnghe values(N'Điện lạnh/Nhiệt lạnh','',1,0)
insert into nghanhnghe values(N'Điện/Điện tử','',1,0)

create table city(
 Id int identity primary key,
 City nvarchar(200),
 Mavung int
)
insert into city values(N'Hà Nội','')
insert into city values(N'TP. Hồ Chí Minh','')
insert into city values(N'Đà Nẵng','')
insert into city values(N'Cần Thơ','')
insert into city values(N'An Giang','')
insert into city values(N'Bà Rịa - Vũng Tàu','')
insert into city values(N'Bạc Liêu','')
insert into city values(N'Bình Dương','')
insert into city values(N'Bình Phước','')
insert into city values(N'Bình Thuận','')
insert into city values(N'Bình Định','')
insert into city values(N'Bắc Giang','')
insert into city values(N'Bắc Kạn','')
insert into city values(N'Bắc Nin','')
insert into city values(N'Bến Tre','')
insert into city values(N'Cao Bằng','')
insert into city values(N'Cà Mau','')
insert into city values(N'Đắk Lắk','')
insert into city values(N'Đồng Nai','')
insert into city values(N'Đồng Tháp','')
insert into city values(N'Đắk Nông','')
insert into city values(N'Gia Lai','')
insert into city values(N'Hậu Giang','')
insert into city values(N'Hà Giang','')
insert into city values(N'Hà Nam','')
insert into city values(N'Hà Tây','')
insert into city values(N'Hà Tĩnh','')
insert into city values(N'Hòa Bình','')
insert into city values(N'Hưng Yên','')
insert into city values(N'Hải Dương','')
insert into city values(N'Hải Phòng','')
insert into city values(N'Khánh Hòa','')
insert into city values(N'Kiên Giang','')
insert into city values(N'Kon Tum','')
insert into city values(N'Lai Châu','')
insert into city values(N'Long An','')
insert into city values(N'Lào Cai','')
insert into city values(N'Lâm Đồng','')
insert into city values(N'Lạng Sơn','')
insert into city values(N'Nam Định','')
insert into city values(N'Nghệ An','')
insert into city values(N'Ninh Bình','')
insert into city values(N'Ninh Thuận','')
insert into city values(N'Phú Thọ','')
insert into city values(N'Phú Yên','')
insert into city values(N'Quảng Bình','')
insert into city values(N'Quảng Nam','')
insert into city values(N'Quảng Ngãi','')
insert into city values(N'Quảng Ninh','')
insert into city values(N'Quảng Trị','')
insert into city values(N'Sơn La','')
insert into city values(N'Thanh Hóa','')
insert into city values(N'Thái Bình','')
insert into city values(N'Thái Nguyên','')
insert into city values(N'Thừa Thiên - Huế','')
insert into city values(N'Tiền Giang','')
insert into city values(N'Trà Vinh','')
insert into city values(N'Tuyên Quang','')
insert into city values(N'Tây Ninh','')
insert into city values(N'Vĩnh Long','')
insert into city values(N'Vĩnh Phúc','')
insert into city values(N'Yên Bái','')
create proc sp_checkloginemployer(
@username varchar(100),
@password varchar(100)
)
as
begin
declare @set int;
if exists(select tencongty from congty where emaildangnhap=@username and matkhau=@password and tinhtrang=1)
set @set=1;
else if exists(select tencongty from congty where emaildangnhap=@username and matkhau=@password and tinhtrang=0)
set @set=2;
else
set @set=0;
select @set
end

create table Quanhuyen(
	Id int identity primary key,
	Tenquanhuyen nvarchar(200),
	Matp int
)
create table KyNangUV(
id int identity primary key,
MAUV int,
TenKN nvarchar(100),
MAKN int 
)
create table NgonNgu(
 Id int identity primary key,
 Ten nvarchar(100),
 Tinhtrang int
)
create table NgonNguUV(
	id int identity primary key,
	Ten nvarchar(100),
	trinhdo int,
	MaNN int
)
create table CBmongmuon(
	Id int identity primary key,
	Ten nvarchar(100),
	Tinhtrang int,
)
create table QuyMoCT(
 Id int identity primary key,
 Ten nvarchar(100)
)
create table MessageBox(
	Id int identity primary key,
	MADK int,
	MAUV int,
	MACT int,
	Mess nvarchar(max),
	Status int,
)
select count(*) from DangTinViecLam where DangTinViecLam.Tinhtrang=1 and DangTinViecLam.Trangthai=1
create table MoiPhongVan(
	Id int identity primary key,
	MAUV int,
	MACT int,
	Tinhtrang int,
	Trangthai int,
	Ngaymoi datetime,
	Ghichu nvarchar(max)
)
create table LuuHoso(
	Id int identity primary key,
	MAUV int,
	MACT int,
	Ngayluu datetime,
	Tinhtrang int,
	Trangthai int,
	Ghichu nvarchar(max)
)
create table SkillWork(
	Id int identity primary key,
	MAN int ,
	MAKN int,
)
select count(*) from DangTinViecLam db where db.Tags1='html' or db.Tags2='html' or db.Tags3='html'

create table News(
newsid int identity primary key,
titlenews nvarchar(max),
titleimages nvarchar(max),
htmlcontent nvarchar(max),
description nvarchar(max),
namepost nvarchar(max),
datepost smalldatetime,
categoryId int,
active bit,
displayorder int,
showonhomepage bit,
important bit,
popular bit,
Featured bit,
ViewBest int,
trash bit,
)
create table categorynews(
categoryId int identity primary key,
categoryName nvarchar(max),
showonhomepage bit,
displayorder int,
position int,
active bit,
trash bit
)
select * from KyNang
select * from KyNangUV
select * from UngVien

insert into KyNangUV values('19','hai','1347')

select * from UngVien uv inner join KyNangUV kn on kn.TenKN like 'hai' and kn.MAUV=uv.MAUV 

create table Slideshow(
id int identity primary key,
imgurl nvarchar(500),
displayorder int,
status int
)

create table FooterLogo(
id int identity primary key,
footermember nvarchar(max),
footeremployer nvarchar(max),
footeradmin nvarchar(max),
logoimg nvarchar(200)
)

create table Advert(
	id int identity primary key,
	advertlayoutleft nvarchar(max),
	advertlayoutright nvarchar(max),
	advertindexleft nvarchar(max),
	advertindexright nvarchar(max),
	advertindextop nvarchar(max),
	advertsearchjobleft nvarchar(max),
	advertsearchjobbottom nvarchar(max),
	advertlistnews nvarchar(max),
	advertdetailnews nvarchar(max),
)

DELETE  FROM HinhAnhCT
WHERE MACT=4;